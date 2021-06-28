using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Applications;
using WebApi.Services.Appications;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
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
        [HttpGet("{id}")]
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
        public async Task<ActionResult<Application>> CreateApplication(ApplicationRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest();
                var createdApplication = await _service.CreateApplication(new Application
                {
                    AppName_si = request.AppName_si,
                    AppName_ta = request.AppName_ta,
                    AppName_en = request.AppName_en,
                });

                return CreatedAtAction(nameof(GetApplication),
                    new { id = createdApplication.Data.Id }, createdApplication.Data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Application record");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> UpdateApplication(int id, ApplicationRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest();

                var updatedApplication = await _service.UpdateApplication(new Application
                {
                    Id=id,
                    AppName_si = request.AppName_si,
                    AppName_ta = request.AppName_ta,
                    AppName_en = request.AppName_en,
                });
                if (!updatedApplication.Success)
                {
                    return BadRequest(new { message = updatedApplication.Message });
                }
                return updatedApplication.Data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Application data");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> ChangeApplicationSecret(int id)
        {
            try
            {
                var updatedApplication = await _service.ChangeApplicationSecret(id);
                if (!updatedApplication.Success)
                {
                    return BadRequest(new { message = updatedApplication.Message });
                }
                return updatedApplication.Data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Application data");
            }
        }

        [HttpDelete("{id}")]
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
