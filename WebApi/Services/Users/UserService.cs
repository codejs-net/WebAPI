using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;
using WebApi.Models.Users;
using WebApi.Models.Validation;
using WebApi.Utilities;

namespace WebApi.Services.Users
{
    public class UserService:IUserService
    {
        private AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserGetResponse>> GetUsers(string AppSecrat)
        {
            var responseList = new List<UserGetResponse>();
            var app = await _context.Applications.FirstOrDefaultAsync(e => e.AppSecret == AppSecrat);
           
            var userlist = await _context.AppRoles
                .Where(x => x.ApplicationId == app.Id)
                .Include(i => i.Role)
                .Include(i => i.User)
                .ToListAsync();
            foreach (var item in userlist)
            {
                var response = new UserGetResponse();
                response.Id = item.User.Id;
                response.Username = item.User.Username;
                //response.Email = item.User.Email;
                response.Role = item.Role;
                responseList.Add(response);
            }
           
            return responseList;
        }

        public async Task<User> GetUser(int Id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<UserResponse> CreateUser(UserRequest request)
        {
            var response = new UserResponse();
            var User = new User();
            var UserDetail = new UserDetail();

            response.Error = await ExistsValidetion(request);
            if (response.Error.Count==0)
            {
                UserDetail.FullName = request.FullName;
                UserDetail.Email = request.Email;
                UserDetail.Mobile = request.Mobile;
                UserDetail.IdCard = request.IdCard;
                UserDetail.PersonId = request.PersonId;
                await _context.UserDetails.AddAsync(UserDetail);
                await _context.SaveChangesAsync();
                //--------------------------
                PasswordHash.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User.PasswordHash = passwordHash;
                User.PasswordSalt = passwordSalt;
                User.Username = request.Username;
                User.UserDetailId = UserDetail.Id;
                await _context.Users.AddAsync(User);
                await _context.SaveChangesAsync();

                response.Message = "User Create Success";
                response.Data = User;
            }
            else
            {
                response.Success = false;
                response.Message = "User Create Faild";
            }
            return response;
        }

        public async Task<User> UpdateUser(User User)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(e => e.Id == User.Id);

            if (result != null)
            {
                result.Username = User.Username;
                //result.Email = User.Email;
                //result.PersonId = User.PersonId;
                result.Status = User.Status;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<User> DeleteUser(int Id)
        {
            var result = await _context.Users
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _context.Users.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> EmailExists(string email)
        {
            if (await _context.UserDetails.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IdCardExists(string idCard)
        {
            if (await _context.UserDetails.AnyAsync(x => x.IdCard.ToLower() == idCard.ToLower()))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> MobileExists(int mobile)
        {
            if (await _context.UserDetails.AnyAsync(x => x.Mobile == mobile))
            {
                return true;
            }
            return false;
        }
        public async Task<List<Error>> ExistsValidetion(UserRequest request)
        {
            var errorlist = new List<Error>();
            if (await _context.Users.AnyAsync(x => x.Username == request.Username))
            {
                var error = new Error();
                error.Name = "UserName";error.ErrorMessage = "UserName Allrady Exists";
                errorlist.Add(error);
            }
            if (await _context.UserDetails.AnyAsync(x => x.Email == request.Email))
            {
                var error = new Error();
                error.Name = "Email"; error.ErrorMessage = "Email Allrady Exists";
                errorlist.Add(error);
            }
            if (await _context.UserDetails.AnyAsync(x => x.Mobile == request.Mobile))
            {
                var error = new Error();
                error.Name = "Mobile"; error.ErrorMessage = "Mobile Allrady Exists";
                errorlist.Add(error);
            }
            if (await _context.UserDetails.AnyAsync(x => x.IdCard == request.IdCard))
            {
                var error = new Error();
                error.Name = "IdCard"; error.ErrorMessage = "Idintity Card No Allrady Exists";
                errorlist.Add(error);
            }
            return errorlist;
        }
    }
}
