using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly IUserProfilesInterface _userProfileService;

        public UserProfilesController(IUserProfilesInterface userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost]
        public IActionResult CreateProfile(UserProfilesModel profile)
        {
            int result = _userProfileService.Create(profile);

            if (result > 0)
            {
                return Ok("User profile created successfully");
            }

            return BadRequest("User profile could not be created");
        }

        [HttpGet]
        public IActionResult ReadProfiles()
        {
            var profiles = _userProfileService.Read();

            return Ok(profiles);
        }

        [HttpPut]
        public IActionResult UpdateProfile(UserProfilesModel profile)
        {
            int result = _userProfileService.Update(profile);

            if (result > 0)
            {
                return Ok("User profile updated successfully");
            }

            return BadRequest("User profile could not be updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfile(long id)
        {
            int result = _userProfileService.Delete(id);

            if (result > 0)
            {
                return Ok("User profile deleted successfully");
            }

            return BadRequest("User profile could not be deleted");
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(long userId)
        {
            var profile = _userProfileService.GetByUserId(userId);

            return Ok(profile);
        }
    }
}
