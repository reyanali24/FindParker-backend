using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public class LoginHistoryModel
    {
        public long HistoryId { get; set; }

        public long UserId { get; set; }

        public string? DeviceInfo { get; set; }

        public string? IpAddress { get; set; }

        public DateTime LoginAt { get; set; }

        public DateTime? LoggedOutAt { get; set; }
    }
}
