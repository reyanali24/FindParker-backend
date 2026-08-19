using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public class MaskedCallsModel
    {
        public long CallId { get; set; }

        public long UserId { get; set; }

        public long? VehicleId { get; set; }

        public string? CallerMaskedNumber { get; set; }

        public string? CallStatus { get; set; }

        public int? CallDurationSeconds { get; set; }

        public DateTime CalledAt { get; set; }
    }
}
