using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicycleCategoryController : ControllerBase
    {
        private readonly IRepository _repository;
        public BicycleCategoryController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("GetAllBicycleCategories")]
        public async Task<IActionResult> GetAllBicycleCategory()
        {
            var bicycleCategory = await _repository.GetAllBicycleCategoryAsync();
            return Ok(bicycleCategory);
        }

        [HttpGet]
        [Route("GetAllBicycleCategory/{bicycleCategoryId}")]
        public async Task<IActionResult> GetBicycleCategory(int bicycleCategoryId)
        {
            try
            {
                var result = await _repository.GetBicycleCategoryAsync(bicycleCategoryId);
                if (result == null) return NotFound("Bicycle  Category does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddBicycleCategory")]
        public async Task<IActionResult> AddBicycleCategory(BicycleCategoryViewModel cvm)
        {
            var bicycleCategory = new BicycleCategory { BicycleCategoryName = cvm.BicycleCategoryName };

            try
            {
                _repository.Add(bicycleCategory);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(bicycleCategory);
        }
        [HttpPut]
        [Route("EditBicycleCategory/{bicycleCategoryId}")]
        public async Task<ActionResult<BicycleCategoryViewModel>> EditBicycleCategory(int bicycleCategoryId, BicycleCategoryViewModel bicycleCategoryModel)
        {
            try
            {
                var existingBicycleCategory = await _repository.GetBicycleCategoryAsync(bicycleCategoryId);
                if (existingBicycleCategory == null) return NotFound($"The Bicycle category does not exist");

                existingBicycleCategory.BicycleCategoryName = bicycleCategoryModel.BicycleCategoryName;


                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingBicycleCategory);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }

        [HttpDelete]
        [Route("DeleteCategoryBicycle/{bicycleCategoryId}")]
        public async Task<IActionResult> DeleteBicycleCategory(int bicycleCategoryId)
        {
            try
            {
                var existingBicycleCategory = await _repository.GetBicycleCategoryAsync(bicycleCategoryId);
                if (existingBicycleCategory == null) return NotFound($"The Bicycle Category does not exist");

                _repository.Delete(existingBicycleCategory);

                if (await _repository.SaveChangesAsync()) return Ok(existingBicycleCategory);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }
    }
}
