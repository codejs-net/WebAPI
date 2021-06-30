using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("ApplicationId")]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
        public bool Activate { get; set; } = false;
    }
}
