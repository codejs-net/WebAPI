using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        public string Role_si { get; set; }
        public string Role_ta { get; set; }
        public string Role_en { get; set; }
        public string Level { get; set; }

    }
}
