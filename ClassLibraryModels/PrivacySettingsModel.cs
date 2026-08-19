using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public class PrivacySettingsModel
    {
        public long SettingId { get; set; }
        public long UserId { get; set; }
        public string? PrivacyMode { get; set; }
        public bool MaskedLineEnabled { get; set; }
        public bool AutoReplyEnabled { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
