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
using PedalProAPI.Other_Models;
using System.Linq;
using System;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReportController : ControllerBase
    {


        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;
        private readonly PedalProDbContext _context;

        public ReportController(IRepository repository, UserManager<PedalProUser> userManager, PedalProDbContext context)
        {
            _repsository = repository;
            _userManager = userManager;
            _context = context;
        }


        [HttpGet("generate")]
        public async Task<IActionResult> GenerateWorkoutReport(string timeInterval)
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




                var startDate = CalculateStartDateBasedOnInterval(timeInterval);
                var endDate = DateTime.Now;

                var workoutData = await _repsository.GetWorkoutDataBetweenDates(startDate, endDate, client.ClientId);

                var totalDuration = TimeSpan.Zero;
                var totalHeartRate = 0;
                var totalDistance = 0.00;
                foreach (var workout in workoutData)
                {
                    totalDuration += workout.Duration ?? TimeSpan.Zero;
                    totalHeartRate += workout.HeartRate ?? 0;
                    totalDistance += workout.Distance ?? 0;
                }
                var averageDuration = totalDuration / workoutData.Count;
                var averageDistance = totalDistance / workoutData.Count;
                var averageHeartRate = totalHeartRate / workoutData.Count;
                // Calculate other aggregate values based on your requirements

                var clientname = client.ClientName + " " + client.ClientSurname;
                var dategenerated = DateTime.Now;

                var reportData = new
                {
                    WorkoutData = workoutData,
                    TotalDuration = totalDuration,
                    AverageDuration = averageDuration,
                    AverageHeartRate = averageHeartRate,
                    TotalDistance = totalDistance,
                    AverageDistance = averageDistance,
                    generateddate = dategenerated,
                    generateby = clientname
                    // Add other aggregate values here
                };

                return Ok(reportData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        private DateTime CalculateStartDateBasedOnInterval(string timeInterval)
        {
            var currentDate = DateTime.Now;

            switch (timeInterval)
            {
                case "last_month":
                    return currentDate.AddMonths(-1);
                case "last_three_months":
                    return currentDate.AddMonths(-3);
                case "last_six_months":
                    return currentDate.AddMonths(-6);
                default:
                    // Default to last month if interval is not recognized
                    return currentDate.AddMonths(-1);
            }
        }
        

        [HttpGet("GeneratePackageReport")]
        public async Task<IActionResult> GeneratePackageReport()
        {
            var packagerevenues = await _repsository.GetAllPackageRevenuesAsync();


            var reportDataList = new List<object>();

            var TotalTotal = 0.00;

            foreach (var rev in packagerevenues)
            {
                var package = await _repsository.GetPackageName(rev.PackageName);

                var packageprices = await _repsository.GetPackageAssocAsync(package.PackageId);

                var price = await _repsository.GetPriceAsync((int)packageprices.PriceId);

                var PackageData = new
                {
                    Price = price.Price1,
                    TotalRevenue = (price.Price1*rev.Quantity),
                    packageName = package.PackageName,
                    quantity=rev.Quantity
                };

                TotalTotal += ((double)price.Price1 * rev.Quantity);

                // Add the reportData object to the list
                reportDataList.Add(PackageData);
            }

            var dategenerated=DateTime.Now;

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

            var employeename = "";

            var employee = await _repsository.GetEmployee(userId);

            if (employee != null)
            {
                employeename = employee.EmpName + " " + employee.EmpSurname;
            }
            else
            {
                var admin = await _repsository.GetAdmin(userId);
                employeename = admin.AdminName + " " + admin.AdminSurname;
            }

            //var employeename = employee.EmpName +" "+ employee.EmpSurname;
;
            var reportdata = new
            {
                reportDataList,
                total = TotalTotal,
                generateddate= dategenerated,
                generateby= employeename
            };
            


            return Ok(reportdata);
        }


        [HttpGet("GenerateRevenueReport")]
        public async Task<IActionResult> GenerateRevenueReport()
        {
            var packagerevenues = await _repsository.GetAllPackageRevenuesAsync();


            var reportDataList = new List<object>();

            var TotalTotal = 0.00;

            var bookingtotaltotal = 0.00;

            foreach (var rev in packagerevenues)
            {
                var package = await _repsository.GetPackageName(rev.PackageName);

                var packageprices = await _repsository.GetPackageAssocAsync(package.PackageId);

                var price = await _repsository.GetPriceAsync((int)packageprices.PriceId);

                var PackageData = new
                {
                    Price = price.Price1,
                    TotalRevenue = (price.Price1 * rev.Quantity),
                    packageName = package.PackageName,
                    quantity = rev.Quantity
                };

                TotalTotal += ((double)price.Price1 * rev.Quantity);

                // Add the reportData object to the list
                reportDataList.Add(PackageData);
            }


            var bookingrevenues=await _repsository.GetAllBookingrevenue();

            var reportbookinglist=new List<object>();

            foreach(var nin in bookingrevenues)
            {
                var bookingType = await _repsository.GetBookingTypeName(nin.BookingType);

                var BookingData = new
                {
                    Price=bookingType.BookingTypePrice,
                    quantity=nin.Quantity,
                    TotalRevenue=((nin.Quantity)*(bookingType.BookingTypePrice)),
                    Bookingtype= bookingType.BookingTypeName
                };

                bookingtotaltotal += ((nin.Quantity) * ((double)bookingType.BookingTypePrice));

                reportbookinglist.Add(BookingData);

            }

            var overallTotal = TotalTotal + bookingtotaltotal;

            var dategenerated = DateTime.Now;

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

            var employee = await _repsository.GetEmployee(userId);

            var employeename = "";

           

            if (employee != null)
            {
                employeename = employee.EmpName + " " + employee.EmpSurname;
            }
            else
            {
                var admin = await _repsository.GetAdmin(userId);
                employeename = admin.AdminName + " " + admin.AdminSurname;
            }



            var reportdata = new
            {
                reportDataList,
                reportbookinglist,
                total = TotalTotal,
                bookingtotal=bookingtotaltotal,
                overall=overallTotal,
                generateddate = dategenerated,
                generateby = employeename
            };



            return Ok(reportdata);
        }


        [HttpGet("GeneratePopularDaysReport")]
        public async Task<IActionResult> GeneratePopularDaysReport()
        {

            var bookings = await _repsository.GetAllBookingAsync();

            // Create a dictionary to store the counters for each day of the week
            Dictionary<DayOfWeek, int> dayCounter = new Dictionary<DayOfWeek, int>
{
            { DayOfWeek.Sunday, 0 },
            { DayOfWeek.Monday, 0 },
            { DayOfWeek.Tuesday, 0 },
            { DayOfWeek.Wednesday, 0 },
            { DayOfWeek.Thursday, 0 },
            { DayOfWeek.Friday, 0 },
            { DayOfWeek.Saturday, 0 }
};

            foreach (var hello in bookings)
            {
                var schedule = await _repsository.GetSchedule((int)hello.ScheduleId);
                var dateslot = await _repsository.getDateBooking((int)schedule.DateslotId);
                var date = await _repsository.GetDateFive((int)dateslot.DateId);

                DateTime datedate = (DateTime)date.Date1;
                DayOfWeek dayOfWeek = datedate.DayOfWeek;

                // Increment the counter for the specific day of the week
                dayCounter[dayOfWeek]++;
            }


            var dategenerated = DateTime.Now;

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

            var employee = await _repsository.GetEmployee(userId);

            var employeename = "";

            

            if (employee != null)
            {
                employeename = employee.EmpName + " " + employee.EmpSurname;
            }
            else
            {
                var admin = await _repsository.GetAdmin(userId);
                employeename = admin.AdminName + " " + admin.AdminSurname;
            }


            // Create an object to hold the day-wise count data
            var reportData = new
            {
                Sunday = dayCounter[DayOfWeek.Sunday],
                Monday = dayCounter[DayOfWeek.Monday],
                Tuesday = dayCounter[DayOfWeek.Tuesday],
                Wednesday = dayCounter[DayOfWeek.Wednesday],
                Thursday = dayCounter[DayOfWeek.Thursday],
                Friday = dayCounter[DayOfWeek.Friday],
                Saturday = dayCounter[DayOfWeek.Saturday],
                generateddate = dategenerated,
                generateby = employeename
            };

            return Ok(reportData);
        }

        [HttpGet("GenerateClientListReport")]
        public async Task<IActionResult> GenerateClientListReport()
        {
            var dategenerated = DateTime.Now;

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

            var employeename = "";

            var employee = await _repsository.GetEmployee(userId);

            if (employee != null)
            {
                employeename = employee.EmpName + " " + employee.EmpSurname;
            }
            else
            {
                var admin = await _repsository.GetAdmin(userId);
                employeename = admin.AdminName + " " + admin.AdminSurname;
            }


            // Create an object to hold the day-wise count data
            var reportData = new
            {
                generateddate = dategenerated,
                generateby = employeename
            };

            return Ok(reportData);
        }

        [HttpGet("GeneratePackageListReport")]
        public async Task<IActionResult> GeneratePackageListReport()
        {
            var dategenerated = DateTime.Now;

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

            var employeename = "";

            var employee = await _repsository.GetEmployee(userId);

            if (employee != null)
            {
                employeename = employee.EmpName + " " + employee.EmpSurname;
            }
            else
            {
                var admin = await _repsository.GetAdmin(userId);
                employeename = admin.AdminName + " " + admin.AdminSurname;
            }


            // Create an object to hold the day-wise count data
            var reportData = new
            {
                generateddate = dategenerated,
                generateby = employeename
            };

            return Ok(reportData);
        }

        [HttpGet("GenerateWorkoutinfoReport")]
        public async Task<IActionResult> GenerateWorkoutinfoReport()
        {
            var dategenerated = DateTime.Now;

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

            var employeename = "";

            var employee = await _repsository.GetEmployee(userId);

            if (employee != null)
            {
                employeename = employee.EmpName + " " + employee.EmpSurname;
            }
            else
            {
                var admin = await _repsository.GetAdmin(userId);
                employeename = admin.AdminName + " " + admin.AdminSurname;
            }


            // Create an object to hold the day-wise count data
            var reportData = new
            {
                generateddate = dategenerated,
                generateby = employeename
            };

            return Ok(reportData);
        }

        [HttpGet("GenerateStaffReport")]
        public async Task<IActionResult> GenerateStaffReport()
        {


            var schedules=await _repsository.GetAllSchedulesAsync();

            var reportDataList = new List<object>();

            int totalTimesAssigned = 0;


            Dictionary<int, int> employeeIdCounts = new Dictionary<int, int>();

            foreach (var schedule in schedules)
            {
                var employeeId = (int)schedule.EmployeeId;

                if (employeeIdCounts.ContainsKey(employeeId))
                {
                    employeeIdCounts[employeeId]++;
                }
                else
                {
                    employeeIdCounts[employeeId] = 1;
                }

                totalTimesAssigned++;
            }

            int totalEmployees = employeeIdCounts.Count;

            foreach (var employeeId in employeeIdCounts.Keys)
            {
                var employeedata = await _repsository.GetEmployeeAsync(employeeId);

                var staffdata = new
                {
                    EmployeeId = employeeId,
                    QuantityAssigned = employeeIdCounts[employeeId],
                    name=employeedata.EmpName,
                    surname=employeedata.EmpSurname
                };

                reportDataList.Add(staffdata);
            }

            //var length = 2 / schedules.Count;

            //var average=Summ()

            var average=totalTimesAssigned/totalEmployees;


            var dategenerated = DateTime.Now;

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

            var employeename = "";

            var employee = await _repsository.GetEmployee(userId);

            if (employee != null)
            {
                employeename = employee.EmpName + " " + employee.EmpSurname;
            }
            else
            {
                var admin = await _repsository.GetAdmin(userId);
                employeename = admin.AdminName + " " + admin.AdminSurname;
            }


            // Create an object to hold the day-wise count data
            var reportData = new
            {
                generateddate = dategenerated,
                generateby = employeename,
                averageinfo= average,
                reportdatadata=reportDataList
            };


            return Ok(reportData);
        }
    }
}
