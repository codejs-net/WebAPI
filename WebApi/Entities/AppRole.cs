using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class AppRole
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public Application Application{ get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public string Verify { get; set; }

        public int VerifidBy { get; set; }
        [ForeignKey("VerifidBy")]
        public User VerifiedUserId { get; set; }


    }
}
