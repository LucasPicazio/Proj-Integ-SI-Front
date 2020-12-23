using System.Text.Json.Serialization;

namespace PSI_FRONT.Models
{
    public class UserToken
    {
        [JsonPropertyName("MEMBERID")]
        public string UserId { get; set; }

        [JsonPropertyName("USERNAME")]
        public string Username { get; set; }

        [JsonPropertyName("PASSWORD")]
        public string Password { get; set; }
    }
}
