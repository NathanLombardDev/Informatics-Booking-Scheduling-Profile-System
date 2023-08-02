using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;

using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingTypeController : ControllerBase
    {
        private readonly IRepository _repository;
        public BookingTypeController(IRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route("GetAllBookingTypes")]
        public async Task<IActionResult> GetAllBookingTypes()
        {
            var bookingTypes = await _repository.GetAllBookingTypeAsync();
            return Ok(bookingTypes);
        }

        [HttpGet]
        [Route("GetBookingType/{bookingTypeId}")]
        public async Task<IActionResult> GetBookingType(int bookingTypeId)
        {
            try
            {
                var result = await _repository.GetBookingTypeAsync(bookingTypeId);
                if (result == null) return NotFound("Booking Type does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddBookingTypes")]
        public async Task<IActionResult> AddBookingTypes(BookingTypeViewModel cvm)
        {
            var bookingType = new BookingType { BookingTypeName = cvm.BookingTypeName };

            try
            {
                _repository.Add(bookingType);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(bookingType);
        }

        [HttpPut]
        [Route("EditBookingType/{bookingTypeId}")]
        public async Task<ActionResult<BookingTypeViewModel>> EditBookingType(int bookingTypeId, BookingTypeViewModel bookingModel)
        {
            try
            {
                var existingbookingType = await _repository.GetBookingTypeAsync(bookingTypeId);
                if (existingbookingType == null) return NotFound($"The Booking Type does not exist");

                existingbookingType.BookingTypeName = bookingModel.BookingTypeName;


                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingbookingType);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }


        [HttpDelete]
        [Route("DeleteBookingType/{bookingTypeId}")]
        public async Task<IActionResult> DeleteBookingType(int bookingTypeId)
        {
            try
            {
                var existingbookingType = await _repository.GetBookingTypeAsync(bookingTypeId);
                if (existingbookingType == null) return NotFound($"The booking type does not exist");

                _repository.Delete(existingbookingType);

                if (await _repository.SaveChangesAsync()) return Ok(existingbookingType);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }
    }
}
