using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryModels
{
   
        public class UserProfilesModel
        {
            public long ProfileId { get; set; }
            public long UserId { get; set; }
            public string? FullName { get; set; }
            public string? PhoneNumber { get; set; }
            public string? ResidentialAddress { get; set; }
            public string? City { get; set; }
            public string? ProfilePhotoUrl { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
