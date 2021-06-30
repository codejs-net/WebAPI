using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;
using WebApi.Models.ApplicationUsers;
using WebApi.Models.Users;
using WebApi.Services.ApplicationUsers;


namespace WebApi.Services.ApplicationUsers
{
    public class ApplicationUserService:IApplicationUserService
    {
        private AppDbContext _context;

        public ApplicationUserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetApplicationUsers(string AppSecrat)
        {
            var responseList = new List<User>();
            var app = await _context.Applications.FirstOrDefaultAsync(e => e.AppSecret == AppSecrat);

            var userlist = await _context.ApplicationUsers
                .Where(x => x.ApplicationId == app.Id)
                .Include(i => i.User)
                .ThenInclude(j => j.UserDetail)
                .ToListAsync();
            foreach (var item in userlist)
            {
                //var response = new User();
                //response.Data = item.User;
                responseList.Add(item.User);
            }

            return responseList;
        }

        public async Task<UserResponse> GetApplicationUser(int id)
        {
            var response = new UserResponse();
            var User = await _context.Users
                .Include(i => i.UserDetail)
                .FirstOrDefaultAsync(e => e.Id == id);

            response.Data = User;
            return response;
        }

        public async Task<ApplicationUserResponse> CreateApplicationUser(ApplicationUserRequest request)
        {
            var app= await _context.Applications.FirstOrDefaultAsync(e => e.AppSecret == request.AppSecret);
            var response = new ApplicationUserResponse();
            var ApplicationUser = new ApplicationUser();
            ApplicationUser.ApplicationId = app.Id;
            ApplicationUser.UserId = request.UserId;

            await _context.ApplicationUsers.AddAsync(ApplicationUser);
            await _context.SaveChangesAsync();

            var CreatedAppUser= await _context.ApplicationUsers
                .Include(i => i.User)
                .ThenInclude(j=>j.UserDetail)
                .FirstOrDefaultAsync(e => e.Id == ApplicationUser.Id);

            response.Message = "Application User Create Success";
            response.Data = CreatedAppUser;
            return response;
        }

 
        public async Task<ApplicationUser> DeleteApplicationUser(string AppSecret, int id)
        {
            var app = await _context.Applications.FirstOrDefaultAsync(e => e.AppSecret ==AppSecret);
            var result = await _context.ApplicationUsers
                .FirstOrDefaultAsync(e => e.UserId == id && e.ApplicationId==app.Id);
            if (result != null)
            {
                _context.ApplicationUsers.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }
        public async Task<bool> IsAppUser(string AppSecrat, int UserId)
        {
            var app = await _context.Applications.FirstOrDefaultAsync(e => e.AppSecret == AppSecrat);
            return await _context.ApplicationUsers.AnyAsync(x => x.ApplicationId == app.Id && x.UserId == UserId);
        }
        public async Task<bool> IsValidApp(string AppSecrat)
        {
            return  await _context.Applications.AnyAsync(e => e.AppSecret == AppSecrat);
        }
        public async Task<bool> IsValidUser(int UserId)
        {
            return await _context.Users.AnyAsync(e => e.Id == UserId);
        }

    }
}
