using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;

namespace WebApi.Services.RolePermissions
{
    public class RolePermissionService:IRolePermissionService
    {
        private AppDbContext _context;

        public RolePermissionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RolePermission>> GetRolePermissions()
        {
            return await _context.RolePermissions.ToListAsync();
        }

        public async Task<RolePermission> GetRolePermission(int Id)
        {
            return await _context.RolePermissions
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<RolePermission> CreateRolePermission(RolePermission RolePermission)
        {
            var result = await _context.RolePermissions.AddAsync(RolePermission);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<RolePermission> UpdateRolePermission(RolePermission RolePermission)
        {
            var result = await _context.RolePermissions
                .FirstOrDefaultAsync(e => e.Id == RolePermission.Id);

            if (result != null)
            {
                result.RoleId = RolePermission.RoleId;
                result.PermissionId = RolePermission.PermissionId;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<RolePermission> DeleteRolePermission(int Id)
        {
            var result = await _context.RolePermissions
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _context.RolePermissions.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
