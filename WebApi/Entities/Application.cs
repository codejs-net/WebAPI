using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public string AppName_si { get; set; }
        public string AppName_ta { get; set; }
        public string AppName_en { get; set; }
        public string AppSecret { get; set; }


    }
}
