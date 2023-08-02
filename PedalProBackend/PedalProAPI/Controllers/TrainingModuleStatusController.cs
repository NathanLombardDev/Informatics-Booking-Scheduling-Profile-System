using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingModuleStatusController : ControllerBase
    {
        private readonly IRepository _repsository;

        public TrainingModuleStatusController(IRepository repository)
        {
            _repsository = repository;
        }


        [HttpGet]
        [Route("GetAllTrainingModuleStatuses")]
        public async Task<IActionResult> GetAllTrainingModuleStatusAsync()
        {
            var statuses = await _repsository.GetAllTrainingModuleStatusAsync();
            return Ok(statuses);
        }



        [HttpGet]
        [Route("GetTrainingModuleStatus/{trainingModuleStatusId}")]
        public async Task<IActionResult> GetRoleAsnyc(int trainingModuleStatusId)
        {
            try
            {
                var result = await _repsository.GetTrainingModuleStatusAsync(trainingModuleStatusId);
                if (result == null) return NotFound("This status does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddTrainingModuleStatus")]
        public async Task<IActionResult> AddStatus(TrainingModuleStatusViewModel statusAdd)
        {
            var status = new TrainingModuleStatus { TrainingModuleStatusName = statusAdd.TrainingModuleStatusName };

            try
            {
                _repsository.Add(status);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(status);
        }


        [HttpPut]
        [Route("EditStatus/{trainingModuleStatusId}")]
        public async Task<ActionResult<TrainingModuleStatusViewModel>> EditStatus(int trainingModuleStatusId, TrainingModuleStatusViewModel statusModel)
        {
            try
            {
                var existingStatus = await _repsository.GetTrainingModuleStatusAsync(trainingModuleStatusId);
                if (existingStatus == null) return NotFound("The status does not exist");

                existingStatus.TrainingModuleStatusName = statusModel.TrainingModuleStatusName;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingStatus);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }



        [HttpDelete]
        [Route("DeleteStatus/{trainingModuleStatusId}")]
        public async Task<IActionResult> DeleteRole(int trainingModuleStatusId)
        {
            try
            {
                var existingStatus = await _repsository.GetTrainingModuleStatusAsync(trainingModuleStatusId);
                if (existingStatus == null) return NotFound($"The status does not exist");

                _repsository.Delete(existingStatus);

                if (await _repsository.SaveChangesAsync()) return Ok(existingStatus);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }

    }
}
