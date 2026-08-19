using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public class VehiclesModel
    {
        public long VehicleId { get; set; }

        public long UserId { get; set; }

        public string? Name { get; set; }

        public string? PlateNumber { get; set; }

        public string? Color { get; set; }

        public string? VehicleType { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
