using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;

namespace WebApi.Services.AppRoles
{
    public class AppRoleService:IAppRoleService
    {
        private AppDbContext _context;

        public AppRoleService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppRole>> GetAppRoles()
        {
            return await _context.AppRoles.ToListAsync();
        }

        public async Task<AppRole> GetAppRole(int Id)
        {
            return await _context.AppRoles
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<AppRole> CreateAppRole(AppRole AppRole)
        {
            var result = await _context.AppRoles.AddAsync(AppRole);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<AppRole> UpdateAppRole(AppRole AppRole)
        {
            var result = await _context.AppRoles
                .FirstOrDefaultAsync(e => e.Id == AppRole.Id);

            if (result != null)
            {
                _context.Entry(AppRole).State = EntityState.Modified;
                //result.ApplicationId = AppRole.ApplicationId;
                //result.RoleId = AppRole.RoleId;
                //result.UserId = AppRole.UserId;
                //result.Verify = AppRole.Verify;
                //result.VerifidBy = AppRole.VerifidBy;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<AppRole> DeleteAppRole(int Id)
        {
            var result = await _context.AppRoles
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _context.AppRoles.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
