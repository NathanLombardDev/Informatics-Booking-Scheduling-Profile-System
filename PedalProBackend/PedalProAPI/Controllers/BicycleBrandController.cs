using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicycleBrandController : ControllerBase
    {
        private readonly IRepository _repsository;

        public BicycleBrandController(IRepository repository)
        {
            _repsository = repository;
        }

        [HttpGet]
        [Route("GetAllBicycleBrands")]
        public async Task<IActionResult> GetAllBicycleBrands()
        {
            var bicycleBrands = await _repsository.GetAllBicycleBrandAsync();
            return Ok(bicycleBrands);
        }

        [HttpGet]
        [Route("GetBicycleBrand/{bicycleBrandId}")]
        public async Task<IActionResult> GetBicycleBrand(int bicycleBrandId)
        {
            try
            {
                var result = await _repsository.GetBicycleBrandAsync(bicycleBrandId);
                if (result == null) return NotFound("Bicycle Brand does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddBicycleBrand")]
        public async Task<IActionResult> AddBicycleBrand(BicycleBrandViewModel brandAdd)
        {

            var brandImage = new BrandImage
            {
                ImageTypeId = brandAdd.ImageTypeId,
                BrandImgName=brandAdd.BrandImgName,
                ImageUrl=brandAdd.ImageUrl
            };

            var brand = new BicycleBrand();

            try
            {

                _repsository.Add(brandImage);
                await _repsository.SaveChangesAsync();

                var test = await _repsository.GetBrandImageAsync(brandImage.BrandImageId);

                brand.BrandName = brandAdd.BrandName;
                brand.BrandImageId = test.BrandImageId;

                _repsository.Add(brand);
                await _repsository.SaveChangesAsync();
            }

            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(brand);
        }



        [HttpPut]
        [Route("EditBicycleBrand/{bicycleBrandId}")]
        public async Task<ActionResult<PedalProRoleViewModel>> EditBicycleBrand(int bicycleBrandId, BicycleBrandViewModel brandModel)
        {

            try
            {

                var existingBicycleBrand= await _repsository.GetBicycleBrandAsync(bicycleBrandId);
                if (existingBicycleBrand == null) return NotFound("The bicycle brand does not exist");

                var existingBrandImage = await _repsository.GetBrandImageAsync((int)existingBicycleBrand.BrandImageId);
                if (existingBrandImage == null) return NotFound("The brand image does not exist");


                existingBrandImage.ImageTypeId = brandModel.ImageTypeId;
                existingBrandImage.BrandImgName = brandModel.BrandImgName;
                existingBrandImage.ImageUrl = brandModel.ImageUrl;


                existingBicycleBrand.BrandName = brandModel.BrandName;
                existingBicycleBrand.BrandImageId= existingBrandImage.BrandImageId;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingBicycleBrand);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }

        [HttpDelete]
        [Route("DeleteBicycleBrand/{bicycleBrandId}")]
        public async Task<IActionResult> DeleteTrainingMaterial(int bicycleBrandId)
        {

            try
            {
                var existingBicycleBrand = await _repsository.GetBicycleBrandAsync(bicycleBrandId);
                if (existingBicycleBrand == null) return NotFound("The bicycle brand does not exist");

                var existingBrandImage = await _repsository.GetImgBrandVidAsync((int)existingBicycleBrand.BrandImageId);
                if (existingBrandImage == null) return NotFound("The brand image does not exist");

                if (existingBrandImage.Any())
                {
                    foreach (var item in existingBrandImage)
                    {
                        _repsository.Delete(item);
                    }
                }

                _repsository.Delete(existingBicycleBrand);

                if (await _repsository.SaveChangesAsync()) return Ok(existingBicycleBrand);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }


        [HttpGet]
        [Route("GetAllImageTypes")]
        public async Task<IActionResult> GetAllImageTypes()
        {
            var imageTypes = await _repsository.GetAllImageTypeAsync();
            return Ok(imageTypes);
        }

        [HttpGet]
        [Route("GetImageType/{imageTypeId}")]
        public async Task<IActionResult> GetImageType(int imageTypeId)
        {
            try
            {
                var result = await _repsository.GetImageTypeAsync(imageTypeId);
                if (result == null) return NotFound("Image Type does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }
    }
}
