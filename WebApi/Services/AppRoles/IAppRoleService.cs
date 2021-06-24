using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services.AppRoles
{
    public interface IAppRoleService
    {
        Task<IEnumerable<AppRole>> GetAppRoles();
        Task<AppRole> GetAppRole(int Id);
        Task<AppRole> CreateAppRole(AppRole AppRole);
        Task<AppRole> UpdateAppRole(AppRole AppRole);
        Task<AppRole> DeleteAppRole(int Id);
    }
}
