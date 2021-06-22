using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Authorization;
using WebApi.Models.Users;
using WebApi.Services;
using WebApi.Services.Auth;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }


        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            AuthenticateResponse response = await _service.Authenticate(request, ipAddress());
            if (response.Success == false)
            {
                return BadRequest(new { message = response.Message });
            }
            else
            {
                //setTokenCookie(response.RefreshToken);
                return Ok(response);
            }
           
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task <IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            //var refreshToken = Request.Cookies["refreshToken"];
            var refreshToken = request.Token;
            var response = await _service.RefreshToken(refreshToken, ipAddress());
            if (response.Success == false)
            {
                return BadRequest(new { message = response.Message });
            }
            else
            {
                //setTokenCookie(response.RefreshToken);
                return Ok(response);
            }
        }
        [AllowAnonymous]
        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequest request)
        {
            // accept refresh token in request body 
            var token = request.Token;

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _service.RevokeToken(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }



        // helper methods

        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
