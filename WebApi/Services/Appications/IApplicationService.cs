using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Applications;

namespace WebApi.Services.Appications
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetApplications();
        Task<Application> GetApplication(int Id);
        Task<ApplicationResponse> CreateApplication(ApplicationRequest request);
        Task<ApplicationResponse> UpdateApplication(int id,ApplicationRequest request);
        Task<ApplicationResponse> ChangeApplicationSecret(int Id);
        Task<Application> DeleteApplication(int Id);
    }
}
