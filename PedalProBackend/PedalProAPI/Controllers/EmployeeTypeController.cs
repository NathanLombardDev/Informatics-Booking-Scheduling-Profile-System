using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;
        public EmployeeTypeController(IRepository repository, UserManager<PedalProUser> userManager)
        {

            _repsository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("GetAllEmployeeTypes")]
        public async Task<IActionResult> GetAllEmployeeTypes()
        {
            var employeeTypes = await _repsository.GetAllEmployeeTypeAsync();
            return Ok(employeeTypes);
        }

        [HttpGet]
        [Route("GetEmployeeType/{employeeTypeId}")]
        public async Task<IActionResult> GetEmployeeType(int employeeTypeId)
        {
            try
            {
                var result = await _repsository.GetEmployeeTypeAsync(employeeTypeId);
                if (result == null) return NotFound("Employee Type does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddEmployeetypes")]
        
        public async Task<IActionResult> AddRole(EmployeeTypeViewModel empTypeAdd)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username not found.");
            }

            var user = _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var userId = user.Id;

            var userClaims = User.Claims;

            bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
            //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasAdminRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }

            var empType = new EmployeeType { EmpTypeName = empTypeAdd.EmpTypeName, EmpTypeDescription = empTypeAdd.EmpTypeDescription };

            try
            {
                _repsository.Add(empType);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(empType);
        }


        [HttpPut]
        [Route("EditEmployeeType/{employeeTypeId}")]
       
        public async Task<ActionResult<EmployeeTypeViewModel>> UpdateEmployeeType(int employeeTypeId, EmployeeTypeViewModel employeeTypeModel)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username not found.");
                }

                var user = _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var userId = user.Id;

                var userClaims = User.Claims;

                bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasAdminRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }

                var existingType = await _repsository.GetEmployeeTypeAsync(employeeTypeId);
                if (existingType == null) return NotFound("The Employee type does not exist");

                existingType.EmpTypeName = employeeTypeModel.EmpTypeName;
                existingType.EmpTypeDescription = employeeTypeModel.EmpTypeDescription;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingType);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }

        [HttpDelete]
        [Route("DeleteEmployeeType/{employeeTypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployeeType(int employeeTypeId)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username not found.");
                }

                var user = _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                var userId = user.Id;

                var userClaims = User.Claims;

                bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasAdminRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }

                var existingType = await _repsository.GetEmployeeTypeAsync(employeeTypeId);
                if (existingType == null) return NotFound($"The Employee type does not exist");

                _repsository.Delete(existingType);

                if (await _repsository.SaveChangesAsync()) return Ok(existingType);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }
    }
}
