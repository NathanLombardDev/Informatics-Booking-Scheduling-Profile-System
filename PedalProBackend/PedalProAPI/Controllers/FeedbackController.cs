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
    public class FeedbackController : ControllerBase
    {

        private readonly UserManager<PedalProUser> _userManager;
        private readonly IRepository _repository;
        private readonly IUserClaimsPrincipalFactory<PedalProUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly PedalProDbContext _context;

        public FeedbackController(UserManager<PedalProUser> userManager, ILogger<AuthenticationController> logger, IUserClaimsPrincipalFactory<PedalProUser> claimsPrincipalFactory, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IRepository repository, PedalProDbContext context)
        {
            _userManager = userManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
            _repository = repository;
            _roleManager = roleManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("GetAllFeedbacks")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var bookingTypes = await _repository.GetAllFeedbackAsync();
            return Ok(bookingTypes);
        }

        [HttpGet]
        [Route("GetFeedback/{feedbackId}")]
        public async Task<IActionResult> GetFeedback(int feedbackId)
        {
            try
            {
                var result = await _repository.GetFeedbackAsync(feedbackId);
                if (result == null) return NotFound("Feedback does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetAllFeedbackCategories")]
        public async Task<IActionResult> GetAllFeedbackCategories()
        {
            var bookingTypes = await _repository.GetAllFeedbackCategoriesAsync();
            return Ok(bookingTypes);
        }

        [HttpGet]
        [Route("GetFeedbackCategory/{feedbacCategorykId}")]
        public async Task<IActionResult> GetFeedbackCategory(int feedbacCategorykId)
        {
            try
            {
                var result = await _repository.GetFeedbackCategoryAsync(feedbacCategorykId);
                if (result == null) return NotFound("Feedback category does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("ProvideFeedback")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ProvideFeedback(FeedbackViewModel cvm)
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

                var bookingType = new Feedback { FeedbackCategoryId = cvm.FeedbackCategoryId, FeedbackDescription = cvm.FeedbackDescription, FeedbackRating = cvm.FeedbackRating,ClientId=client.ClientId};

                _repository.Add(bookingType);
                await _repository.SaveChangesAsync();

                return Ok(bookingType);
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            
        }

        [HttpGet]
        public ActionResult<IEnumerable<FeedbackCategory>> GetFeedbackTypes()
        {
            var feedbackTypes = _context.FeedbackCategories;
            return Ok(feedbackTypes);
        }

        // POST: api/Feedback
        [HttpPost]
        public ActionResult<Feedback> PostFeedback(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

            return Ok(feedback);
        }


    }
}
