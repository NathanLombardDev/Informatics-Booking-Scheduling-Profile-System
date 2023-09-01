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
              ""type"": ""service_account"",
              ""project_id"": ""shaped-approach-397221"",
              ""private_key_id"": ""bd268996f301e2f06b578647d3e52bd0708f142c"",
              ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDJI4g6FEpWQtaR\ng4HdIaocEGMdXEq1M2mUzn4yX79GviWjie1bO6DTo/Ue3SvswXw9M2ngc0/MRyGZ\nbmG8jBMHC52ivRSIAExP1aRLXoV/Hrgr6s7IMWIjOJ0tATI0p3VQJ+92/CuRpZ52\nx8fEQqCgMvfDed62kTORl4OB2a4xkoZaTRTDh7bM5Sl+QT5AVZ/rHSRUyy7XdVQo\nrJxpVn1ZFUpC/R2IH5pudUDD6g39y4dUahftpU1w9oHj3Jge1KRcAk7mTfpe8Ps1\nukQEm3Y3HLspKrlunj/9tlEYWSWxhK1EugPTCWzu9Hli7+6YBly9hVqNg6tIY2RY\nnd+IAEDBAgMBAAECggEAIrW4hPlKaL9pArx0dSIhlm03hXIPTFy+bBkESxId0onu\nud2L8dIE63DgVH6rXVR4aaFNNJgBnclqoW0cfaqYf/8/qT1C5UNT5/UAwKbjl6/H\n7YA8RezLuDNFYGZT8SUyx+3UU66poB06Dtpj/VjYGj0hZ4DI7613/jhsz8QuXMxT\nCkQ5IsYBFoIvd/QeC05vKwSCUIZBJO9opqZBrfVpjJAMtBcch5AMZPy9pNpk9iOq\nQ+rhdCVx1iIKGHhT0AiWI6D5mYDLK+Vh7xgANyBvnMAGL/b3JTkeAAIN4NSI4wYf\n8Lwzg1FgDU4aeOvosL3SCv5fFsstTef9UWZGw8sEiQKBgQD9aonqdT/n4OS0uaJ0\nLj/4cIgQ+SahMfxcTjJrBv5d1a87Uv3JiozoXUsIacfWnZxi7FUrVx6Nlgf6cmWU\nnWH7xtbH5YbQoY67udyTyqrkVvTUhHPpzPzxN0kkzcQAq+Szxi9xHgU5GFFDcFmR\nCRzCUju6PypNcxCNW1PPfr+BzQKBgQDLMIpImpr5udiMtq+bmdrV5/GAZOBr2MaE\nQDo9PY8VLjql6lTPQuejNLBVoSFNwwVKa+0wm6rAizjzVjOnhEp8wT1Kk8FASsb+\nHLHwCKtd3ZVnOf52WfKiGymYptZAbsG8m2j8uLsxjFIQ+K3Y/PHwJSzPFEsgWjwN\nQAGc6KDWxQKBgQC9fqMujQqCa+rZMU7HLaZsMkms14IJW4VnyJlu6sXeiOEFrWNV\nB6OiRdDLs73ZP/YnCq4NQJH6Mcw5fL10ydLxIJiO1ZVgAM7olWzZntz27gcZuwmq\nNLyX601ole0Qy3iy7WmgXmBtdz+c/DAdggDdVfyPopgRXg9shHRBXnf5qQKBgH7t\n97SbQCy4eBqPFOxzFE2D801ttvQoGvsK1FblWbi1hFWZKRnAhctiYrVbTt6w8WEo\nQqeW1vgpAI6iTupard15KbyoyJRNIjoj2tRD1ilw/p/ZjqiIUBYMOdPuhPEDP2t+\n+frFu8qcCbgoYRGoEcserfs+hh/TvqfYUCmg+LixAoGAIQX1CMznpc609Kzx6g7N\nZBQ8yljiQVQpINZ9gzBMQunbK2oktfSjxQhYwnRY27IWDoUgMhbEbpBTapIbcImg\n5t5pjqUWADlvRXrKSvU5BNGxsRMSSICh8bXJX196x6J5hqfK/GwSowaZs+31H6nX\nVvVCQGl+JNlZXZiDUU1lHGc=\n-----END PRIVATE KEY-----\n"",
              ""client_email"": ""pedalpedal@shaped-approach-397221.iam.gserviceaccount.com"",
              ""client_id"": ""105315503759594244607"",
              ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
              ""token_uri"": ""https://oauth2.googleapis.com/token"",
              ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
              ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/pedalpedal%40shaped-approach-397221.iam.gserviceaccount.com"",
              ""universe_domain"": ""googleapis.com""
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

