using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;

namespace WebApi.Services.Roles
{
    public class RoleService:IRoleService
    {
        private AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRole(int Id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<Role> CreateRole(Role Role)
        {
            var result = await _context.Roles.AddAsync(Role);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Role> UpdateRole(Role Role)
        {
            var result = await _context.Roles
                .FirstOrDefaultAsync(e => e.Id == Role.Id);

            if (result != null)
            {
                result.ApplicationId = Role.ApplicationId;
                result.Role_si = Role.Role_si;
                result.Role_ta = Role.Role_ta;
                result.Role_en = Role.Role_en;
                result.Level = Role.Level;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Role> DeleteRole(int Id)
        {
            var result = await _context.Roles
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _context.Roles.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
