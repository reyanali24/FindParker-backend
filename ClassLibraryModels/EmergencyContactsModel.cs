using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
    public class EmergencyContactsModel
    {
        public long ContactId { get; set; }
        public long UserId { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? Relationship { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
