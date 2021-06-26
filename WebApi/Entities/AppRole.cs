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
        [ForeignKey("ApplicationId")]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public string Verify { get; set; }

        //[ForeignKey("VerifidBy")]
        public int VerifidBy { get; set; }
        //public User VerifiedUser { get; set; }


    }
}
