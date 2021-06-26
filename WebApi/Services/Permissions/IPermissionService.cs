using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services.Permissions
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission> GetPermission(int Id);
        Task<Permission> CreatePermission(Permission Permission);
        Task<Permission> UpdatePermission(Permission Permission);
        Task<Permission> DeletePermission(int Id);
    }
}
