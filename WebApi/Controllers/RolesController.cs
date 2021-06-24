using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services.Roles;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            try
            {
                return Ok(await _service.GetRoles());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            try
            {
                var result = await _service.GetRole(id);

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
        public async Task<ActionResult<Role>> CreateRole(Role Role)
        {
            try
            {
                if (Role == null)
                    return BadRequest();

                var createdRole = await _service.CreateRole(Role);

                return CreatedAtAction(nameof(GetRole),
                    new { id = createdRole.Id }, createdRole);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Role record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(int id, Role Role)
        {
            try
            {
                if (id != Role.Id)
                    return BadRequest("Role ID mismatch");

                var RoleToUpdate = await _service.GetRole(id);

                if (RoleToUpdate == null)
                    return NotFound($"Role with Id = {id} not found");

                return await _service.UpdateRole(Role);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Role data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Role>> DeleteRole(int id)
        {
            try
            {
                var RoleToDelete = await _service.GetRole(id);

                if (RoleToDelete == null)
                {
                    return NotFound($"Role with Id = {id} not found");
                }

                return await _service.DeleteRole(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
