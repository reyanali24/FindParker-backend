using ClassLibraryDAL.Interfaces;
using ClassLibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindParkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodesController : ControllerBase
    {
        private readonly IQrCodesInterface _qrCodeService;
        public QrCodesController(IQrCodesInterface qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }
        [HttpPost]
        public IActionResult CreateQrCode(QrCodesModel qrCode)
        {
            int result = _qrCodeService.Create(qrCode);
            if (result > 0)
            {
                return Ok("QR code created successfully");
            }
            return BadRequest("QR code could not be created");
        }
        [HttpGet]
        public IActionResult ReadQrCodes()
        {
            var qrCodes = _qrCodeService.Read();
            return Ok(qrCodes);
        }
        [HttpPut]
        public IActionResult UpdateQrCode(QrCodesModel qrCode)
        {
            int result = _qrCodeService.Update(qrCode);
            if (result > 0)
            {
                return Ok("QR code updated successfully");
            }
            return BadRequest("QR code could not be updated");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteQrCode(long id)
        {
            int result = _qrCodeService.Delete(id);
            if (result > 0)
            {
                return Ok("QR code deleted successfully");
            }
            return BadRequest("QR code could not be deleted");
        }
        [HttpGet("vehicle/{vehicleId}")]
        public IActionResult GetByVehicleId(long vehicleId)
        {
            var qrCodes =_qrCodeService.GetByVehicleId(vehicleId);

            return Ok(qrCodes);
        }

        [HttpGet("public/{qrCodeValue}")]
        public IActionResult GetPublicQrCode(string qrCodeValue)
        {
            var qrCode = _qrCodeService.GetPublicQrCode(
                qrCodeValue
            );

            if (qrCode == null)
            {
                return NotFound(
                    "QR code was not found or is not assigned."
                );
            }

            return Ok(qrCode);
        }
    }
}
