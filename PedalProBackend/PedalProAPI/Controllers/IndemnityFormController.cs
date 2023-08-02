using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using PedalProAPI.Models;
using Microsoft.AspNetCore.Identity;
using PedalProAPI.Context;
using PedalProAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndemnityFormController : ControllerBase
    {

        private readonly IRepository _repsository;
        private readonly UserManager<PedalProUser> _userManager;
        private readonly PedalProDbContext _dbContext;

        public IndemnityFormController(IRepository repository, UserManager<PedalProUser> userManager, PedalProDbContext dbContext)
        {
            _userManager = userManager;
            _repsository = repository;
            _dbContext = dbContext;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadDocument")]
        public async Task<IActionResult> UploadDocument([FromForm] IFormFile file, [FromForm] string title)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Check if the uploaded file is a Word document (application/msword or application/vnd.openxmlformats-officedocument.wordprocessingml.document)
                if (!(file.ContentType == "application/msword" || file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                {
                    return BadRequest("Invalid file format. Please upload a Word document.");
                }

                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();

                    var document = new IndemnityForm
                    {
                        Title = title,
                        FileContent = fileBytes,
                        DateUploaded = DateTime.Now
                    };

                    _repsository.Add(document);
                    await _repsository.SaveChangesAsync();
                }

                return Ok("Document uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("ClientUploadDocument")]
        public async Task<IActionResult> ClientUploadDocument([FromForm] IFormFile file, [FromForm] string title)
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



                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Check if the uploaded file is a Word document (application/msword or application/vnd.openxmlformats-officedocument.wordprocessingml.document)
                if (file.ContentType != "application/pdf")
                {
                    return BadRequest("Invalid file format. Please upload a PDF document.");
                }

                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();

                    var document = new ClientIndemnityForm
                    {
                        Title = title,
                        FileContent = fileBytes,
                        DateUploaded = DateTime.Now,
                        ClientId=client.ClientId
                    };

                    _repsository.Add(document);
                    await _repsository.SaveChangesAsync();
                }

                return Ok("Document uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("GetLatestDocument")]
        public IActionResult GetLatestDocument()
        {
            var task = _repsository.GetLatestDocument();
            var latestDocument = task.Result;

            if (latestDocument == null)
            {
                return NotFound();
            }

            return File(latestDocument.FileContent, "application/msword", "document.docx");
        }
    }
}
