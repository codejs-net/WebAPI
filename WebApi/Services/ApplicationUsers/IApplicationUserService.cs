using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.ApplicationUsers;
using WebApi.Models.Users;

namespace WebApi.Services.ApplicationUsers
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<User>> GetApplicationUsers(string AppSecrat);
        Task<UserResponse> GetApplicationUser(int id);
        Task<ApplicationUserResponse> CreateApplicationUser(ApplicationUserRequest request);
        Task<ApplicationUser> DeleteApplicationUser(string AppSecrat, int Id);
        Task<bool> IsAppUser(string AppSecrat, int UserId);
        Task<bool> IsValidApp(string AppSecrat);
        Task<bool> IsValidUser(int UserId);
    }
}
