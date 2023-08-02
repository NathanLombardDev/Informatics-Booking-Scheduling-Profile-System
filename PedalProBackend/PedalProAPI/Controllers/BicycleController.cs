using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using System.Data;
using System.Security.Claims;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BicycleController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;

        public BicycleController(IRepository repository, UserManager<PedalProUser> userManager)
        {
            _repsository = repository;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("GetAllBicycles")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetAllBicycles()
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

                var client = await _repsository.GetClient(userId);

                if (client == null)
                {
                    return BadRequest("Client not found.");
                }

                var userClaims = User.Claims;
                bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasClientRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }

                var bicycles = await _repsository.GetAllBicyclesAsyncTwo(client.ClientId);

                return Ok(bicycles);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetBicycle/{bicycleId}")]
        
        public async Task<IActionResult> GetBicycle(int bicycleId)
        {
            try
            {
                var result = await _repsository.GetBicycleAsync(bicycleId);
                if (result == null) return NotFound("Bicycle does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        /*
        [HttpPost]
        [Route("AddBicyle")]
       
        public async Task<IActionResult> AddBicyle(BicycleViewModel bicyleAdd)
        {
            var bicycle = new Bicycle 
            { 
                ClientId = bicyleAdd.ClientId,
                BicycleBrandId = bicyleAdd.BicycleBrandId,
                BicycleCategoryId=bicyleAdd.BicycleCategoryId,
                BicycleName=bicyleAdd.BicycleName
            };

            try
            {
                _repsository.Add(bicycle);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(bicycle);
        }
        */
        [HttpPost]
        [Route("AddBicycle")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> AddBicycleTwo(BicycleViewModel bicycleAdd)
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

                var client = await _repsository.GetClient(userId);

                if (client == null)
                {
                    return BadRequest("Client not found.");
                }
            try
            {
                var bicycle = new Bicycle
                {
                    ClientId = client.ClientId,
                    BicycleBrandId = bicycleAdd.BicycleBrandId,
                    BicycleCategoryId = bicycleAdd.BicycleCategoryId,
                    BicycleName = bicycleAdd.BicycleName
                };

                _repsository.Add(bicycle);
                await _repsository.SaveChangesAsync();

                return Ok(bicycle);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while adding a bicycle.");

                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        
        [HttpPut]
        [Route("EditBicycle/{bicycleId}")]
        [Authorize(Roles = "Client")]
        public async Task<ActionResult<PedalProRoleViewModel>> EditBicycle(int bicycleId, BicycleViewModel bicycleModel)
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

                var client = await _repsository.GetClient(userId);

                if (client == null)
                {
                    return BadRequest("Client not found.");
                }

                var existingBicycle = await _repsository.GetBicycleAsync(bicycleId);
                if (existingBicycle == null) return NotFound("The bicycle does not exist");

                existingBicycle.BicycleName = bicycleModel.BicycleName;
                existingBicycle.BicycleCategoryId = bicycleModel.BicycleCategoryId;
                existingBicycle.BicycleBrandId = bicycleModel.BicycleBrandId;
                existingBicycle.ClientId=client.ClientId;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingBicycle);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }
        


        [HttpDelete]
        [Route("Deletebicycle/{bicycleId}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Deletebicycle(int bicycleId)
        {
            try
            {
                var existingBicycle = await _repsository.GetBicycleAsync(bicycleId);
                if (existingBicycle == null) return NotFound($"The Bicycle does not exist");

                _repsository.Delete(existingBicycle);

                if (await _repsository.SaveChangesAsync()) return Ok(existingBicycle);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }
    }
}
