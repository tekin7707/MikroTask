using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Services.Interfaces
{
    public interface IMovieService
    {
        Task<List<MovieListDto>> GetAllAsync();
        Task<MovieDto> GetAsync(int id);
        Task<CommentDto> AddCommentAsync(CommentAddDto commentDto);
        Task<NoContent> RecommendMovieAsync(RecommendMovieDto recommendMovieDto);

    }
}
