using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
   
        [ForeignKey("PermissionId")]
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }




    }
}
