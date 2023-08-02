using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IRepository _repsository;
        public EmployeeTypeController(IRepository repository)
        {

            _repsository = repository;
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
        public async Task<IActionResult> DeleteEmployeeType(int employeeTypeId)
        {
            try
            {
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
