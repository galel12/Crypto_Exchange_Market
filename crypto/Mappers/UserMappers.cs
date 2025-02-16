using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto.Dtos;
using crypto.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace crypto.Mappers
{
    public class UserMappers
    {
        public static UserResponseDto MapToUserResponseDto(User user)
        {
            return new UserResponseDto(user.Id, user.Username, user.DateCreated);
        }
    }
}