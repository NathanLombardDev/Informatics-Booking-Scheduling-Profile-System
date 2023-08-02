using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpCategoryController : ControllerBase
    {
        private readonly IRepository _repsository;

        public HelpCategoryController(IRepository repository)
        {
            _repsository = repository;
        }


        [HttpGet]
        [Route("GetAllHelpCategories")]
        public async Task<IActionResult> GetAllHelpCategoriesAsync()
        {
            var categories = await _repsository.GetAllHelpCategoriesAsync();
            return Ok(categories);
        }



        [HttpGet]
        [Route("GetHelpCategory/{helpCategoryId}")]
        public async Task<IActionResult> GetHelpCategory(int helpCategoryId)
        {
            try
            {
                var result = await _repsository.GetHelpCategoryAsync(helpCategoryId);
                if (result == null) return NotFound("This category does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddHelpCategory")]
        public async Task<IActionResult> AddCategory(HelpCategoryViewModel helpCategoryAdd)
        {
            var category = new HelpCategory { HelpCategoryName = helpCategoryAdd.HelpCategoryName };

            try
            {
                _repsository.Add(category);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(category);
        }


        [HttpPut]
        [Route("EditCategory/{helpCategoryId}")]
        public async Task<ActionResult<HelpCategoryViewModel>> EditCategory(int helpCategoryId, HelpCategoryViewModel categoryModel)
        {
            try
            {
                var existingCategory = await _repsository.GetHelpCategoryAsync(helpCategoryId);
                if (existingCategory == null) return NotFound("The category does not exist");

                existingCategory.HelpCategoryName = categoryModel.HelpCategoryName;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingCategory);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }



        [HttpDelete]
        [Route("DeleteCategory/{helpCategoryId}")]
        public async Task<IActionResult> DeleteCategory(int helpCategoryId)
        {
            try
            {
                var existingCategory = await _repsository.GetHelpCategoryAsync(helpCategoryId);
                if (existingCategory == null) return NotFound($"The category does not exist");

                _repsository.Delete(existingCategory);

                if (await _repsository.SaveChangesAsync()) return Ok(existingCategory);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }
    }
}
