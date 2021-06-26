using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services.RolePermissions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionsController : ControllerBase
    {
        private IRolePermissionService _service;

        public RolePermissionsController(IRolePermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetRolePermissions()
        {
            try
            {
                return Ok(await _service.GetRolePermissions());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RolePermission>> GetRolePermission(int id)
        {
            try
            {
                var result = await _service.GetRolePermission(id);

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
        public async Task<ActionResult<RolePermission>> CreateRolePermission(RolePermission RolePermission)
        {
            try
            {
                if (RolePermission == null)
                    return BadRequest();

                var createdRolePermission = await _service.CreateRolePermission(RolePermission);

                return CreatedAtAction(nameof(GetRolePermission),
                    new { id = createdRolePermission.Id }, createdRolePermission);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new RolePermission record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RolePermission>> UpdateRolePermission(int id, RolePermission RolePermission)
        {
            try
            {
                if (id != RolePermission.Id)
                    return BadRequest("RolePermission ID mismatch");

                var RolePermissionToUpdate = await _service.GetRolePermission(id);

                if (RolePermissionToUpdate == null)
                    return NotFound($"RolePermission with Id = {id} not found");

                return await _service.UpdateRolePermission(RolePermission);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating RolePermission data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<RolePermission>> DeleteRolePermission(int id)
        {
            try
            {
                var RolePermissionToDelete = await _service.GetRolePermission(id);

                if (RolePermissionToDelete == null)
                {
                    return NotFound($"RolePermission with Id = {id} not found");
                }

                return await _service.DeleteRolePermission(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
