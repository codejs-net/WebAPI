using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.ApplicationUsers;
using WebApi.Models.Users;
using WebApi.Services.ApplicationUsers;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private IApplicationUserService _service;

        public ApplicationUsersController(IApplicationUserService service)
        {
            _service = service;
        }

        [HttpGet("{AppSecrat}")]
        public async Task<ActionResult> GetApplicationUsers(string AppSecrat)
        {
            try
            {
                if (!await _service.IsValidApp(AppSecrat))
                { ModelState.AddModelError(nameof(AppSecrat), "InValid Application"); }

                if (!ModelState.IsValid)
                { return BadRequest(ModelState); }

                var result = await _service.GetApplicationUsers(AppSecrat);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{AppSecrat}/{UserId}")]
        public async Task<ActionResult<UserResponse>> GetApplicationUser(string AppSecrat,int UserId)
        {
            try
            {
                if (!await _service.IsValidApp(AppSecrat))
                { ModelState.AddModelError(nameof(AppSecrat), "InValid Application"); return BadRequest(ModelState); }

                if (!await _service.IsAppUser(AppSecrat, UserId))
                { ModelState.AddModelError(nameof(UserId), "InValid User"); return BadRequest(ModelState); }

                var result = await _service.GetApplicationUser(UserId);
                return Ok(result.Data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationUserResponse>> CreateApplicationUser(ApplicationUserRequest request)
        {
            try
            {
                if (request == null) return BadRequest();

                if (!await _service.IsValidApp(request.AppSecret))
                {ModelState.AddModelError(nameof(request.AppSecret), "InValid Application");}

                if (!await _service.IsValidUser(request.UserId))
                {ModelState.AddModelError(nameof(request.UserId), "InValid User");}

                if (!ModelState.IsValid)
                { return BadRequest(ModelState); }

                if (await _service.IsAppUser(request.AppSecret, request.UserId))
                { 
                    ModelState.AddModelError(nameof(request.UserId), "User Allredy Exsist For Requset Application");
                    return BadRequest(ModelState);
                }

                var createdApplicationUser = await _service.CreateApplicationUser(request);
                return createdApplicationUser;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Application record");
            }
        }

       

        [HttpDelete("{AppSecrat}/{UserId}")]
        public async Task<ActionResult<ApplicationUser>> DeleteApplicationUser(string AppSecrat, int UserId)
        {
            try
            {
                if (!await _service.IsValidApp(AppSecrat))
                { ModelState.AddModelError(nameof(AppSecrat), "InValid Application"); return BadRequest(ModelState); }

                if (!await _service.IsAppUser(AppSecrat, UserId))
                { ModelState.AddModelError(nameof(UserId), "InValid User"); return BadRequest(ModelState); }

                return await _service.DeleteApplicationUser(AppSecrat,UserId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
