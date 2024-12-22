using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto.Models
{
    public class User
    {
        public int? Id {get; set;}
        public string? Name {get; set;}
        public string? Username {get; set;}
        public string? HashPassword{ get; set;}
        public string? JWT{get;set;}
    }
}