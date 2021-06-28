using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Validation;

namespace WebApi.Models.Users
{
    public class UserResponse
    {  
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public List<Error> Error { get; set; }
        //public ICollection<Error> Error { get; set; }
        public User Data { get; set; }
    }
}
