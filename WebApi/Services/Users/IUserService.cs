﻿using System;
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
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int Id);
        Task<UserResponse> CreateUser(User User,string password);
        Task<User> UpdateUser(User User);
        Task<User> DeleteUser(int Id);
    }
}
