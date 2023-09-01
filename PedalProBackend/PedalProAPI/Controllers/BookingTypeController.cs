using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookingTypeController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly UserManager<PedalProUser> _userManager;
        public BookingTypeController(IRepository repository, UserManager<PedalProUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("GetAllBookingTypes")]
        public async Task<IActionResult> GetAllBookingTypes()
        {
            var bookingTypes = await _repository.GetAllBookingTypeAsync();
            return Ok(bookingTypes);
        }

        [HttpGet]
        [Route("GetBookingType/{bookingTypeId}")]
        public async Task<IActionResult> GetBookingType(int bookingTypeId)
        {
            try
            {
                var result = await _repository.GetBookingTypeAsync(bookingTypeId);
                if (result == null) return NotFound("Booking Type does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddBookingTypes")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddBookingTypes(BookingTypeViewModel cvm)
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

            var userClaims = User.Claims;

            bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
            //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasAdminRole && !hasEmployeeRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }


            var bookingType = new BookingType { BookingTypeName = cvm.BookingTypeName, BookingTypePrice=cvm.Price };

            try
            {
                _repository.Add(bookingType);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(bookingType);
        }

        [HttpPut]
        [Route("EditBookingType/{bookingTypeId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<BookingTypeViewModel>> EditBookingType(int bookingTypeId, BookingTypeViewModel bookingModel)
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

                var userClaims = User.Claims;

                bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasAdminRole && !hasEmployeeRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }


                var existingbookingType = await _repository.GetBookingTypeAsync(bookingTypeId);
                if (existingbookingType == null) return NotFound($"The Booking Type does not exist");

                existingbookingType.BookingTypeName = bookingModel.BookingTypeName;
                existingbookingType.BookingTypePrice = bookingModel.Price;


                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingbookingType);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }


        [HttpDelete]
        [Route("DeleteBookingType/{bookingTypeId}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> DeleteBookingType(int bookingTypeId)
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

                var userClaims = User.Claims;

                bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasAdminRole && !hasEmployeeRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }


                var existingbookingType = await _repository.GetBookingTypeAsync(bookingTypeId);
                if (existingbookingType == null) return NotFound($"The booking type does not exist");

                _repository.Delete(existingbookingType);

                if (await _repository.SaveChangesAsync()) return Ok(existingbookingType);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }
    }
}
