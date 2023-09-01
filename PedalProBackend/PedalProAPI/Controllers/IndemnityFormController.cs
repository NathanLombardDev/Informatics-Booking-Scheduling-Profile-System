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
using Tesseract;
using CloudinaryDotNet.Actions;
using System.Diagnostics.Metrics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using IronOcr;
using Google.Cloud.Vision.V1;


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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Client")]
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

                var userClaims = User.Claims;

                //bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasClientRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
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


        [HttpPost("UploadImage")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> UploadImage()
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

                //bool hasAdminRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                //bool hasEmployeeRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
                bool hasClientRole = userClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Client");

                if (!hasClientRole)
                {
                    return Forbid("You do not have the necessary role to perform this action.");
                }

                var file = Request.Form.Files[0]; // Assuming only one file is uploaded

                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Save the uploaded file to a temporary location
                var tempFilePath = Path.GetTempFileName();
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Perform OCR on the uploaded image using Google Cloud Vision
                string extractedText = PerformOCR(tempFilePath);

                // Create the PDF from the extracted text
                byte[] pdfBytes = CreatePdfFromText(extractedText);

                // Save the PDF bytes and form info to the database
                SaveToDatabase(file.FileName, pdfBytes, client.ClientId);

                // Delete the temporary file
                System.IO.File.Delete(tempFilePath);

                return Ok(new { Message = "Image uploaded, text extracted, and PDF saved in the database." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        private byte[] CreatePdfFromText(string text)
        {
            using (var memoryStream = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(); // Use iTextSharp.text.Document here

                PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Add the extracted text to the PDF document
                document.Add(new iTextSharp.text.Paragraph(text)); // Use iTextSharp.text.Paragraph here

                document.Close();

                return memoryStream.ToArray();
            }
        }

        private void SaveToDatabase(string fileName, byte[] pdfBytes, int clientId)
        {
            var form = new ClientIndemnityForm
            {
                Title = Path.GetFileNameWithoutExtension(fileName),
                FileContent = pdfBytes,
                DateUploaded = DateTime.Now,
                ClientId = clientId
                // Other properties initialization
            };

            _repsository.Add(form);
            _repsository.SaveChangesAsync();
        }

        private string PerformOCR(string imagePath)
        {
            var credentialsJson = @"
            {
              
            }";

            var clientBuilder = new ImageAnnotatorClientBuilder
            {
                JsonCredentials = credentialsJson // Provide the credentials JSON
            };

            var client = clientBuilder.Build();

            // Load the image from the file path
            var image = Google.Cloud.Vision.V1.Image.FromFile(imagePath);

            // Perform OCR using Google Cloud Vision API
            var response = client.DetectDocumentText(image);

            // Extract the text from the response
            string extractedText = "";
            foreach (var page in response.Pages)
            {
                foreach (var block in page.Blocks)
                {
                    foreach (var paragraph in block.Paragraphs)
                    {
                        foreach (var word in paragraph.Words)
                        {
                            foreach (var symbol in word.Symbols)
                            {
                                extractedText += symbol.Text;
                            }
                            extractedText += " ";
                        }
                        extractedText += Environment.NewLine;
                    }
                }
            }

            return extractedText;
        }
    }
}

