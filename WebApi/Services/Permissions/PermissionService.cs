using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;

namespace WebApi.Services.Permissions
{
    public class PermissionService:IPermissionService
    {
        private AppDbContext _context;

        public PermissionService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetPermission(int Id)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<Permission> CreatePermission(Permission Permission)
        {
            var result = await _context.Permissions.AddAsync(Permission);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Permission> UpdatePermission(Permission Permission)
        {
            var result = await _context.Permissions
                .FirstOrDefaultAsync(e => e.Id == Permission.Id);

            if (result != null)
            {
                result.ApplicationId = Permission.ApplicationId;
                result.Permissions = Permission.Permissions;
                result.Action = Permission.Action;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Permission> DeletePermission(int Id)
        {
            var result = await _context.Permissions
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _context.Permissions.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
