using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services.AppRoles;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppRolesController : ControllerBase
    {
        private IAppRoleService _service;

        public AppRolesController(IAppRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAppRoles()
        {
            try
            {
                return Ok(await _service.GetAppRoles());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppRole>> GetAppRole(int id)
        {
            try
            {
                var result = await _service.GetAppRole(id);

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
        public async Task<ActionResult<AppRole>> CreateAppRole(AppRole AppRole)
        {
            try
            {
                if (AppRole == null)
                    return BadRequest();

                var createdAppRole = await _service.CreateAppRole(AppRole);

                return CreatedAtAction(nameof(GetAppRole),
                    new { id = createdAppRole.Id }, createdAppRole);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new AppRole record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppRole>> UpdateAppRole(int id, AppRole AppRole)
        {
            try
            {
                if (id != AppRole.Id)
                    return BadRequest("AppRole ID mismatch");

                var AppRoleToUpdate = await _service.GetAppRole(id);

                if (AppRoleToUpdate == null)
                    return NotFound($"AppRole with Id = {id} not found");

                return await _service.UpdateAppRole(AppRole);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating AppRole data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AppRole>> DeleteAppRole(int id)
        {
            try
            {
                var AppRoleToDelete = await _service.GetAppRole(id);

                if (AppRoleToDelete == null)
                {
                    return NotFound($"AppRole with Id = {id} not found");
                }

                return await _service.DeleteAppRole(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
