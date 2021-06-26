using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.App;

namespace WebApi.Utilities
{
    public class validation
    {
        public  AppDbContext _context;

        public validation(AppDbContext context)
        {
            _context = context;
        }
      
    }
}
