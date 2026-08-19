using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public class AlertsModel
    {
        public long AlertId { get; set; }

        public long UserId { get; set; }

        public long? VehicleId { get; set; }

        public string? AlertType { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
