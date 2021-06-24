using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.App;
using WebApi.Entities;
using WebApi.Models.Users;
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
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int Id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<UserResponse> CreateUser(User User,string password)
        {
            UserResponse response = new UserResponse();
            if (await UserExists(User.Username))
            {
                response.Success = false;
                response.Message = "User Allready Exists";
            }
            else
            {
                PasswordHash.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                User.PasswordHash = passwordHash;
                User.PasswordSalt = passwordSalt;

                await _context.Users.AddAsync(User);
                await _context.SaveChangesAsync();
                response.Message = "User Create Success";
                response.Data = User;
               
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
                result.Email = User.Email;
                result.PersonId = User.PersonId;
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
    }
}
