using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Text;
using MimeKit;
using PedalProAPI.Context;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using System.Data;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Voice;
using Vonage;
using Vonage.Request;
using Vonage.Messages.Sms;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PedalProUserController : ControllerBase
    {
        private readonly PedalProDbContext _context;
        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;
        
        private readonly IUserClaimsPrincipalFactory<PedalProUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<PedalProUserController> _logger;

        
        public PedalProUserController(PedalProDbContext context, IRepository repsository, UserManager<PedalProUser> userManager, ILogger<PedalProUserController> logger, IUserClaimsPrincipalFactory<PedalProUser> claimsPrincipalFactory, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _repsository = repsository;
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _context.PedalProUsers.ToListAsync();
            return Ok(roles);
        }


        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            var roles = await _context.Clients.ToListAsync();
            return Ok(roles);
        }

        [HttpGet("GetClientsWithBookings")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<ActionResult> GetClientsWithBookings()
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

            var clientIdsWithBookings = await _context.Bookings
                .Select(booking => booking.ClientId)
                .Distinct()
                .ToListAsync();

            var clientsWithBookings = await _context.Clients
                .Where(client => clientIdsWithBookings.Contains(client.ClientId))
                .ToListAsync();

            return Ok(clientsWithBookings);
        }


        [HttpPost]
        [Route("SendBookingReminder/{clientId}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> SendBookingReminder(int clientId)
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

            var clientbookingRem = await _repsository.GetBookingsReminder(clientId);

            if(clientbookingRem!=null)
            {

                var clientClient=await _repsository.GetClientClient(clientId);

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("nathantheawsome1234@gmail.com"));
                email.To.Add(MailboxAddress.Parse(clientClient.ClientEmailAddress));
                email.Subject = "CBT Booking Reminder";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = "<h1>Dear " + clientClient.ClientTitle + " " + clientClient.ClientSurname + "<h1/> <p><small>This email serves as a reminder for you booking at Callan's Bike Tech. Please ensure that youa arrive in time for your session.<br/> <br/> If you wish to cancel your booking, please ensure that you do it 24 hours in advance. We hope to see you shortly. <br/><br/> The CBT Team </small></p>"
                };
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("nathantheawsome1234@gmail.com", "fanmgdiiigkpjnsc");
                smtp.Send(email);
                smtp.Disconnect(true);
                var responseObj = new { message = "Booking reminder has been sent" };
                return Ok(responseObj);
            }
            else
            {
                return BadRequest("Client does not have a booking under their name");
            }

            
        }

        [HttpGet]
        [Route("GetClientDetails")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetClientDetails()
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

                if(client!=null)
                {
                    return Ok(client);
                }
            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("We were unable to complete this request");
        } 


        [HttpPost]
        [Route("SendBookingReminderTwo/{clientId}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> SendBookingReminderTwo(int clientId)
        {

            var clientbookingRem = await _repsository.GetBookingsReminder(clientId);



            


            if (clientbookingRem != null)
            {
                var clientClient = await _repsository.GetClientClient(clientId);

                var phoneNum = "+27" + clientClient.ClientPhoneNum;
                var name = clientClient.ClientName + " " + clientClient.ClientSurname;

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Dear "+ name+ ",this is a reminder of your booking(s) made at CBT. Please ensure that you arrive on time for your specified timeslot. Kind regards, CBT Team",
                    from: new Twilio.Types.PhoneNumber("+12722256251"),
                    to: new Twilio.Types.PhoneNumber(phoneNum)
                //+12722256251
                );

                return Ok($"SMS sent with SID: {message.Sid}");
            }
            else
            {
                return BadRequest("Client does not have a booking under their name");
            }

        }


        [HttpPost]
        [Route("api/send-sms")]
        public async Task<IActionResult> SendSms(int clientId)
        {
            

            var clientbookingRem = await _repsository.GetBookingsReminder(clientId);


            if (clientbookingRem != null)
            {
                var clientClient = await _repsository.GetClientClient(clientId);

                var phoneNum = clientClient.ClientPhoneNum;
                var name = clientClient.ClientName + " " + clientClient.ClientSurname;
                var message = "Dear " + name + ",this is a reminder of your booking(s) made at CBT. Please ensure that you arrive on time for your specified timeslot. Kind regards, CBT Team";
                VonageClient client = new VonageClient(new Credentials
                {
                    ApiKey = apiKey,
                    ApiSecret = apiSecret
                });

                var response = client.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest
                {
                    To = phoneNum,
                    From = "CBT",
                    Text = message
                });

                if (response.Messages[0].Status == "0")
                {
                    return Ok("SMS sent successfully.");
                }
                else
                {
                    return BadRequest("Failed to send SMS.");
                }
            }
            else
            {
                return BadRequest("Client does not have a booking under their name");
            }

        }
    }
}
