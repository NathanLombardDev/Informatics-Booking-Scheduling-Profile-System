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
    public class ScheduleController : ControllerBase
    {
        private readonly IRepository _repsository;
        private readonly PedalProDbContext _context;
        private readonly UserManager<PedalProUser> _userManager;

        public ScheduleController(IRepository repsository, PedalProDbContext context, UserManager<PedalProUser> userManager)
        {
            _repsository = repsository;
            _context = context;
            _userManager = userManager;
        }
        
        
        
        [HttpGet]
        [Route("GetDateSlots/{id}")]
        public async Task<ActionResult<IEnumerable<DateSlot>>> GetDateSlots(int id)
        {
            var dateSlots = await _context.DateSlots
                .Include(ds => ds.Timeslot)
                .Where(ds => ds.DateId == id)
                .ToListAsync();

            return dateSlots;
        }
        
        
        
        
        
        [HttpGet]
        [Route("GetTimeslotstwo/{dateId}")]

        public async Task<ActionResult<IEnumerable<Timeslot>>> GetTimeslotstwo(int dateId)
        {
            var dateSlots = await _context.DateSlots
                .Where(s => s.DateId == dateId)
                .ToListAsync();

            var timeslotIds = dateSlots.Select(ds => ds.TimeslotId).ToList();

            var timeslots = await _context.Timeslots
                .Where(t => timeslotIds.Contains(t.TimeslotId))
                .ToListAsync();

            return Ok(timeslots);
        }


        [HttpGet]
        [Route("GetTimeslotsthree/{date}")]
        [Authorize(Roles = "Client,Employee,Admin")]
        public async Task<ActionResult<IEnumerable<Timeslot>>> GetTimeslotsthree(DateTime date)
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
            bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasAdminRole && !hasEmployeeRole &&!hasClientRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }

            var dateTwo = await _context.Dates.Where(s => s.Date1 == date).ToListAsync();

            var dateTwoID=dateTwo.Select(hy=>hy.DateId).ToList();

            var dateSlots = await _context.DateSlots
                .Where(s => dateTwoID.Contains((int)s.DateId))
                .ToListAsync();

            var timeslotIds = dateSlots.Select(ds => ds.TimeslotId).ToList();

            var timeslots = await _context.Timeslots
                .Where(t => timeslotIds.Contains(t.TimeslotId) && t.TimeslotStatusId == 1)
                .ToListAsync();

            return Ok(timeslots);
        }


        [HttpPost]
        [Route("AddTimeslot")]
        public async Task<IActionResult> AddTimeslot(DateWithTimeslotDto dateWithTimeslotDto)
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
            //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
            //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasAdminRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }

            var existingDate = await _context.Dates.FirstOrDefaultAsync(d => d.Date1 == dateWithTimeslotDto.date);

            Date date;
            if (existingDate != null)
            {
                date = existingDate;
            }
            else
            {
                date = new Date
                {
                    Date1 = dateWithTimeslotDto.date
                };
                _repsository.Add(date);
                await _repsository.SaveChangesAsync();
            }

            var timeslot = new Timeslot
            {
                StartTime = dateWithTimeslotDto.StartTime,
                EndTime = dateWithTimeslotDto.EndTime,
                TimeslotStatusId=1
            };
            _repsository.Add(timeslot);
            await _repsository.SaveChangesAsync();

            var dateSlot = new DateSlot
            {
                Date = date,
                Timeslot = timeslot
            };
            _context.DateSlots.Add(dateSlot);
            await _repsository.SaveChangesAsync();


            var schedule = new Schedule
            {
                DateslotId = dateSlot.DateSlotId,
                EmployeeId = dateWithTimeslotDto.EmployeeId
                
            };
            _repsository.Add(schedule);
            await _repsository.SaveChangesAsync();


            return CreatedAtAction("GetDates", new { id = date.DateId }, date);

        }

        [HttpGet]
        [Route("GetDates")]
        public async Task<ActionResult<IEnumerable<Date>>> GetDates()
        {
            var dates = await _context.Dates.ToListAsync();

            return Ok(dates);
        }

        [HttpGet]
        [Route("Gettimeslots")]
        public async Task<ActionResult<IEnumerable<Timeslot>>> Gettimeslots()
        {
            var dates = await _context.Timeslots.ToListAsync();

            return Ok(dates);
        }

        [HttpPost]
        [Route("CreateDates")]
        public async Task<IActionResult> CreateDates()
        {
            var currentDate = DateTime.Today;
            var endDate = currentDate.AddMonths(12);

            var dates = new List<Date>();

            while (currentDate <= endDate)
            {
                dates.Add(new Date { Date1 = currentDate });
                currentDate = currentDate.AddDays(1);
            }

            _context.Dates.AddRange(dates);
            await _context.SaveChangesAsync();

            return Ok(dates);
        }


        [HttpDelete]
        [Route("DeleteTimeSlot/{timeslotId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTimeSlot(int timeslotId)
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
                //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasAdminRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }

                var existingWorkout = await _repsository.GetTimeslotAsync(timeslotId);
                if (existingWorkout == null) return NotFound($"The tiemslots does not exist");


                var existingTimeslot = await _repsository.GetDateslotFour(timeslotId);
                if (existingTimeslot == null) return NotFound($"The dateslot does not exist");

                var schedule=await _repsository.GetScheduleSecond(existingTimeslot.DateSlotId);

                _repsository.Delete(schedule);
                _repsository.Delete(existingTimeslot);
                _repsository.Delete(existingWorkout);

                if (await _repsository.SaveChangesAsync()) return Ok(existingWorkout);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }

        [HttpPut]
        [Route("UpdateTimeslot/{timeslotId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTimeslot(int timeslotId, DateWithTimeslotDto dateWithTimeslotDto)
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
            //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
            //bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

            if (!hasAdminRole)
            {
                return Forbid("You do not have the necessary role to perform this action.");
            }

            var existingDate = await _context.Dates.FirstOrDefaultAsync(d => d.Date1 == dateWithTimeslotDto.date);

            Date date;
            if (existingDate != null)
            {
                date = existingDate;
            }
            else
            {
                date = new Date
                {
                    Date1 = dateWithTimeslotDto.date
                };
                _repsository.Add(date);
                await _repsository.SaveChangesAsync();
            }

            var timeslot = await _context.Timeslots.FindAsync(timeslotId);

            // If the timeslot does not exist, return NotFound
            if (timeslot == null)
            {
                return NotFound();
            }

            timeslot.StartTime = dateWithTimeslotDto.StartTime;
            timeslot.EndTime = dateWithTimeslotDto.EndTime;
            _context.Entry(timeslot).State = EntityState.Modified;
            await _repsository.SaveChangesAsync();

            var existingdateslot = await _context.DateSlots.FindAsync(timeslot.TimeslotId);

            existingdateslot.TimeslotId = timeslot.TimeslotId;
            existingdateslot.DateId = date.DateId;

            await _repsository.SaveChangesAsync();

            var schedule = new Schedule
            {
                DateslotId = existingdateslot.DateSlotId,
                EmployeeId = dateWithTimeslotDto.EmployeeId
            };
            _repsository.Add(schedule);
            await _repsository.SaveChangesAsync();

            return CreatedAtAction("GetDates", new { id = date.DateId }, date);
        }


        [HttpGet]
        [Route("GetTimeSlot/{timeslotId}")]
        public async Task<IActionResult> GetTimeSlot(int timeslotId)
        {
            try
            {
                var result = await _repsository.GetTimeslotAsync(timeslotId);
                if (result == null) return NotFound("Timeslot does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

    }
}
