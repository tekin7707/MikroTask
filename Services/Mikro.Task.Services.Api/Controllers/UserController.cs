using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mikro.Task.Services.Application.Dtos.User;
using Mikro.Task.Services.Application.Helpers;
using Mikro.Task.Services.Application.Services.Interfaces;
using Mikro.Task.Services.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mikro.Task.Services.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterAsync([FromBody] UserRegisterDto userRegisterDto)
        {
            await _userService.RegisterAsync(userRegisterDto);
            return Ok("User created successfully!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userService.LoginAsync(userLoginDto);
            if (user != null)
                return Ok(user);

            return Unauthorized();
        }

    }
}