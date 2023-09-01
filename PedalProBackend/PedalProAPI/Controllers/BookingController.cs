using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBitcoin.JsonConverters;
using PedalProAPI.Context;
using PedalProAPI.Models;
using PedalProAPI.Other_Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;
using System.Security.Claims;
using System.Text;

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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BookingController(IRepository repository, UserManager<PedalProUser> userManager, PedalProDbContext dbContext, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _userManager = userManager;
            _repsository = repository;
            _dbContext = dbContext;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllBookings")]
        
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
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
           
        }
        private async Task<double> CalculateTotalAmount(int bookingTypeId, string userId)
        {
            var bookingType = await _repsository.GetBookingTypetwo(bookingTypeId);
            var client = await _repsository.GetClient(userId);

            var total = 0.00;

            if (client.NumBookingsAllowance <= 0)
            {
                total = (double)bookingType.BookingTypePrice;
            }
            else
            {
                total = ((int)bookingType.BookingTypePrice) / 2;
            }

            // Add other conditions or calculations here if needed

            return (double)total;
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
       
        public async Task<IActionResult> AddBooking(ComplexBookingViewModel schedule)
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
                setup.SetupDescription = schedule.Description;
                setup.BicycleId = schedule.bicycleId;
                _repsository.Add(setup);
                await _repsository.SaveChangesAsync();
                sched.SetupId = setup.SetupId;
                await _repsository.SaveChangesAsync();


                var revenue = new BookingRevenue();


                var bookingRevenue = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);

                if(bookingRevenue==null)
                {
                    revenue.BookingType = bookingTypeType.BookingTypeName;
                    revenue.Quantity = 1;
                    _repsository.Add(revenue);
                }
                else
                {
                    bookingRevenue.Quantity += 1;
                }
                await _repsository.SaveChangesAsync();


            }
            else if(bookingTypeType.BookingTypeName.Contains("Service") ||
                bookingTypeType.BookingTypeName.Contains("fix") ||
                bookingTypeType.BookingTypeName.Contains("Repair")) 
            {
                var service = new Service();
                service.ServiceDescription = "";
                service.BicyclePartId = schedule.BicyclePartId;
                service.BicycleId = schedule.bicycleId;
                _repsository.Add(service);
                await _repsository.SaveChangesAsync();
                sched.ServiceId = service.ServiceId;
                await _repsository.SaveChangesAsync();

                var revenue = new BookingRevenue();


                var bookingRevenue = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);

                if (bookingRevenue == null)
                {
                    revenue.BookingType = bookingTypeType.BookingTypeName;
                    revenue.Quantity = 1;
                    _repsository.Add(revenue);
                }
                else
                {
                    bookingRevenue.Quantity += 1;
                }
                await _repsository.SaveChangesAsync();
            }
            else if (bookingTypeType.BookingTypeName.Contains("Training") ||
                bookingTypeType.BookingTypeName.Contains("session") ||
                bookingTypeType.BookingTypeName.Contains("train"))
            {
                var trainingSession = new TrainingSession();

                trainingSession.ClientId= client.ClientId;
                trainingSession.TrainingSessionDescription = schedule.Description;

                _repsository.Add(trainingSession);
                await _repsository.SaveChangesAsync();
                sched.TrainingSessionId = trainingSession.TrainingSessionId;
                await _repsository.SaveChangesAsync();

                var revenue = new BookingRevenue();


                var bookingRevenue = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);

                if (bookingRevenue == null)
                {
                    revenue.BookingType = bookingTypeType.BookingTypeName;
                    revenue.Quantity = 1;
                    _repsository.Add(revenue);
                }
                else
                {
                    bookingRevenue.Quantity += 1;
                }
                await _repsository.SaveChangesAsync();

            }

            string referenceNum = await GetUniqueReferenceNumberAsync();

                if (string.IsNullOrEmpty(referenceNum))
                {
                    return BadRequest("Could not generate a unique reference number.");
                }

                booking.ReferenceNum = referenceNum;

                try
                {
                    var timeslot = await _dbContext.Timeslots.FindAsync(schedule.timeslotId);

                    if (timeslot == null)
                    {
                    return NotFound();
                    }

                    timeslot.TimeslotStatusId = 2;

                    _dbContext.Entry(timeslot).State = EntityState.Modified;
                    await _repsository.SaveChangesAsync();

                    _repsository.Add(booking);
                    await _repsository.SaveChangesAsync();
                    return Ok(booking);
            }
                catch (DbUpdateException ex)
                {
                    return BadRequest("Error while saving data to the database.");
                }
                catch (Exception ex)
                {
                    return BadRequest("An unexpected error occurred during the transaction.");
                }

                
            
        }

        private async Task<bool> IsReferenceNumberUsedAsync(string referenceNumber)
        {
            bool exists = await _dbContext.Bookings.AnyAsync(b => b.ReferenceNum == referenceNumber);
            return exists;
        }

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

        [HttpPost("GetBookingPaymentURL")]
        public async Task<IActionResult> GetBookingPaymentURL(int bookingTypeid)
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


            var bookingtypeinfo=await _repsository.GetBookingTypetwo(bookingTypeid);

            var bookingtypename = bookingtypeinfo.BookingTypeName;

            var total = bookingtypeinfo.BookingTypePrice;

            var totalsa = await CalculateTotalAmount(bookingTypeid, userId);

            var amount = totalsa;

            var payFastMerchantId = _configuration["PayFast:MerchantId"];
            var payFastMerchantKey = _configuration["PayFast:MerchantKey"];

            using (var client = _httpClientFactory.CreateClient())
            {
                var itemParameters = new StringBuilder();

                var returnUrl = "http://localhost:4200/SuccessfulBookingPayment";

                var paymentUrl = $"https://sandbox.payfast.co.za/eng/process" +
                                 $"/?merchant_id={payFastMerchantId}" +
                                 $"&merchant_key={payFastMerchantKey}" +
                                 $"&amount={amount}" + 
                                 $"&item_name={bookingtypename}&item_amount={amount}" + $"&return_url={Uri.EscapeDataString(returnUrl)}" + $"&cancel_url=http://localhost:4200/UnSuccessfulBookingPayment";

                return Ok(new { PaymentUrl = paymentUrl });
            }
        }



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


        [HttpGet]
        [Route("GetDateandTimeBooking/{scheduleId}")]
        public async Task<IActionResult> GetDateandTimeBooking(int scheduleId)
        {
            try
            {
                var gg = await _repsository.GetSchedule(scheduleId);

                var ss = await _repsository.getDateBooking((int)gg.DateslotId);

                var tt = await _repsository.GetDateFive((int)ss.DateId);

                var time=await _repsository.GetTimeslotAsync((int)ss.TimeslotId);

                var data = new
                {
                    dateDate = tt.Date1,
                    timeTime=time.StartTime+"-"+time.EndTime
                };
                
                if (data == null) return NotFound("This date and timeslot does not exist");
                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpDelete]
        [Route("DeleteBooking/{bookingId}")]
        
        public async Task<IActionResult> DeleteBooking(int bookingId)
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
            var booking = await _repsository.GetBookingAsync(bookingId);

            var bookingBookingBooking = booking;

            try
            {

                var schedule = await _repsository.GetSchedule((int)bookingBookingBooking.ScheduleId);

                var dateslot = await _repsository.getDateBooking((int)schedule.DateslotId);

                var timeslot = await _repsository.GetTimeslotAsync((int)dateslot.TimeslotId);

                if (timeslot == null)
                {
                    return NotFound();
                }

                timeslot.TimeslotStatusId = 1;

                _dbContext.Entry(timeslot).State = EntityState.Modified;
                await _repsository.SaveChangesAsync();


                var bookingTypeType = await _repsository.GetBookingTypetwo((int)bookingBookingBooking.BookingTypeId);

                if (bookingTypeType.BookingTypeName.Contains("Setup") ||
                    bookingTypeType.BookingTypeName.Contains("set") ||
                    bookingTypeType.BookingTypeName.Contains("up"))
                {

                    var setupID = schedule.SetupId;

                    var setup = await _repsository.GetSetup((int)setupID);

                    schedule.SetupId = null;
                    _dbContext.Entry(schedule).State = EntityState.Modified;
                    await _repsository.SaveChangesAsync();

                    _repsository.Delete(setup);
                    await _repsository.SaveChangesAsync();


                    var bookingrev = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);
                    _repsository.Delete(bookingrev);
                    await _repsository.SaveChangesAsync();

                }
                else if (bookingTypeType.BookingTypeName.Contains("Service") ||
                    bookingTypeType.BookingTypeName.Contains("fix") ||
                    bookingTypeType.BookingTypeName.Contains("Repair"))
                {
                    var serviceID = schedule.ServiceId;

                    var service = await _repsository.GetService((int)serviceID);

                    schedule.ServiceId = null;
                    _dbContext.Entry(schedule).State = EntityState.Modified;
                    await _repsository.SaveChangesAsync();

                    _repsository.Delete(service);
                    await _repsository.SaveChangesAsync();

                    var bookingrev = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);
                    _repsository.Delete(bookingrev);
                    await _repsository.SaveChangesAsync();
                }
                else if (bookingTypeType.BookingTypeName.Contains("Training") ||
                    bookingTypeType.BookingTypeName.Contains("session") ||
                    bookingTypeType.BookingTypeName.Contains("train"))
                {
                    var TrainingSessionId = schedule.TrainingSessionId;

                    var tsession = await _repsository.GettrainingSession((int)TrainingSessionId);

                    schedule.TrainingSessionId = null;
                    _dbContext.Entry(schedule).State = EntityState.Modified;
                    await _repsository.SaveChangesAsync();

                    _repsository.Delete(tsession);
                    await _repsository.SaveChangesAsync();


                    var bookingrev = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);
                    _repsository.Delete(bookingrev);
                    await _repsository.SaveChangesAsync();

                }

                _repsository.Delete(booking);

                if (await _repsository.SaveChangesAsync()) return Ok(bookingBookingBooking);

                return Ok();
            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            
        }

        [HttpPost("SaveBookingPayment")]    
        public async Task<IActionResult> SaveBookingPayment(int bookingId)
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
            var booking = await _repsository.GetBookingAsync(bookingId);

            var bookingBookingBooking = booking;

            var bookingtype = await _repsository.GetBookingTypeAsync((int)bookingBookingBooking.BookingTypeId);

            var total = await CalculateTotalAmount(bookingtype.BookingTypeId, userId);

            var payment = new Payment
            {
                PaymentAmount = total,
                PaymentDate = DateTime.Now,
            };


            try
            {
                _repsository.Add(payment);
                await _repsository.SaveChangesAsync();

                var checkout = new Checkout
                {
                    PaymentId = payment.PaymentId,
                    CartId = null,

                };
                _repsository.Add(checkout);
                await _repsository.SaveChangesAsync();

                if (client.NumBookingsAllowance > 0)
                {
                    client.NumBookingsAllowance--;
                }
                await _repsository.SaveChangesAsync();

                return Ok(checkout);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpDelete]
        [Route("CancelBooking/{bookingId}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CancelBooking(int bookingId,string reason)
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
            var booking = await _repsository.GetBookingAsync(bookingId);

            var bookingBookingBooking = booking;

            try
            {
                var schedule = await _repsository.GetSchedule((int)bookingBookingBooking.ScheduleId);

                var dateslot = await _repsository.getDateBooking((int)schedule.DateslotId);

                var tt = await _repsository.GetDateFive((int)dateslot.DateId);

                var dateofbooking = tt.Date1;
                DateTime currentDate = DateTime.Now;

                TimeSpan timeDifference = (TimeSpan)(dateofbooking - currentDate);

                if (timeDifference.TotalHours > 48)
                {
                    var refundReason = new RefundReason
                    {
                        RefundReasonDesc = reason
                    };

                    _repsository.Add(refundReason);
                    await _repsository.SaveChangesAsync();

                    var refund = new Refund
                    {
                        RefundReasonId = refundReason.RefundReasonId,
                        RefundDate = currentDate,
                        RefundAmount = 100.00,
                    };

                    _repsository.Add(refund);
                    await _repsository.SaveChangesAsync();
                }
                
               
                    

                    var timeslot = await _repsository.GetTimeslotAsync((int)dateslot.TimeslotId);

                    if (timeslot == null)
                    {
                        return NotFound();
                    }

                    timeslot.TimeslotStatusId = 1;

                    _dbContext.Entry(timeslot).State = EntityState.Modified;
                    await _repsository.SaveChangesAsync();


                    var bookingTypeType = await _repsository.GetBookingTypetwo((int)bookingBookingBooking.BookingTypeId);

                    if (bookingTypeType.BookingTypeName.Contains("Setup") ||
                        bookingTypeType.BookingTypeName.Contains("set") ||
                        bookingTypeType.BookingTypeName.Contains("up"))
                    {

                        var setupID = schedule.SetupId;

                        var setup = await _repsository.GetSetup((int)setupID);

                        schedule.SetupId = null;
                        _dbContext.Entry(schedule).State = EntityState.Modified;
                        await _repsository.SaveChangesAsync();

                        _repsository.Delete(setup);
                        await _repsository.SaveChangesAsync();


                        var bookingrev = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);
                        _repsository.Delete(bookingrev);
                        await _repsository.SaveChangesAsync();


                    }
                    else if (bookingTypeType.BookingTypeName.Contains("Service") ||
                        bookingTypeType.BookingTypeName.Contains("fix") ||
                        bookingTypeType.BookingTypeName.Contains("Repair"))
                    {
                        var serviceID = schedule.ServiceId;

                        var service = await _repsository.GetService((int)serviceID);

                        schedule.ServiceId = null;
                        _dbContext.Entry(schedule).State = EntityState.Modified;
                        await _repsository.SaveChangesAsync();

                        _repsository.Delete(service);
                        await _repsository.SaveChangesAsync();

                        var bookingrev = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);
                        _repsository.Delete(bookingrev);
                        await _repsository.SaveChangesAsync();
                    }
                    else if (bookingTypeType.BookingTypeName.Contains("Training") ||
                        bookingTypeType.BookingTypeName.Contains("session") ||
                        bookingTypeType.BookingTypeName.Contains("train"))
                    {
                        var TrainingSessionId = schedule.TrainingSessionId;

                        var tsession = await _repsository.GettrainingSession((int)TrainingSessionId);

                        schedule.TrainingSessionId = null;
                        _dbContext.Entry(schedule).State = EntityState.Modified;
                        await _repsository.SaveChangesAsync();

                        _repsository.Delete(tsession);
                        await _repsository.SaveChangesAsync();


                        var bookingrev = await _repsository.GetBookingRevenue(bookingTypeType.BookingTypeName);
                        _repsository.Delete(bookingrev);
                        await _repsository.SaveChangesAsync();


                    }

                    _repsository.Delete(booking);

                    if (await _repsository.SaveChangesAsync()) return Ok(bookingBookingBooking);

                    return Ok();
                
            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }



    }
}
