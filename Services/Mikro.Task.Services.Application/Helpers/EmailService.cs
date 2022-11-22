using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Services;
using Mikro.Task.Services.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Helpers
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(RecommendMovieEmailDto recommendMovieEmailDto);
    }

    public class EmailService: IEmailService
    {
        public async Task<bool> SendEmailAsync(RecommendMovieEmailDto recommendMovieEmailDto)
        {//mock
            return true;
        }
    }
}
