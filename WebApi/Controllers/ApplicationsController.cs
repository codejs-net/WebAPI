using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Services.Appications;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private IApplicationService _service;

        public ApplicationsController(IApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetApplications()
        {
            try
            {
                return Ok(await _service.GetApplications());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            try
            {
                var result = await _service.GetApplication(id);

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
        public async Task<ActionResult<Application>> CreateApplication(Application Application)
        {
            try
            {
                if (Application == null)
                    return BadRequest();

                var createdApplication = await _service.CreateApplication(Application);

                return CreatedAtAction(nameof(GetApplication),
                    new { id = createdApplication.Id }, createdApplication);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Application record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> UpdateApplication(int id, Application Application)
        {
            try
            {
                if (id != Application.Id)
                    return BadRequest("Application ID mismatch");

                var ApplicationToUpdate = await _service.GetApplication(id);

                if (ApplicationToUpdate == null)
                    return NotFound($"Application with Id = {id} not found");

                return await _service.UpdateApplication(Application);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Application data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Application>> DeleteApplication(int id)
        {
            try
            {
                var ApplicationToDelete = await _service.GetApplication(id);

                if (ApplicationToDelete == null)
                {
                    return NotFound($"Application with Id = {id} not found");
                }

                return await _service.DeleteApplication(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
