using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedalProRoleController : ControllerBase
    {

        private readonly IRepository _repsository;

        public PedalProRoleController(IRepository repository)
        {

            _repsository = repository;
        }


        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _repsository.GetAllRoleAsync();
            return Ok(roles);
        }



        [HttpGet]
        [Route("GetRole/{roleId}")]
        public async Task<IActionResult> GetRoleAsnyc(int roleId)
        {
            try
            {
                var result = await _repsository.GetRoleAsync(roleId);
                if (result == null) return NotFound("Course does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddRoles")]
        public async Task<IActionResult> AddRole(PedalProRoleViewModel roleAdd)
        {
            var role = new PedalProRole { RoleName = roleAdd.RoleName };

            try
            {
                _repsository.Add(role);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(role);
        }


        [HttpPut]
        [Route("EditRole/{roleId}")]
        public async Task<ActionResult<PedalProRoleViewModel>> EditRole(int roleId, PedalProRoleViewModel roleModel)
        {
            try
            {
                var existingRole = await _repsository.GetRoleAsync(roleId);
                if (existingRole == null) return NotFound("The role does not exist");

                existingRole.RoleName = roleModel.RoleName;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingRole);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }



        [HttpDelete]
        [Route("DeleteRole/{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            try
            {
                var existingRole = await _repsository.GetRoleAsync(roleId);
                if (existingRole == null) return NotFound($"The role does not exist");

                _repsository.Delete(existingRole);

                if (await _repsository.SaveChangesAsync()) return Ok(existingRole);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }

    }
}
