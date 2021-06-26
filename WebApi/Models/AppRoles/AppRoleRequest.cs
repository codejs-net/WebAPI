using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.AppRoles
{
    public class AppRoleRequest
    {
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Verify { get; set; }
        public int VerifidBy { get; set; }

    }
}
