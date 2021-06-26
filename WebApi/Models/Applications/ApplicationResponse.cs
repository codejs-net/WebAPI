using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Models.Applications
{
    public class ApplicationResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public Application Data { get; set; }
    }
}
