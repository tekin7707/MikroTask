using Microsoft.IdentityModel.Tokens;
using Mikro.Task.Services.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserRegisterDto userRegisterDto); 
        Task<UserDto> LoginAsync(UserLoginDto userLoginDto);
    }
}
