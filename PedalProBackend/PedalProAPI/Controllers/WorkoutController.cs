using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using System.Security.Claims;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkoutController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;

        public WorkoutController(IRepository repository, UserManager<PedalProUser> userManager)
        {
            _repsository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAllWorkouts")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetAllWorkouts()
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

                var bicycles = await _repsository.GetAllWorkoutsAsync(client.ClientId);

                return Ok(bicycles);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetWorkout/{workoutId}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetWorkout(int workoutId)
        {
            try
            {
                var result = await _repsository.GetBicycleAsync(workoutId);
                if (result == null) return NotFound("Workout does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddWorkout")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> AddWorkout(WorkoutViewModel workoutAdd)
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

                

                var workout = new Workout
                {
                    ClientId = client.ClientId,
                    Distance = workoutAdd.Distance,
                    Duration= TimeSpan.Parse(workoutAdd.Duration),
                    HeartRate = workoutAdd.HeartRate,
                    WorkoutTypeId=workoutAdd.WorkoutTypeId
                };

                _repsository.Add(workout);
                await _repsository.SaveChangesAsync();

                return Ok(workout);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // _logger.LogError(ex, "An error occurred while adding a bicycle.");

                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpDelete]
        [Route("DeleteWorkout/{workoutId}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> DeleteWorkout(int workoutId)
        {
            try
            {
                var existingWorkout = await _repsository.GetWorkout(workoutId);
                if (existingWorkout == null) return NotFound($"The workout does not exist");

                _repsository.Delete(existingWorkout);

                if (await _repsository.SaveChangesAsync()) return Ok(existingWorkout);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }



        [HttpGet]
        [Route("GetAllWorkoutTypes")]
        public async Task<IActionResult> GetAllWorkoutTypes()
        {
            try
            {
                var workoutTypes = await _repsository.GetAllWorkoutTypes();

                return Ok(workoutTypes);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetWorkoutType/{workoutTypeId}")]
        public async Task<IActionResult> GetWorkoutType(int workoutTypeId)
        {
            try
            {
                var result = await _repsository.GetWorkoutType(workoutTypeId);
                if (result == null) return NotFound("Workout type does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddWorkoutType")]
        public async Task<IActionResult> AddWorkoutType(WorkoutTypeViewModel cvm)
        {
            var clientType = new WorkoutType { WorkoutTypeName = cvm.WorkoutTypeName };

            try
            {
                _repsository.Add(clientType);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(clientType);
        }

        [HttpPut]
        [Route("EditWorkoutType/{workoutTypeId}")]
        public async Task<ActionResult<WorkoutTypeViewModel>> EditWorkoutType(int workoutTypeId, WorkoutTypeViewModel clientModel)
        {
            try
            {
                var existingclientType = await _repsository.GetWorkoutType(workoutTypeId);
                if (existingclientType == null) return NotFound($"The workout Type does not exist");

                existingclientType.WorkoutTypeName = clientModel.WorkoutTypeName;


                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingclientType);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }


        [HttpDelete]
        [Route("DeleteWorkoutType/{workoutTypeId}")]
        public async Task<IActionResult> DeleteWorkoutTypes(int workoutTypeId)
        {
            try
            {
                var existingclientType = await _repsository.GetWorkoutType(workoutTypeId);
                if (existingclientType == null) return NotFound($"The workout type does not exist");

                _repsository.Delete(existingclientType);

                if (await _repsository.SaveChangesAsync()) return Ok(existingclientType);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }

    }
}
