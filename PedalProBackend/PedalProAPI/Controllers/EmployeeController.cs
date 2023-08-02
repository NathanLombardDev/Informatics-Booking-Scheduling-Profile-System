using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository _repsository;

        private readonly UserManager<PedalProUser> _userManager;
        private readonly IRepository _repository;
        private readonly IUserClaimsPrincipalFactory<PedalProUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;

        public EmployeeController(IRepository repository, UserManager<PedalProUser> userManager, ILogger<AuthenticationController> logger, IUserClaimsPrincipalFactory<PedalProUser> claimsPrincipalFactory, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IRepository repsository)
        {
            _repsository = repository;
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
            _repository = repsository;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employees = await _repsository.GetAllEmployeeAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("GetEmployee/{employeeId}")]
        public async Task<IActionResult> GetEmployee(int employeeId)
        {
            try
            {
                var result = await _repsository.GetEmployeeAsync(employeeId);
                if (result == null) return NotFound("Employee does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeAdd)
        {
            try
            {

                var userTwo = await _userManager.FindByEmailAsync(employeeAdd.EmailAddress);

                if (userTwo == null)
                {
                    userTwo = new PedalProUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = employeeAdd.EmailAddress,
                        Email = employeeAdd.EmailAddress
                    };
                    var result = await _userManager.CreateAsync(userTwo, employeeAdd.Password);
                    if (result.Succeeded)
                    {
                        // Check if the role "Client" exists
                        var empRoleExists = await _roleManager.RoleExistsAsync("Employee");

                        if (!empRoleExists)
                        {
                            // Create the role "Client"
                            var empRole = new IdentityRole("Employee");
                            await _roleManager.CreateAsync(empRole);
                        }

                        // Assign role to the user
                        await _userManager.AddToRoleAsync(userTwo, "Employee");


                    }
                    else
                    {
                        // Return a BadRequest response with the validation errors
                        return BadRequest(result.Errors);
                    }
                }
                else
                {
                    return Forbid("Account already exists.");
                }

                var employee = new Employee
                {
                    UserId=userTwo.Id,
                    EmpTitle = employeeAdd.EmpTitle,
                    EmpName = employeeAdd.EmpName,
                    EmpSurname = employeeAdd.EmpSurname,
                    EmpPhoneNum = employeeAdd.EmpPhoneNum,
                    EmpStatusId = employeeAdd.EmpStatusId,
                    EmpTypeId = employeeAdd.EmpTypeId,
                    EmpEmailAddress = userTwo.Email
                };

                    _repsository.Add(employee);
                    await _repsository.SaveChangesAsync();

                    var email = new MimeMessage();
                    email.From.Add(MailboxAddress.Parse("nathantheawsome1234@gmail.com"));
                    email.To.Add(MailboxAddress.Parse(employee.EmpEmailAddress));
                    email.Subject = "PedalPro: New employee log in details";
                    email.Body = new TextPart(TextFormat.Html)
                    {
                        Text = "<h1>Dear " + employee.EmpTitle + " " + employee.EmpSurname + "<h1/> <p>Your new details to access the PedalPro system are as follows: <br/> Email: " + employee.EmpEmailAddress + " <br/>Username:" + userTwo.UserName + " <br/> Password: " + employeeAdd.Password + "</p>"
                    };

                    using var smtp = new SmtpClient();
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("nathantheawsome1234@gmail.com", "fanmgdiiigkpjnsc");
                    smtp.Send(email);
                    smtp.Disconnect(true);

                return Ok(employee);

            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            
        }





        [HttpPut]
        [Route("EditEmployee/{employeeId}")]
        public async Task<ActionResult<EmployeeViewModel>> UpdateEmployee(int employeeId, EmployeeViewModelTwo employeeModel)
        {
            try
            {
                var userTwo = await _userManager.FindByEmailAsync(employeeModel.EmailAddress);

                


                var existingEmployee = await _repsository.GetEmployeeAsync(employeeId);
                if (existingEmployee == null) return NotFound("The Employee does not exist");

                existingEmployee.EmpTitle = employeeModel.EmpTitle;
                existingEmployee.EmpName = employeeModel.EmpName;
                existingEmployee.EmpSurname = employeeModel.EmpSurname;
                existingEmployee.EmpPhoneNum = employeeModel.EmpPhoneNum;
                existingEmployee.EmpEmailAddress = employeeModel.EmailAddress;
                existingEmployee.EmpStatusId = employeeModel.EmpStatusId;
                existingEmployee.EmpTypeId = employeeModel.EmpTypeId;

                
                var existUser=await _repository.GetUserFromEmp(existingEmployee.UserId);
                if (existUser == null) return NotFound("The User does not exist");

                /*
                var existingUser = await _userManager.FindByEmailAsync(employeeModel.EmailAddress);
                if (existingUser == null) return NotFound("The User does not exist");
                */


                existUser.UserName = employeeModel.EmailAddress;
                //existingUser.PasswordHash = employeeModel.Password;
                existUser.Email = employeeModel.EmailAddress;
                //existingUser.RoleId = employeeModel.RoleId;


                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("nathantheawsome1234@gmail.com"));
                email.To.Add(MailboxAddress.Parse(existingEmployee.EmpEmailAddress));
                email.Subject = "PedalPro: Updated employee log in details";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = "<h1>Dear " + existingEmployee.EmpTitle + " " + existingEmployee.EmpSurname + "<h1/> <p>Your updated details to access the PedalPro system are as follows: <br/> Email: " + existingEmployee.EmpEmailAddress+ " <br/>Username:" + employeeModel.EmailAddress + " <br/> Password:Your original password please " + "</p>"
                };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("nathantheawsome1234@gmail.com", "fanmgdiiigkpjnsc");
                smtp.Send(email);
                smtp.Disconnect(true);


                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingEmployee);
                }

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }

        [HttpDelete]
        [Route("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            try
            {
                var existingEmployee = await _repsository.GetEmployeeAsync(employeeId);
                if (existingEmployee == null) return NotFound($"The employee does not exist");

                var existingUser = await _repsository.GetUserAsync(existingEmployee.UserId.ToString());
                if (existingUser != null)
                {
                    _repsository.Delete(existingUser);
                }

                _repsository.Delete(existingEmployee);

                if (await _repsository.SaveChangesAsync()) return Ok(existingEmployee);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }


        [HttpGet]
        [Route("GetAllEmployeeStatuses")]
        public async Task<IActionResult> GetAllEmployeeStatus()
        {
            var employeeStatuses = await _repsository.GetAllEmployeeStatusAsync();
            return Ok(employeeStatuses);
        }

        [HttpGet]
        [Route("GetEmployeeStatus/{employeeStatusId}")]
        public async Task<IActionResult> GetEmployeeStatus(int employeeStatusId)
        {
            try
            {
                var result = await _repsository.GetEmployeeStatusAsync(employeeStatusId);
                if (result == null) return NotFound("Employee status does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


    }
}
