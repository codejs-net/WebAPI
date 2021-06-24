using System.Text.Json.Serialization;
using WebApi.Entities;

namespace WebApi.Models.Auth
{
    public class AuthenticateResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public int Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }

      //  [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}