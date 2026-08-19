using ClassLibraryDAL.Interfaces.ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrScansController : ControllerBase
    {
        private readonly IQrScansInterface _scanService;
        public QrScansController(
            IQrScansInterface scanService)
        {
            _scanService = scanService;
        }

        [HttpPost]
        public IActionResult CreateQrScan(
            QrScansModel scan)
        {
            int result = _scanService.Create(scan);
            if (result > 0)
            {
                return Ok("QR scan created successfully");
            }

            return BadRequest("QR scan could not be created");
        }

        [HttpGet]
        public IActionResult ReadQrScans()
        {
            var scans = _scanService.Read();
            return Ok(scans);
        }

        [HttpPut]
        public IActionResult UpdateQrScan(
            QrScansModel scan)
        {
            int result = _scanService.Update(scan);
            if (result > 0)
            {
                return Ok("QR scan updated successfully");
            }

            return BadRequest("QR scan could not be updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQrScan(long id)
        {
            int result = _scanService.Delete(id);
            if (result > 0)
            {
                return Ok("QR scan deleted successfully");
            }

            return BadRequest("QR scan could not be deleted");
        }
        [HttpGet("vehicle/{vehicleId}")]
        public IActionResult GetQrScansByVehicleId(long vehicleId)
        {
            var scans =_scanService.GetByVehicleId(vehicleId);
            return Ok(scans);
        }
    }
}
