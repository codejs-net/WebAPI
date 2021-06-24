using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Auth
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string AppSecret { get; set; }
    }
}