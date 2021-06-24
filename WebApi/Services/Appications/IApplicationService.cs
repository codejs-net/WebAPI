using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services.Appications
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetApplications();
        Task<Application> GetApplication(int Id);
        Task<Application> CreateApplication(Application Application);
        Task<Application> UpdateApplication(Application Application);
        Task<Application> DeleteApplication(int Id);
    }
}
