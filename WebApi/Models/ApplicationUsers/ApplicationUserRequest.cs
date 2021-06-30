using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.ApplicationUsers
{
    public class ApplicationUserRequest
    {
        public int UserId { get; set; }
        public string AppSecret { get; set; }
    }
}
