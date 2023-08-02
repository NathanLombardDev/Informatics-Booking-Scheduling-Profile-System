using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PedalProAPI.Models;
using PedalProAPI.Repositories;
using PedalProAPI.ViewModels;

namespace PedalProAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientTypeController : ControllerBase
    {
        private readonly IRepository _repository;
        public ClientTypeController(IRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route("GetAllClientTypes")]
        public async Task<IActionResult> GetAllClientType()
        {
            var ClientTypes = await _repository.GetAllClientTypeAsync();
            return Ok(ClientTypes);
        }

        [HttpGet]
        [Route("GetAllClientType/{clientTypeId}")]
        public async Task<IActionResult> GetClientType(int clientTypeId)
        {
            try
            {
                var result = await _repository.GetClientTypeAsync(clientTypeId);
                if (result == null) return NotFound("Client Type does not exist");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPost]
        [Route("AddClientTypes")]
        public async Task<IActionResult> AddClientTypes(ClientTypeViewModel cvm)
        {
            var clientType = new ClientType { ClientTypeName = cvm.ClientTypeName };

            try
            {
                _repository.Add(clientType);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return Ok(clientType);
        }

        [HttpPut]
        [Route("EditClientType/{clientTypeId}")]
        public async Task<ActionResult<ClientTypeViewModel>> EditClientType(int clientTypeId, ClientTypeViewModel clientModel)
        {
            try
            {
                var existingclientType = await _repository.GetClientTypeAsync(clientTypeId);
                if (existingclientType == null) return NotFound($"The Client Type does not exist");

                existingclientType.ClientTypeName = clientModel.ClientTypeName;


                if (await _repository.SaveChangesAsync())
                {
                    return Ok(existingclientType);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }


        [HttpDelete]
        [Route("DeleteClientType/{clientTypeId}")]
        public async Task<IActionResult> DeleteClientType(int clientTypeId)
        {
            try
            {
                var existingclientType = await _repository.GetClientTypeAsync(clientTypeId);
                if (existingclientType == null) return NotFound($"The client type does not exist");

                _repository.Delete(existingclientType);

                if (await _repository.SaveChangesAsync()) return Ok(existingclientType);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("");
        }
    }
}
