using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Auth;
using WebApi.Models.Users;

namespace WebApi.Services.Users
{
    public interface IUserService
    {
        Task<User> GetUser(int Id);
        Task<UserResponse> CreateUser(UserRequest UserRequest);
        Task<UserResponse> UpdateUser(UserRequest request, int userid);
        Task<User> DeleteUser(int Id);
        Task<bool> UserExists(string username);
        Task<bool> EmailExists(string email);
        Task<bool> IdCardExists(string idCard);
        Task<bool> MobileExists(string mobile);
    }
}
