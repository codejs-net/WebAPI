using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services.RolePermissions
{
    public interface IRolePermissionService
    {
        Task<IEnumerable<RolePermission>> GetRolePermissions();
        Task<RolePermission> GetRolePermission(int Id);
        Task<RolePermission> CreateRolePermission(RolePermission RolePermission);
        Task<RolePermission> UpdateRolePermission(RolePermission RolePermission);
        Task<RolePermission> DeleteRolePermission(int Id);
    }
}
