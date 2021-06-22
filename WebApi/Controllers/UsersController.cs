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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IAuthService _service;

        public UsersController(IAuthService service)
        {
            _service = service;
        }

    }
}
