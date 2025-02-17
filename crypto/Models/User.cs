using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? HashPassword { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        //Refresh token
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}