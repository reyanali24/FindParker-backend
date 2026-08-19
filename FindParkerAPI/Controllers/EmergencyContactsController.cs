using ClassLibraryDAL.Interfaces;
using ClassLibraryDAL.Services;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyContactsController : ControllerBase
    {
        private readonly IEmergencyContactsInterface _ContactService;
        public EmergencyContactsController(IEmergencyContactsInterface ContactService)
        {
            _ContactService = ContactService;
        }

        [HttpPost]
        public IActionResult CreateEmergencyContact(EmergencyContactsModel contact)
        {
            int result = _ContactService.Create(contact);
            if (result > 0)
            {
                return Ok("Emergency contact created successfully");
            }
            return BadRequest("Emergency contact could not be created");
        }

        [HttpGet]
        public IActionResult ReadEmergencyContacts()
        {
            var contacts = _ContactService.Read();
            return Ok(contacts);
        }

        [HttpPut]
        public IActionResult UpdateEmergencyContact(EmergencyContactsModel contact)
        {
            int result = _ContactService.Update(contact);
            if (result > 0)
            {
                return Ok("Emergency contact updated successfully");
            }
            return BadRequest("Emergency contact could not be updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmergencyContact(long id)
        {
            int result = _ContactService.Delete(id);
            if (result > 0)
            {
                return Ok("Emergency contact deleted successfully");
            }
            return BadRequest("Emergency contact could not be deleted");
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(long userId)
        {
            var contacts =_ContactService.GetByUserId(userId);

            return Ok(contacts);
        }
    }
}
