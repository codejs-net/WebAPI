using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationId")]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public string Action { get; set; }
        public string Permissions { get; set; }



    }
}
