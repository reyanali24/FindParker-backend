using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginHistoryController : ControllerBase
    {
        private readonly ILoginHistoryInterface _loginHistoryService;

        public LoginHistoryController(
            ILoginHistoryInterface loginHistoryService)
        {
            _loginHistoryService = loginHistoryService;
        }
      
        [HttpPost]
        public IActionResult CreateLoginHistory(
            LoginHistoryModel history)
        {
            int result =_loginHistoryService.Create(history);

            if (result > 0)
            {
                return Ok("Login history created successfully");
            }

            return BadRequest("Login history could not be created");
        }


        [HttpGet]
        public IActionResult ReadLoginHistory()
        {
            var history =_loginHistoryService.Read();
            return Ok(history);
        }


        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(long userId)
        {
            var history =_loginHistoryService.GetByUserId(userId);

            return Ok(history);
        }


   
        [HttpPut]
        public IActionResult UpdateLoginHistory(LoginHistoryModel history)
        {
            int result =    _loginHistoryService.Update(history);

            if (result > 0)
            {
                return Ok("Login history updated successfully");
            }

            return BadRequest("Login history could not be updated");
        }


       
        [HttpDelete("{id}")]
        public IActionResult DeleteLoginHistory(long id)
        {
            int result =_loginHistoryService.Delete(id);

            if (result > 0)
            {
                return Ok("Login history deleted successfully");
            }

            return BadRequest("Login history could not be deleted");
        }
    }
}
