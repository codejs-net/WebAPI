using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Models.Users
{
    public class UserResponse
    {   public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public User Data { get; set; }
    }
}
