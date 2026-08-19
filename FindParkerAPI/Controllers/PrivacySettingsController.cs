using ClassLibraryDAL.Interfaces;
using ClassLibraryDAL.Services;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivacySettingsController : ControllerBase
    {
        private readonly IPrivacySettingsInterface _privacyService;

        public PrivacySettingsController(IPrivacySettingsInterface privacyService)
        {
            _privacyService = privacyService;
        }

        [HttpPost]
        public IActionResult CreatePrivacySettings(
            PrivacySettingsModel settings)
        {
            int result = _privacyService.Create(settings);

            if (result > 0)
            {
                return Ok("Privacy settings created successfully");
            }

            return BadRequest("Privacy settings could not be created");
        }

        [HttpGet]
        public IActionResult ReadPrivacySettings()
        {
            var settings = _privacyService.Read();

            return Ok(settings);
        }

        [HttpPut]
        public IActionResult UpdatePrivacySettings(
            PrivacySettingsModel settings)
        {
            int result = _privacyService.Update(settings);

            if (result > 0)
            {
                return Ok("Privacy settings updated successfully");
            }

            return BadRequest("Privacy settings could not be updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePrivacySettings(long id)
        {
            int result = _privacyService.Delete(id);

            if (result > 0)
            {
                return Ok("Privacy settings deleted successfully");
            }

            return BadRequest("Privacy settings could not be deleted");
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(long userId)
        {
            var settings =
                _privacyService.GetByUserId(userId);

            return Ok(settings);
        }
    }
}
