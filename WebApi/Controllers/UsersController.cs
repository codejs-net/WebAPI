using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Models.Auth;
using WebApi.Models.Users;
using WebApi.Services.Users;

namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                return Ok(await _service.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var result = await _service.GetUser(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserRequest request)
        {
            try
            {
                if (User == null)
                    return BadRequest();
                var createdUser = await _service.CreateUser(new User 
                { 
                  Username = request.Username, 
                  Email = request.Email ,
                  PersonId=request.PersonId,
                  Status=request.Status
                },
                request.Password);
                if (!createdUser.Success)
                {
                    return BadRequest(new { message = createdUser.Message });
                }
                   return CreatedAtAction(nameof(GetUser), new { id = createdUser.Data.Id }, createdUser.Data);
                
               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new User record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User User)
        {
            try
            {
                if (id != User.Id)
                    return BadRequest("User ID mismatch");

                var UserToUpdate = await _service.GetUser(id);

                if (UserToUpdate == null)
                    return NotFound($"User with Id = {id} not found");

                return await _service.UpdateUser(User);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating User data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                var UserToDelete = await _service.GetUser(id);

                if (UserToDelete == null)
                {
                    return NotFound($"User with Id = {id} not found");
                }

                return await _service.DeleteUser(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

    }
}
