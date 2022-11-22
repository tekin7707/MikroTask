using Mikro.Task.Services.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Services.Interfaces
{
    public interface ITheMovieService
    {
        Task<bool> AddRangeAsync(List<Result> model);

    }
}
