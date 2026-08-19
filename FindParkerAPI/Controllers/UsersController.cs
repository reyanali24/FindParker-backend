using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersInterface _userService;

        public UsersController(IUsersInterface userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateUser(UsersModel user)
        {
            long result = _userService.CreateUser(user);

            if (result > 0)
            {
                return Ok("User created successfully");
            }

            return BadRequest("User could not be created");
        }
        [HttpGet]
        public IActionResult ReadUsers()
        {
            var users = _userService.Read();
            return Ok(users);
        }
        [HttpPut]
        public IActionResult UpdateUser(UsersModel user)
        {
            int result = _userService.Update(user);
            if (result > 0)
            {
                return Ok("User updated successfully");
            }
            return BadRequest("User could not be updated");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            int result= _userService.Delete(id);
            if (result > 0) {
                return Ok("Student Deleted Succesfully"); }
            return BadRequest("Unable to Delete");
            }
        }
    
    }


