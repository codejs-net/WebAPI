using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class UserDetail
    {
        public int Id { get;set; }
        public string FullName { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
        public DateTime BirthDay { get; set; }
        public int PersonId { get; set; }


    }
}
