using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
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
using System;

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

                if (!string.IsNullOrEmpty(uvm.ClientPhoneNum))
                {
                    client.ClientPhoneNum = uvm.ClientPhoneNum;
                }
                if (!string.IsNullOrEmpty(uvm.ClientPhysicalAddress))
                {
                    client.ClientPhysicalAddress = uvm.ClientPhysicalAddress;
                }
                if (!string.IsNullOrEmpty(uvm.ClientTitle))
                {
                    client.ClientTitle = uvm.ClientTitle;
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

        [HttpPut]
        [Route("DeactivateMyAccount")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> DeactivateMyAccount()
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

            var userId = user.Id;

            var client = await _repository.GetClient(userId);

            if (client == null)
            {
                return BadRequest("Client not found.");
            }

            var userClaims = User.Claims;
            bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasClientRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }


            if (user == null)
                return NotFound();

            client.IsActive = false;
            await _repository.SaveChangesAsync();

            var message = "Your account has been deactivated.";
            var responseObject = new { Message = message };
            /*
            user.IsActive = false;
            _dbContext.SaveChanges();*/

            return Ok(responseObject);
        }

        [HttpPut]
        [Route("UploadProfileImage")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
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

                var userId = user.Id;

                var client = await _repository.GetClient(userId);

                if (client == null)
                {
                    return BadRequest("Client not found.");
                }

                var userClaims = User.Claims;
                bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasClientRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }

                client.ClientProfilePicture = null;
                await _repository.SaveChangesAsync();

                if (file != null && file.Length > 0)
                {
                    // Configure your Cloudinary account
                    Account account = new Account(
                        "dcpmharuk",
                        "183493828529672",
                        "869tkBTJoV1UmiO0ubhatZ5rNSs"
                    );

                    Cloudinary cloudinary = new Cloudinary(account);

                    // Upload the image to Cloudinary
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream())
                    };

                    var uploadResult = await cloudinary.UploadAsync(uploadParams);

                    var url = uploadResult.Url.AbsoluteUri;

                    client.ClientProfilePicture = url;

                    await _repository.SaveChangesAsync();

                    return Ok(uploadResult);
                }

                return BadRequest("No image uploaded.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
    }
}
