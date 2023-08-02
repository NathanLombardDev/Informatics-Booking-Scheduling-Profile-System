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


        public PedalProUserController(PedalProDbContext context, IRepository repsository, UserManager<PedalProUser> userManager)
        {
            _context = context;
            _repsository = repsository;
            _userManager = userManager;
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


        [HttpPost]
        [Route("SendBookingReminder/{clientId}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> SendBookingReminder(int clientId)
        {
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


    }
}
