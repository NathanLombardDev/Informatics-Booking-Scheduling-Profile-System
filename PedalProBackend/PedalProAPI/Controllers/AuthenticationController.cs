using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PedalProAPI.Context;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Vonage.Users;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<PedalProUser> _userManager;
        private readonly IRepository _repository;
        private readonly IUserClaimsPrincipalFactory<PedalProUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(UserManager<PedalProUser> userManager, ILogger<AuthenticationController> logger, IUserClaimsPrincipalFactory<PedalProUser> claimsPrincipalFactory, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IRepository repository)
        {
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
            _repository = repository;
            _roleManager = roleManager;
            _logger = logger;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserViewModel uvm)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(uvm.EmailAddress);

                if (user == null)
                {
                    user = new PedalProUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = uvm.EmailAddress,
                        Email = uvm.EmailAddress
                    };

                    var result = await _userManager.CreateAsync(user, uvm.Password);

                    if (result.Succeeded)
                    {
                        // Check if the role "Client" exists
                        var clientRoleExists = await _roleManager.RoleExistsAsync("Client");

                        if (!clientRoleExists)
                        {
                            // Create the role "Client"
                            var clientRole = new IdentityRole("Client");
                            await _roleManager.CreateAsync(clientRole);
                        }

                        // Assign role to the user
                        await _userManager.AddToRoleAsync(user, "Client");  
                    }
                    else
                    {
                        // Return a BadRequest response with the validation errors
                        return BadRequest(result.Errors);
                    }
                }
                else
                {
                    return BadRequest("Account already exists.");
                }
                var client = new Client
                {
                    UserId = user.Id,
                    ClientEmailAddress = uvm.EmailAddress,
                    ClientName = uvm.ClientName,
                    ClientSurname = uvm.ClientSurname,
                    ClientTypeId = 1,
                    ClientDateOfBirth = uvm.ClientDateOfBirth,
                    ClientPhoneNum = uvm.ClientPhoneNum,
                    ClientPhysicalAddress = uvm.ClientPhysicalAddress,
                    ClientProfilePicture = null,
                    ClientTitle = uvm.ClientTitle,
                    IsActive = true,
                    NumBookingsAllowance=0
                    // Set other properties as needed
                };

                // Add the client to the database
                _repository.Add(client);
                await _repository.SaveChangesAsync();

                return Ok(client);
            }

            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred during user registration.");

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");
            }
        }





        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginViewModel uvm)
        {
            var user = await _userManager.FindByNameAsync(uvm.EmailAddress);

           

            if (user != null && await _userManager.CheckPasswordAsync(user, uvm.Password))
            {
                try
                {

                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Client"))
                    {
                        var client = await _repository.GetClient(user.Id);
                        if (client.IsActive == false)
                        {
                            return BadRequest("Your account has been deactivated.");
                        }
                        else
                        {
                            var token = GenerateJWTToken(user, roles);
                            return Ok(token);
                        }

                    }
                    else
                    {
                        var token = GenerateJWTToken(user, roles);
                        return Ok(token);
                    }

                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");
                }
            }
            else
            {
                return NotFound("User does not exist or incorrect password.");
            }
        }        
        
        private ActionResult GenerateJWTToken(PedalProUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: credentials
            );

            return Created("", new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                user = user.UserName
            });
        }
        
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string emailAddress)
        {
          
            var user = await _userManager.FindByEmailAsync(emailAddress);
            if (user == null)
            {
                return NotFound("Email address not found.");
            }

            // Generate a password reset code (token)
            var resetCode = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Prepare the email content
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("nathantheawsome1234@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailAddress));
            email.Subject = "Password Reset Request for PedalPro";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<h1>Dear {user.UserName},</h1>" +
                       $"<p>You have requested to reset your password for the PedalPro system.</p>" +
                       $"<p>Enter the following code on the reset password page to proceed:</p>" +
                       $"<h2>{resetCode}</h2>"
            };


            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("nathantheawsome1234@gmail.com", "fanmgdiiigkpjnsc");
            smtp.Send(email);
            smtp.Disconnect(true);

            

            return Ok(new {message= "Password reset code sent successfully. Please check your email for the reset code." });
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound(new { message = "Email address not found." });
            }


            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Password reset successful. You can now log in with the new password." });
            }
            else
            {
                var errors = result.Errors.Select(error => error.Description);
                return BadRequest(new { errors });
            }
        }

        [HttpPut]
        [Route("ReactivateAccount")]
        public async Task<IActionResult> ReactivateAccount(LoginViewModel uvm)
        {
            var user = await _userManager.FindByNameAsync(uvm.EmailAddress);

            try
            {

                if (user != null && await _userManager.CheckPasswordAsync(user, uvm.Password))
                {

                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("Client"))
                    {
                        var client = await _repository.GetClient(user.Id);
                        if (client.IsActive == false)
                        {
                            client.IsActive = true;
                            await _repository.SaveChangesAsync();

                           
                            var message = "Your account has been reactivated.";
                            var responseObject = new { Message = message };
                            return Ok(responseObject);
                        }
                        else
                        {
                            var message = "Your account is already active";
                            var responseObject = new { Message = message };
                            return Ok(responseObject);
                        }

                    }
                    else
                    {
                        return BadRequest("You have not been registered as a client");
                    }
                }
                else
                {
                    return NotFound("User does not exist or incorrect password.");
                }


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin(UserViewModel uvm)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(uvm.EmailAddress);

                if (user == null)
                {
                    user = new PedalProUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = uvm.EmailAddress,
                        Email = uvm.EmailAddress
                    };

                    var result = await _userManager.CreateAsync(user, uvm.Password);

                    if (result.Succeeded)
                    {
                        // Check if the role "Client" exists
                        var clientRoleExists = await _roleManager.RoleExistsAsync("Admin");

                        if (!clientRoleExists)
                        {
                            // Create the role "Client"
                            var clientRole = new IdentityRole("Admin");
                            await _roleManager.CreateAsync(clientRole);
                        }

                        // Assign role to the user
                        await _userManager.AddToRoleAsync(user, "Admin");


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
                var admin = new Administrator
                {
                    UserId = user.Id,
                    AdminEmail = uvm.EmailAddress,
                    AdminName = uvm.ClientName,
                    AdminSurname = uvm.ClientSurname,
                    AdminPhoneNum = uvm.ClientPhoneNum,
                    Title = uvm.ClientTitle,
                    // Set other properties as needed
                };

                // Add the client to the database
                _repository.Add(admin);
                await _repository.SaveChangesAsync();

                return Ok(admin);
            }

            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred during user registration.");

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");
            }
        }
    }
}