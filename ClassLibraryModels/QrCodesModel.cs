using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public class QrCodesModel
    {
        public long QrId { get; set; }
        public string? SerialNo { get; set; }
        public string? QrCodeValue { get; set; }
        public string? QrLink { get; set; }
        public bool IsAssigned { get; set; }
        public long? VehicleId { get; set; }
        public string? Status { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
