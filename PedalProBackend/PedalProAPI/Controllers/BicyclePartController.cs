using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicyclePartController : ControllerBase
    {
        private readonly IRepository _repsository;
        public BicyclePartController(IRepository repository)
        {
            _repsository = repository;
        }

        [HttpGet]
        [Route("GetAllBicyclePart")]
        public async Task<IActionResult> GetAllBicyclePart()
        {
            var bicyclePart = await _repsository.GetAllBicyclePartAsync();
             return Ok(bicyclePart);
        }

        [HttpGet]
        [Route("GetBicyclePart/{bicyclePartId}")]
        public async Task<IActionResult> GetBicyclePart(int bicyclePartId)
        {
            try
            {
                var result = await _repsository.GetBicyclePartAsync(bicyclePartId);
                if (result == null) return NotFound("Bicycle part does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }


        [HttpPost]
        [Route("AddbicyclePart")]
        public async Task<IActionResult> Addbicyclepart(BicyclePartViewModel bicyclePartAdd)
        {
            var bicyclepartAdd = new BicyclePart { BicyclePartName = bicyclePartAdd.BicyclePartName };

            try
            {
                _repsository.Add(bicyclepartAdd);
                await _repsository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(bicyclepartAdd);
        }


        [HttpPut]
        [Route("Editbicyclepart/{bicyclePartId}")]
        public async Task<ActionResult<BicyclePartViewModel>> Updatebicyclepart(int bicyclePartId, BicyclePartViewModel bicyclePartModel)
        {
            try
            {
                var existingPart = await _repsository.GetBicyclePartAsync(bicyclePartId);
                if (existingPart == null) return NotFound("The Bicycle Part does not exist");

                existingPart.BicyclePartName = bicyclePartModel.BicyclePartName;

                if (await _repsository.SaveChangesAsync())
                {
                    return Ok(existingPart);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid");
        }

        [HttpDelete]
        [Route("DeleteBicyclePart/{bicyclePartId}")]
        public async Task<IActionResult> Deletebicyclepart(int bicyclePartId)
        {
            try
            {
                var existingPart = await _repsository.GetBicyclePartAsync(bicyclePartId);
                if (existingPart == null) return NotFound($"The Bicycle Part does not exist");

                _repsository.Delete(existingPart);

                if (await _repsository.SaveChangesAsync()) return Ok(existingPart);

            }
            catch
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }
    }
}
