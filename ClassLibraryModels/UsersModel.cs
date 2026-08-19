
using System.Text.Json.Serialization;
namespace ClassLibraryModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AuthProvider
    {
        Google,
        Password
    }
    public class UsersModel
    {
        public long UserId { get; set; }
        public string? FirebaseUid { get; set; }
        public string? Email { get; set; }
        public AuthProvider AuthProvider { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

    }
}
