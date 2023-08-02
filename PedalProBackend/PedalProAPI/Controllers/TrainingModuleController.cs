using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using System.Data;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingModuleController : ControllerBase
    {
        private readonly IRepository _repsository;


        public TrainingModuleController(IRepository repository)
        {

            _repsository = repository;

        }


        [HttpGet]
        [Route("GetAllModules")]
        public async Task<IActionResult> GetAllModules()
        {
            var modules = await _repsository.GetAllTrainingModuleAsync();
            return Ok(modules);
        }

        [HttpGet]
        [Route("GetModule/{moduleId}")]
        public async Task<IActionResult> GetAllModules(int moduleId)
        {
            try
            {
                var modules = await _repsository.GetTrainingModuleAsync(moduleId);
                if (modules == null) return NotFound("Training Module does not exist");
                return Ok(modules);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddModule")]
        public async Task<IActionResult> AddModule(TrainingModuleViewModel moduleAdd)
        {
            var module = new TrainingModule
            {
                TrainingModuleName = moduleAdd.TrainingModuleName,
                TrainingModuleDescription = moduleAdd.TrainingModuleDescription,
                PackageId = moduleAdd.PackageId,
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
        public async Task<ActionResult<PedalProRoleViewModel>> EditModule(int moduleId, TrainingModuleViewModel moduleModel)
        {
            try
            {
                var existingModule = await _repsository.GetTrainingModuleAsync(moduleId);
                if (existingModule == null) return NotFound("The module does not exist");

                existingModule.TrainingModuleName = moduleModel.TrainingModuleName;
                existingModule.TrainingModuleDescription = moduleModel.TrainingModuleDescription;
                existingModule.TrainingModuleStatusId = moduleModel.TrainingModuleStatusId;
                existingModule.PackageId = moduleModel.PackageId;

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
        public async Task<IActionResult> DeleteModule(int moduleId)
        {

            try
            {

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
        public async Task<IActionResult> GetAllTrainingMaterial()
        {
            var trainingmaterials = await _repsository.GetAllTrainingMaterialAsync();
            return Ok(trainingmaterials);
        }

        [HttpGet]
        [Route("GetTrainingMaterial/{trainingMaterialId}")]
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
        public async Task<ActionResult> AddTrainingMaterial(TrainingMaterialViewModel trainingMaterialModel)
        {
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
        public async Task<ActionResult<PedalProRoleViewModel>> EditTrainingMaterial(int materialId, TrainingMaterialViewModel materialModel)
        {

            try
            {

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
        public async Task<IActionResult> DeleteTrainingMaterial(int materialId)
        {

            try
            {
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
