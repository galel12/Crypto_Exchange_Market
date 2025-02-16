using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto.Dtos
{
    public class UpdateUserDto
    {
        public string? Username {get; init;}
        public string? Password { get; init;}
    }
}