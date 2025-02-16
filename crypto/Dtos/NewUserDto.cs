using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crypto.Dtos
{
    public record NewUserDto
    {
    [Required]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
    public string Username { get; init; }

    [Required]
    [MinLength(3, ErrorMessage = "Password must be at least 3 characters long.")]
    public string Password { get; init; }
    }
}