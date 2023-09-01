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
    public class BicyclePartController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;
        public BicyclePartController(IRepository repository, UserManager<PedalProUser> userManager)
        {
            _repsository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAllBicyclePart")]
        public async Task<IActionResult> GetAllBicyclePart()
        {
            var bicyclePart = await _repsository.GetAllBicyclePartAsync();
             return Ok(bicyclePart);
        }

        [HttpGet]
        [Route("GetBicyclePart/{bicyclePartId}")]
        public async Task<IActionResult> GetBicyclePart(int bicyclePartId)
        {
            try
            {
                var result = await _repsository.GetBicyclePartAsync(bicyclePartId);
                if (result == null) return NotFound("Bicycle part does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddbicyclePart")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Addbicyclepart(BicyclePartViewModel bicyclePartAdd)
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


            var bicyclepartAdd = new BicyclePart { BicyclePartName = bicyclePartAdd.BicyclePartName };

            try
            {
                _repsository.Add(bicyclepartAdd);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(bicyclepartAdd);
        }


        [HttpPut]
        [Route("Editbicyclepart/{bicyclePartId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<BicyclePartViewModel>> Updatebicyclepart(int bicyclePartId, BicyclePartViewModel bicyclePartModel)
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

                var existingPart = await _repsository.GetBicyclePartAsync(bicyclePartId);
                if (existingPart == null) return NotFound("The Bicycle Part does not exist");

                existingPart.BicyclePartName = bicyclePartModel.BicyclePartName;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingPart);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }

        [HttpDelete]
        [Route("DeleteBicyclePart/{bicyclePartId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Deletebicyclepart(int bicyclePartId)
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

                var existingPart = await _repsository.GetBicyclePartAsync(bicyclePartId);
                if (existingPart == null) return NotFound($"The Bicycle Part does not exist");

                _repsository.Delete(existingPart);

                if (await _repsository.SaveChangesAsync()) return Ok(existingPart);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }
    }
}
