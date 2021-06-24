using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRole(int Id);
        Task<Role> CreateRole(Role Role);
        Task<Role> UpdateRole(Role Role);
        Task<Role> DeleteRole(int Id);
    }
}
