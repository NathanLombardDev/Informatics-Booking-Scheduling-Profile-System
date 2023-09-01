using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrainingModuleController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;


        public TrainingModuleController(IRepository repository, UserManager<PedalProUser> userManager)
        {

            _repsository = repository;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("GetAllModules")]
        [Authorize(Roles = "Client,Admin,Employee")]
        public async Task<IActionResult> GetAllModules()
        {
                // For admins and employees, retrieve and return all modules
                var modules = await _repsository.GetAllTrainingModuleAsync();
                return Ok(modules);
            

        }

        [HttpGet]
        [Route("GetModule/{moduleId}")]
        [Authorize(Roles = "Client,Admin,Employee")]
        public async Task<IActionResult> GetAllModules(int moduleId)
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

                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Contains("Client"))
                {
                    // If the user is a client, do something specific for clients

                    var client = await _repsository.GetClient(userId);

                    if (client == null)
                    {
                        return BadRequest("Client not found.");
                    }

                    var clientPackages = await _repsository.GetClientPackagesAsync(client.ClientId);

                    if (clientPackages.Any())
                    {
                        var modules = await _repsository.GetTrainingModuleAsync(moduleId);
                        if (modules == null) return NotFound("Training Module does not exist");
                        return Ok(modules);
                    }
                    else
                    {
                        // If the client hasn't purchased packages, return modules available to all clients
                        //var modules = await _repsository.GetAllTrainingModuleAsync();
                        return BadRequest("You do not have access to this without purchasing a package");
                    }
                }
                else
                {
                    var modules = await _repsository.GetTrainingModuleAsync(moduleId);
                    if (modules == null) return NotFound("Training Module does not exist");
                    return Ok(modules);
                }

                
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddModule")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddModule(TrainingModuleViewModel moduleAdd)
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

            var module = new TrainingModule
            {
                TrainingModuleName = moduleAdd.TrainingModuleName,
                TrainingModuleDescription = moduleAdd.TrainingModuleDescription,
                //PackageId = moduleAdd.PackageId,
                TrainingModuleStatusId = moduleAdd.TrainingModuleStatusId
            };

            try
            {
                _repsository.Add(module);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(module);
        }


        [HttpPut]
        [Route("EditModule/{moduleId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<PedalProRoleViewModel>> EditModule(int moduleId, TrainingModuleViewModel moduleModel)
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

                var existingModule = await _repsository.GetTrainingModuleAsync(moduleId);
                if (existingModule == null) return NotFound("The module does not exist");

                existingModule.TrainingModuleName = moduleModel.TrainingModuleName;
                existingModule.TrainingModuleDescription = moduleModel.TrainingModuleDescription;
                existingModule.TrainingModuleStatusId = moduleModel.TrainingModuleStatusId;
                //existingModule.PackageId = moduleModel.PackageId;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingModule);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }



        [HttpDelete]
        [Route("DeleteModule/{moduleId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteModule(int moduleId)
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

                var existingMaterial = await _repsository.GetTrainingMaatModAsync(moduleId);
                if (existingMaterial == null) return NotFound($"The material does not exist");



                if (existingMaterial.Any())
                {
                    foreach (var item in existingMaterial)
                    {
                        _repsository.Delete(item);
                    }
                }


                var existingModule = await _repsository.GetTrainingModuleAsync(moduleId);
                if (existingModule == null) return NotFound($"The module does not exist");

                _repsository.Delete(existingModule);

                if (await _repsository.SaveChangesAsync()) return Ok(existingModule);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }



        //Training material
        [HttpGet]
        [Route("GetAllTrainingMaterial")]
        [Authorize(Roles = "Client,Admin,Employee")]
        public async Task<IActionResult> GetAllTrainingMaterial()
        {
            var trainingmaterials = await _repsository.GetAllTrainingMaterialAsync();
            return Ok(trainingmaterials);
        }

        [HttpGet]
        [Route("GetTrainingMaterial/{trainingMaterialId}")]
        [Authorize(Roles = "Client,Admin,Employee")]
        public async Task<IActionResult> GetMaterial(int trainingMaterialId)
        {
            try
            {
                var result = await _repsository.GetTrainingMaterialAsync(trainingMaterialId);
                if (result == null) return NotFound("Training Material does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddTrainingMaterial")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult> AddTrainingMaterial(TrainingMaterialViewModel trainingMaterialModel)
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

            var video = new VideoLink
            {
                VideoUrl = trainingMaterialModel.VideoUrl,
                VideoTypeId = trainingMaterialModel.VideoTypeId
            };

            var materialTwo = new TrainingMaterial();

            try
            {
                _repsository.Add(video);
                await _repsository.SaveChangesAsync();
                var test = await _repsository.GetVideoLinkAsync(video.VideoLinkId);


                materialTwo.TrainingMaterialName = trainingMaterialModel.TrainingMaterialName;
                materialTwo.TrainingModuleId = trainingMaterialModel.TrainingModuleId;
                materialTwo.Content = trainingMaterialModel.Content;
                materialTwo.VideoLinkId = test.VideoLinkId;


                _repsository.Add(materialTwo);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }

            return Ok(materialTwo);
        }



        [HttpPut]
        [Route("EditTrainingMaterial/{materialId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<PedalProRoleViewModel>> EditTrainingMaterial(int materialId, TrainingMaterialViewModel materialModel)
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

                var existingMaterial = await _repsository.GetTrainingMaterialAsync(materialId);
                if (existingMaterial == null) return NotFound("The material does not exist");

                var existingVideo = await _repsository.GetVideoLinkAsync((int)existingMaterial.VideoLinkId);


                existingVideo.VideoUrl = materialModel.VideoUrl;
                existingVideo.VideoTypeId = materialModel.VideoTypeId;


                existingMaterial.TrainingMaterialName = materialModel.TrainingMaterialName;
                existingMaterial.Content = materialModel.Content;
                existingMaterial.VideoLinkId = existingVideo.VideoLinkId;
                existingMaterial.TrainingModuleId = materialModel.TrainingModuleId;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingMaterial);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }


        [HttpDelete]
        [Route("DeleteTrainingMaterial/{materialId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteTrainingMaterial(int materialId)
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

                var existingMaterial = await _repsository.GetTrainingMaterialAsync(materialId);
                if (existingMaterial == null) return NotFound($"The material does not exist");

                var existingVideo = await _repsository.GetTrainingMateVidAsync((int)existingMaterial.VideoLinkId);
                if (existingVideo == null) return NotFound("Video link does not exist");

                if (existingVideo.Any())
                {
                    foreach (var item in existingVideo)
                    {
                        _repsository.Delete(item);
                    }
                }

                _repsository.Delete(existingMaterial);

                if (await _repsository.SaveChangesAsync()) return Ok(existingMaterial);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }

        //Video types

        [HttpGet]
        [Route("GetAllVideoTypes")]
        public async Task<IActionResult> GetAllVideoTypes()
        {
            var videoTypes = await _repsository.GetAllVideoTypeAsync();
            return Ok(videoTypes);
        }

        //Video links
        [HttpGet]
        [Route("GetVideoLink/{linkId}")]
        public async Task<IActionResult> GetVideoLink(int linkId)
        {
            try
            {
                var result = await _repsository.GetVideoLinkAsync(linkId);
                if (result == null) return NotFound("Video link does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetAllTrainingContent/{moduleId}")]
        public async Task<IActionResult> GetAllTrainingContent(int moduleId)
        {
            var materials = await _repsository.GetTrainingMaterialsVid(moduleId);
            return Ok(materials);
        }
    }
}
