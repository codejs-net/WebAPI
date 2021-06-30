using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Users
{
    public class UserRequest
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Empty Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Mobile Number")]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        public string IdCard { get; set; }
        public DateTime BirthDay { get; set; }
        public int PersonId { get; set; }
        public string Status { get; set; }



    }
}
