using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesInterface _vehicleService;
        public VehiclesController(IVehiclesInterface vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpPost]
        public IActionResult CreateVehicle(VehiclesModel vehicle)
        {
            int result = _vehicleService.Create(vehicle);
            if (result > 0)
            {
                return Ok("Vehicle created successfully");
            }
            return BadRequest("Vehicle could not be created");
        }
        [HttpGet]
        public IActionResult ReadVehicles()
        {
            var vehicles = _vehicleService.Read();
            return Ok(vehicles);
        }
        [HttpPut]
        public IActionResult UpdateVehicle(VehiclesModel vehicle)
        {
            int result = _vehicleService.Update(vehicle);
            if (result > 0)
            {
                return Ok("Vehicle updated successfully");
            }
            return BadRequest("Vehicle could not be updated");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(long id)
        {
            int result = _vehicleService.Delete(id);
            if (result > 0)
            {
                return Ok("Vehicle deleted successfully");
            }
            return BadRequest("Vehicle could not be deleted");
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(long userId)
        {
            var vehicles = _vehicleService.GetByUserId(userId);

            return Ok(vehicles);
        }

    }
}
