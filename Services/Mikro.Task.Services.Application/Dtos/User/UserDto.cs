using System.ComponentModel.DataAnnotations;

namespace Mikro.Task.Services.Application.Dtos.User
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
