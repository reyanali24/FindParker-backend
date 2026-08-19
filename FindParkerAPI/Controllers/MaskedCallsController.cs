using ClassLibraryDAL.Services;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaskedCallsController : ControllerBase
    {
        private readonly IMaskedCallsInterface _callService;

        public MaskedCallsController(
            IMaskedCallsInterface callService)
        {
            _callService = callService;
        }

        [HttpPost]
        public IActionResult CreateMaskedCall(
            MaskedCallsModel call)
        {
            int result = _callService.Create(call);

            if (result > 0)
            {
                return Ok( "Masked call created successfully");
            }

            return BadRequest("Masked call could not be created");
        }

        [HttpGet]
        public IActionResult ReadMaskedCalls()
        {
            var calls = _callService.Read();
            return Ok(calls);
        }

        [HttpPut]
        public IActionResult UpdateMaskedCall(
            MaskedCallsModel call)
        {
            int result = _callService.Update(call);
            if (result > 0)
            {
                return Ok("Masked call updated successfully");
            }

            return BadRequest("Masked call could not be updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMaskedCall(long id)
        {
            int result = _callService.Delete(id);

            if (result > 0)
            {
                return Ok("Masked call deleted successfully");
            }

            return BadRequest("Masked call could not be deleted");
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(long userId)
        {
            var calls =
                _callService.GetByUserId(userId);

            return Ok(calls);
        }
    }
}
