using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mikro.Task.Services.Application.Dtos.User;
using Mikro.Task.Services.Application.Helpers;
using Mikro.Task.Services.Application.Services.Interfaces;
using Mikro.Task.Services.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<UserModel> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                };

                var token = GetToken(authClaims);

                return
                    new UserDto
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Email = user.Email
                    };
            }
            throw new NotAuthorizedException();
        }

        public async Task<bool> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var userExists = await _userManager.FindByNameAsync(userRegisterDto.Email);
            if (userExists != null)
                throw new AppException("User already exists!");

            var user = new UserModel()
            {
                Email = userRegisterDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userRegisterDto.Email
            };
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (!result.Succeeded)
                new AppException("User creation failed! Please check user details and try again.");

            return true;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            int expire;
            if (!int.TryParse(_configuration["JWT:Expire"], out expire))
                expire = 24;
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddHours(expire),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
