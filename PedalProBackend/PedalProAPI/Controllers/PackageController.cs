using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PackageController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;

        public PackageController(IRepository repository, UserManager<PedalProUser> userManager)
        {

            _repsository = repository;
            _userManager = userManager;
        }



        [HttpGet]
        [Route("GetAllPackagePrices")]
        public async Task<IActionResult> GetAllPackagePrices()
        {
            var packagePrices = await _repsository.GetAllPackagePriceAsync();
            return Ok(packagePrices);
        }


        [HttpGet]
        [Route("GetPackagePrice/{packagePriceId}")]
        public async Task<IActionResult> GetPackagePriceAsnyc(int packagePriceId)
        {
            try
            {
                var result = await _repsository.GetPackageAssocAsync(packagePriceId);
                if (result == null) return NotFound("Package Price does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }






        [HttpGet]
        [Route("GetAllPackages")]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _repsository.GetAllPackageAsync();
            return Ok(packages);
        }



        [HttpGet]
        [Route("GetPackage/{packageId}")]
        public async Task<IActionResult> GetPackageAsnyc(int packageId)
        {
            try
            {
                var result = await _repsository.GetPackageAsync(packageId);
                if (result == null) return NotFound("Package does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddPackage")]
        public async Task<IActionResult> AddPackage(PackageViewModel packageAdd)
        {
            try
            {
                

                var price = new Price()
                {
                    Price1 = packageAdd.Price1,
                    PriceDate = DateTime.Now
                };
                _repsository.Add(price);
                await _repsository.SaveChangesAsync();

                var package = new Package()
                {
                    PackageName = packageAdd.PackageName,
                    PackageDescription = packageAdd.PackageDescription,
                    NumPackageBookings=packageAdd.packagebookings
                };
                _repsository.Add(package);
                await _repsository.SaveChangesAsync();

                var packagePrice = new PackagePrice()
                {
                    PackageId = package.PackageId,
                    PriceId = price.PriceId
                };
                _repsository.Add(packagePrice);

                await _repsository.SaveChangesAsync();

                return Ok(package);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }


        }


        [HttpPut]
        [Route("EditPackage/{packageId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<PedalProRoleViewModel>> EditPackage(int packageId, PackageViewModel packageModel)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username not found.");
                }

                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var userId = user.Id;

                var userClaims = User.Claims;

                bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasAdminRole && !hasEmployeeRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }

                var existingPackagePrice = await _repsository.GetPackageAssocAsync(packageId);
                if (existingPackagePrice == null) return NotFound("The package price does not exist");

                //existingPackage. = roleModel.RoleName;
                var existingPackage=await _repsository.GetPackageAsync(packageId);
                var existingPrice = await _repsository.GetPriceAsync((int)existingPackagePrice.PriceId);

                existingPackage.PackageName= packageModel.PackageName;
                existingPackage.PackageDescription= packageModel.PackageDescription;
                existingPackage.NumPackageBookings = packageModel.packagebookings;

                existingPrice.Price1 = packageModel.Price1;
                existingPrice.PriceDate = DateTime.Now;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingPackage);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }



        [HttpDelete]
        [Route("DeletePackage/{packageId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteRole(int packageId)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username not found.");
                }

                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var userId = user.Id;

                var userClaims = User.Claims;

                bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasAdminRole && !hasEmployeeRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }

                var existingPackagePrice = await _repsository.GetPackageAssocAsync(packageId);
                if (existingPackagePrice == null) return NotFound("The package price does not exist");

                var existingPackage = await _repsository.GetPackageAsync(packageId);
                var existingPrice = await _repsository.GetPriceAsync((int)existingPackagePrice.PriceId);

                

                _repsository.Delete(existingPrice);
                await _repsository.SaveChangesAsync();

                _repsository.Delete(existingPackage);
                await _repsository.SaveChangesAsync();

                _repsository.Delete(existingPackagePrice);
                

                if (await _repsository.SaveChangesAsync()) return Ok(existingPackage);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }

        [HttpGet]
        [Route("GetPrice/{priceId}")]
        public async Task<IActionResult> GetPriceAsnyc(int priceId)
        {
            try
            {
                var result = await _repsository.GetPriceAsync(priceId);
                if (result == null) return NotFound("Price does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
    }
}
