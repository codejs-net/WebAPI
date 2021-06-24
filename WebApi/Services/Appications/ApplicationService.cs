using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;

namespace WebApi.Services.Appications
{
    public class ApplicationService:IApplicationService
    {
        private AppDbContext _context;

        public ApplicationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Application>> GetApplications()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<Application> GetApplication(int Id)
        {
            return await _context.Applications
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<Application> CreateApplication(Application Application)
        {
            var result = await _context.Applications.AddAsync(Application);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Application> UpdateApplication(Application Application)
        {
            var result = await _context.Applications
                .FirstOrDefaultAsync(e => e.Id == Application.Id);

            if (result != null)
            {
                result.AppName_si = Application.AppName_si;
                result.AppName_ta = Application.AppName_ta;
                result.AppName_en = Application.AppName_en;
                result.AppSecret = Application.AppSecret;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Application> DeleteApplication(int Id)
        {
            var result = await _context.Applications
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _context.Applications.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
