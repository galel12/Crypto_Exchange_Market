using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto.Dtos
{
    public class RefreshTokenRequestDto
    {
        public int Id { get; set; }
        public string? RefreshToken { get; set; }
    }
}