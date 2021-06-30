using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Models.ApplicationUsers
{
    public class ApplicationUserResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public ApplicationUser Data { get; set; }
    }
}
