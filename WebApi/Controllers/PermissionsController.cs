using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services.Permissions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private IPermissionService _service;

        public PermissionsController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetPermissions()
        {
            try
            {
                return Ok(await _service.GetPermissions());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Permission>> GetPermission(int id)
        {
            try
            {
                var result = await _service.GetPermission(id);

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
        public async Task<ActionResult<Permission>> CreatePermission(Permission Permission)
        {
            try
            {
                if (Permission == null)
                    return BadRequest();

                var createdPermission = await _service.CreatePermission(Permission);

                return CreatedAtAction(nameof(GetPermission),
                    new { id = createdPermission.Id }, createdPermission);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Permission record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Permission>> UpdatePermission(int id, Permission Permission)
        {
            try
            {
                if (id != Permission.Id)
                    return BadRequest("Permission ID mismatch");

                var PermissionToUpdate = await _service.GetPermission(id);

                if (PermissionToUpdate == null)
                    return NotFound($"Permission with Id = {id} not found");

                return await _service.UpdatePermission(Permission);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Permission data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Permission>> DeletePermission(int id)
        {
            try
            {
                var PermissionToDelete = await _service.GetPermission(id);

                if (PermissionToDelete == null)
                {
                    return NotFound($"Permission with Id = {id} not found");
                }

                return await _service.DeletePermission(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
