using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IRepository _repsository;

        public HelpController(IRepository repository)
        {
            _repsository = repository;
        }

        [HttpGet]
        [Route("GetAllHelp")]
        public async Task<IActionResult> GetAllHelp()
        {
            var helps = await _repsository.GetAllHelpAsync();
            return Ok(helps);
        }

        [HttpGet]
        [Route("GetHelp/{helpId}")]
        public async Task<IActionResult> GetHelp(int helpId)
        {
            try
            {
                var result = await _repsository.GetHelpAsync(helpId);
                if (result == null) return NotFound("Help does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddHelp")]
        public async Task<IActionResult> AddHelp(HelpViewModel helpAdd)
        {
            var help = new Help
            {
                HelpName = helpAdd.HelpName,
                HelpDescription = helpAdd.HelpDescription,
                HelpCategoryId = helpAdd.HelpCategoryId
            };
            try
            {
                _repsository.Add(help);
                await _repsository.SaveChangesAsync();

            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(help);
        }

        [HttpPut]
        [Route("EditHelp/{helpId}")]
        public async Task<ActionResult<HelpViewModel>> UpdateHelp(int helpId, HelpViewModel helpModel)
        {
            try
            {
                var existingHelp = await _repsository.GetHelpAsync(helpId);
                if (existingHelp == null) return NotFound("The Help does not exist");

                existingHelp.HelpName = helpModel.HelpName;
                existingHelp.HelpDescription = helpModel.HelpDescription;
                existingHelp.HelpCategoryId = helpModel.HelpCategoryId;




                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingHelp);
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }

        [HttpDelete]
        [Route("DeleteHelp/{helpId}")]
        public async Task<IActionResult> DeleteHelp(int helpId)
        {
            try
            {
                var existingHelp = await _repsository.GetHelpAsync(helpId);
                if (existingHelp == null) return NotFound($"The help does not exist");


                _repsository.Delete(existingHelp);

                if (await _repsository.SaveChangesAsync()) return Ok(existingHelp);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }


        [HttpGet]
        [Route("GetAllHelpCategories")]
        public async Task<IActionResult> GetAllHelpCategories()
        {
            var helpCategories = await _repsository.GetAllHelpCategoriesAsync();
            return Ok(helpCategories);
        }

        [HttpGet]
        [Route("GetHelpCategory/{helpCategoryId}")]
        public async Task<IActionResult> GetHelpCategory(int helpCategoryId)
        {
            try
            {
                var result = await _repsository.GetHelpCategoryAsync(helpCategoryId);
                if (result == null) return NotFound("Category does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
    }
}
