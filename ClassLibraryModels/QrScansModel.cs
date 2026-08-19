using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public class QrScansModel
    {
        public long ScanId { get; set; }

        public long VehicleId { get; set; }

        public string? ScanLocation { get; set; }

        public string? ScanResult { get; set; }

        public DateTime ScannedAt { get; set; }
    }
}
