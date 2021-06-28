using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;
using WebApi.Models.Applications;
using WebApi.Utilities;

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

        public async Task<ApplicationResponse> CreateApplication(Application Application)
        {
            ApplicationResponse response = new ApplicationResponse();
            Application.AppSecret = await GenerateAppSecert();
            await _context.Applications.AddAsync(Application);
            await _context.SaveChangesAsync();

            response.Message = "Application Create Success";
            response.Data = Application; 
            return response;
        }

        public async Task<ApplicationResponse> UpdateApplication(Application Application)
        {
            ApplicationResponse response = new ApplicationResponse();
            var result = await _context.Applications.FirstOrDefaultAsync(e => e.Id == Application.Id);
            if (result != null)
            {
                result.AppName_si = Application.AppName_si;
                result.AppName_ta = Application.AppName_ta;
                result.AppName_en = Application.AppName_en;

                await _context.SaveChangesAsync();
                response.Message = "Application Update Success";
                response.Data = result; 
            }
            else
            {
                response.Message = "Application Not Found";
                response.Success = false;
            }
            return response;
        }

        public async Task<ApplicationResponse> ChangeApplicationSecret(int Id)
        {
            ApplicationResponse response = new ApplicationResponse();
            var result = await _context.Applications.FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                result.AppSecret = await GenerateAppSecert();
                await _context.SaveChangesAsync();
                response.Message = "Application Secret Update Success";
                response.Data = result;
            }
            else
            {
                response.Message = "Application Not Found";
                response.Success = false;
            }
            return response;
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

        public async Task<string> GenerateAppSecert()
        {
            string secret = Generate.RandomString(20);
            bool exsit = true;
            while (exsit == true)
            {
                if (await _context.Applications.AnyAsync(x => x.AppSecret == secret))
                { secret = Generate.RandomString(20); }
                else
                {exsit = false;}
            }
            return secret;
        }
    }
}
