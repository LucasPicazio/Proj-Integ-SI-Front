using System;
using System.Text.Json.Serialization;

namespace PSI_FRONT.Models
{
    public class User
    {
        [JsonPropertyName("USERNAME")]
        public string Username { get; set; }

        [JsonPropertyName("PASSWORD")]
        public string Password { get; set; }

        [JsonPropertyName("EMAIL")]
        public string Email { get; set; }

        [JsonPropertyName("FULLNAME")]
        public string FullName { get; set; }

        [JsonPropertyName("PHONENUMBER")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("ADDRESS")]
        public string Address { get; set; }

        [JsonPropertyName("BIRTHDAY")]
        public DateTime Birthday { get; set; }
    }
}
