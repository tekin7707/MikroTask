using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Helpers;
using Mikro.Task.Services.Application.Models;
using Mikro.Task.Services.Application.Services.Interfaces;
using Mikro.Task.Services.Db;
using Mikro.Task.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _movieDbContext;
        private readonly IMapper _mapper;

        public MovieService(MovieDbContext movieDbContext, IMapper mapper)
        {
            _movieDbContext = movieDbContext;
            _mapper = mapper;
        }

        public async Task<List<MovieListDto>> GetAllAsync()
        {
            var movieList = await _movieDbContext.Movies.ToListAsync();
            if (movieList.Count == 0)
                throw new AppException("Any movie not found");

            return _mapper.Map<List<MovieListDto>>(movieList);
        }

        public async Task<MovieDto> GetAsync(int id)
        {
            var movie = await _movieDbContext.Movies.Include(x => x.Comments).FirstOrDefaultAsync(x => x.id == id);
            if (movie == null)
                throw new AppException("Movie not found");

            var result = _mapper.Map<MovieDto>(movie);
            return result;
        }

        public async Task<CommentDto> AddCommentAsync(CommentAddDto commentDto)
        {
            var movie = await _movieDbContext.Movies.FirstOrDefaultAsync(x => x.id == commentDto.MovieId);
            if (movie == null)
                throw new AppException("Movie not found");

            movie.vote_user = commentDto.Point;
            _movieDbContext.Movies.Update(movie);

            var comment = _mapper.Map<MovieCommentModel>(commentDto);
            await _movieDbContext.Comments.AddAsync(comment);
            await _movieDbContext.SaveChangesAsync();

            return _mapper.Map<CommentDto>(comment);
        }

        public Task<NoContent> RecommendMovieAsync(RecommendMovieDto recommendMovieDto)
        {
            throw new NotImplementedException();
        }


    }
}
