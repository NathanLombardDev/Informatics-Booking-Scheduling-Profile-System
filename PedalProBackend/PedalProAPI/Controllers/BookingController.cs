using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PedalProAPI.Context;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using System.Security.Claims;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookingController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;
        private readonly PedalProDbContext _dbContext;

        public BookingController(IRepository repository, UserManager<PedalProUser> userManager, PedalProDbContext dbContext)
        {
            _userManager = userManager;
            _repsository = repository;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllBookings")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetAllBookings()
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

                var client = await _repsository.GetClient(userId);

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

                var bookingst = await _repsository.GetAllBookingAsyncTwo(client.ClientId);

                return Ok(bookingst);
            }
            catch {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            
            
            //var bookings = await _repsository.GetAllBookingAsync();
            
        }

        [HttpGet]
        [Route("GetBooking/{bookingId}")]
        public async Task<IActionResult> GetBooking(int bookingId)
        {
            try
            {
                var result = await _repsository.GetBookingAsync(bookingId);
                if (result == null) return NotFound("Booing does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddBooking")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> AddBooking(ScheduleViewModel schedule)
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

                var client = await _repsository.GetClient(userId);

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

                var booking = new Booking();

                booking.BookingStatusId = 1;


                var dateslotget = await _repsository.GetDateslotFour(schedule.timeslotId);

                var scheduleNum = await _repsository.GetScheduleSecond(dateslotget.DateSlotId);

                booking.ScheduleId = scheduleNum.ScheduleId;

                var sched = await _repsository.GetSchedule(scheduleNum.ScheduleId);

                

                booking.ClientId = client.ClientId;

                booking.BookingTypeId = schedule.bookingTypeID;

                var bookingTypeType = await _repsository.GetBookingTypetwo(schedule.bookingTypeID);

            if (bookingTypeType.BookingTypeName.Contains("Setup") ||
                bookingTypeType.BookingTypeName.Contains("set") ||
                bookingTypeType.BookingTypeName.Contains("up"))
            {
                var setup = new Setup();
                setup.SetupDescription = "";
                setup.BicycleId = 1;
                _repsository.Add(setup);
                await _repsository.SaveChangesAsync();
                sched.SetupId = setup.SetupId;
                await _repsository.SaveChangesAsync();
            }
            else if(bookingTypeType.BookingTypeName.Contains("Service") ||
                bookingTypeType.BookingTypeName.Contains("fix") ||
                bookingTypeType.BookingTypeName.Contains("Repair")) 
            {
                var service = new Service();
                service.ServiceDescription = "";
                service.BicyclePartId = 1;
                service.BicycleId = 1;
                _repsository.Add(service);
                await _repsository.SaveChangesAsync();
                sched.ServiceId = service.ServiceId;
                await _repsository.SaveChangesAsync();
            }
            else if (bookingTypeType.BookingTypeName.Contains("Training") ||
                bookingTypeType.BookingTypeName.Contains("session") ||
                bookingTypeType.BookingTypeName.Contains("train"))
            {
                var trainingSession = new TrainingSession();

                trainingSession.ClientId= client.ClientId;
                trainingSession.TrainingSessionDescription = "";

                _repsository.Add(trainingSession);
                await _repsository.SaveChangesAsync();
                sched.TrainingSessionId = trainingSession.TrainingSessionId;
                await _repsository.SaveChangesAsync();
            }

            // Asynchronous random number generation
            string referenceNum = await GetUniqueReferenceNumberAsync();

                if (string.IsNullOrEmpty(referenceNum))
                {
                    return BadRequest("Could not generate a unique reference number.");
                }

                booking.ReferenceNum = referenceNum;

                try
                {
                    _repsository.Add(booking);
                    await _repsository.SaveChangesAsync();
                    return Ok(booking);
            }
                catch (DbUpdateException ex)
                {
                    // Log the specific database exception for further analysis
                    // For example: Logger.LogError($"Database error: {ex.Message}");
                    return BadRequest("Error while saving data to the database.");
                }
                catch (Exception ex)
                {
                    // Log other unexpected exceptions for further analysis
                    // For example: Logger.LogError($"Unexpected error: {ex.Message}");
                    return BadRequest("An unexpected error occurred during the transaction.");
                }

                
            
        }

        // Asynchronously check if the reference number is already used in the database
        private async Task<bool> IsReferenceNumberUsedAsync(string referenceNumber)
        {
            bool exists = await _dbContext.Bookings.AnyAsync(b => b.ReferenceNum == referenceNumber);
            return exists;
        }

        // Asynchronously generate a unique reference number
        private async Task<string> GetUniqueReferenceNumberAsync()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            while (true)
            {
                string referenceNum = new string(Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                bool isUsed = await IsReferenceNumberUsedAsync(referenceNum);
                if (!isUsed)
                {
                    return referenceNum;
                }
            }
        }

        /*
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
        [Route("DeleteBooking/{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            try
            {
                var existingBooking = await _repsository.GetEmployeeTypeAsync(bookingId);
                if (existingBooking == null) return NotFound($"The Employee type does not exist");

                _repsository.Delete(existingBooking);

                if (await _repsository.SaveChangesAsync()) return Ok(existingBooking);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }*/


        [HttpGet]
        [Route("GetAllBookingTypes")]
        public async Task<IActionResult> GetAllBookingTypes()
        {
            var bookingtypes = await _repsository.GetBookingTypes();
            return Ok(bookingtypes);
        }


        [HttpGet]
        [Route("GetDate/{scheduleId}")]
        public async Task<IActionResult> GetDate(int scheduleId)
        {
            try
            {
                var gg=await _repsository.GetSchedule(scheduleId);

                var ss = await _repsository.getDateBooking((int)gg.DateslotId);

                var tt = await _repsository.GetDateFive((int)ss.DateId);

                var result = tt;
                if (result == null) return NotFound("This date does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }



    }
}
