using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertsInterface _alertService;

        public AlertsController(IAlertsInterface alertService)
        {
            _alertService = alertService;
        }
        [HttpPost]
        public IActionResult CreateAlert(AlertsModel alert)
        {
            int result = _alertService.Create(alert);
            if (result > 0)
            {
                return Ok("Alert created successfully");
            }
            return BadRequest("Alert could not be created");
        }

        [HttpGet]
        public IActionResult ReadAlerts()
        {
            var alerts = _alertService.Read();

            return Ok(alerts);
        }

       
        [HttpGet("user/{userId}")]
        public IActionResult GetAlertsByUserId(long userId)
        {
            var alerts = _alertService.GetByUserId(userId);
            return Ok(alerts);
        }

        [HttpPut]
        public IActionResult UpdateAlert(AlertsModel alert)
        {
            int result = _alertService.Update(alert);

            if (result > 0)
            {
                return Ok("Alert updated successfully");
            }
            return BadRequest("Alert could not be updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAlert(long id)
        {
            int result = _alertService.Delete(id);

            if (result > 0)
            {
                return Ok("Alert deleted successfully");
            }
            return BadRequest("Alert could not be deleted");
        }
    }
}
