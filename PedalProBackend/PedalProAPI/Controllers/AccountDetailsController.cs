using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using System.Data;
using System.Security.Claims;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountDetailsController : ControllerBase
    {
        private readonly UserManager<PedalProUser> _userManager;
        private readonly IRepository _repository;
        private readonly IUserClaimsPrincipalFactory<PedalProUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;

        public AccountDetailsController(UserManager<PedalProUser> userManager, ILogger<AuthenticationController> logger, IUserClaimsPrincipalFactory<PedalProUser> claimsPrincipalFactory, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IRepository repository)
        {
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
            _repository = repository;
            _roleManager = roleManager;
            _logger = logger;
        }


        [HttpPut]
        [Route("UpdateDetails")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> UpdateDetails(UserViewModel uvm)
        {
            try
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest("Username not found.");
                }

                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                // Update the user's email if provided
                if (!string.IsNullOrEmpty(uvm.EmailAddress))
                {
                    user.Email = uvm.EmailAddress;
                    user.UserName = uvm.EmailAddress;
                }

                // Update the user's password if provided
                if (!string.IsNullOrEmpty(uvm.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, uvm.Password);

                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }
                }

                // Update other user details if provided (e.g., first name, last name, etc.)
                // Update as needed based on your data model

                var client = await _repository.GetClient(user.Id);

                if (client == null)
                {
                    return BadRequest("Client not found.");
                }

                // Update the client's details if provided
                if (!string.IsNullOrEmpty(uvm.ClientName))
                {
                    client.ClientName = uvm.ClientName;
                }

                if (!string.IsNullOrEmpty(uvm.ClientSurname))
                {
                    client.ClientSurname = uvm.ClientSurname;
                }

                if (uvm.ClientDateOfBirth != DateTime.MinValue)
                {
                    client.ClientDateOfBirth = uvm.ClientDateOfBirth;
                }

                // Update other client details if provided
                // Update as needed based on your data model

                // Save the changes to the database
                await _repository.SaveChangesAsync();

                return Ok(client);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred during user details update.");

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error. Please contact support.");
            }
        }
    }
}
