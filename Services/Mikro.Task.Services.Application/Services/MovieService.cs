using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Helpers;
using Mikro.Task.Services.Application.Models;
using Mikro.Task.Services.Application.Services.Interfaces;
using Mikro.Task.Services.Db;
using Mikro.Task.Services.Domain;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _movieDbContext;
        private readonly IMapper _mapper;
        private readonly RedisService _redisService;

        public MovieService(MovieDbContext movieDbContext, IMapper mapper, RedisService redisService)
        {
            _movieDbContext = movieDbContext;
            _mapper = mapper;
            _redisService = redisService;
        }

        public async Task<List<MovieListDto>> GetAllAsync()
        {
            const string redisKey = "movielist";
            List<MovieListDto> result;

            //get from redis
            var redisValue = await _redisService.GetDb().StringGetAsync(redisKey);
            if (!string.IsNullOrEmpty(redisValue))
                return JsonSerializer.Deserialize<List<MovieListDto>>(redisValue);

            var movieList = await _movieDbContext.Movies.ToListAsync();
            if (movieList.Count == 0)
                throw new AppException("Any movie not found");

            result =_mapper.Map<List<MovieListDto>>(movieList);

            //set to redis
            await _redisService.GetDb().StringSetAsync(redisKey, JsonSerializer.Serialize(result));

            return result;
        }

        public async Task<MovieDto> GetAsync(int id)
        {
            //get from redis
            var redisValue = await _redisService.GetDb().StringGetAsync($"movie{id}");
            if (!string.IsNullOrEmpty(redisValue))
                return JsonSerializer.Deserialize<MovieDto>(redisValue);

            var movie = await _movieDbContext.Movies.Include(x => x.Comments).FirstOrDefaultAsync(x => x.id == id);
            if (movie == null)
                throw new AppException("Movie not found");

            var result = _mapper.Map<MovieDto>(movie);

            //set to redis
            await _redisService.GetDb().StringSetAsync($"movie{id}", JsonSerializer.Serialize(result));

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

            var result =_mapper.Map<CommentDto>(comment);

            //remove movie from redis
            var status = await _redisService.GetDb().KeyDeleteAsync($"movie{commentDto.MovieId}");

            return result;
        }

        public Task<NoContent> RecommendMovieAsync(RecommendMovieDto recommendMovieDto)
        {
            throw new NotImplementedException();
        }

    }
}
