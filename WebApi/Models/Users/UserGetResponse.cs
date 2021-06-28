using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Models.Users
{
    public class UserGetResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserDetail UserDetail { get; set; }
        public Role Role { get; set; }

    }
}
