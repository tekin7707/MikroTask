using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Helpers;
using Mikro.Task.Services.Application.Services.Interfaces;
using Mikro.Task.Services.Db;
using System.Text.Json;

namespace Mikro.Task.Services.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _movieDbContext;
        private readonly IMapper _mapper;
        private readonly RedisService _redisService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public MovieService(MovieDbContext movieDbContext, IMapper mapper, RedisService redisService, ISendEndpointProvider sendEndpointProvider)
        {
            _movieDbContext = movieDbContext;
            _mapper = mapper;
            _redisService = redisService;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<List<MovieListDto>> GetAllAsync()
        {
            const string redisKey = "movielist";
            List<MovieListDto> result;

            //get from redis
            var redisValue = await _redisService.GetDb().StringGetAsync(redisKey);
            if (!string.IsNullOrEmpty(redisValue))
                return JsonSerializer.Deserialize<List<MovieListDto>>(redisValue);

            var movieList = await _movieDbContext.Movies.OrderBy(x => x.id).ToListAsync();
            if (movieList.Count == 0)
                throw new AppException("Any movie not found");

            result = _mapper.Map<List<MovieListDto>>(movieList);

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

            var comment = _mapper.Map<MovieCommentModel>(commentDto);
            await _movieDbContext.Comments.AddAsync(comment);
            await _movieDbContext.SaveChangesAsync();

            var result = _mapper.Map<CommentDto>(comment);

            //remove movie from redis
            var status = await _redisService.GetDb().KeyDeleteAsync($"movie{commentDto.MovieId}");

            return result;
        }

        public async Task<bool> RecommendMovieAsync(RecommendMovieDto recommendMovieDto)
        {
            var movie = await GetAsync(recommendMovieDto.MovieId);

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:sendemailqueue"));
            RecommendMovieEmailDto model = new RecommendMovieEmailDto { Email = recommendMovieDto.Email, Movie = movie };

            await sendEndpoint.Send<RecommendMovieEmailDto>(model);

            return true;
        }

        public async Task<MovieCollection> GetAllWithPageAsync(int count, int page = 1)
        {
            int skip = page > 0 ? (page - 1) * count : 0;
            var movieList = await GetAllAsync();
            movieList = movieList.Skip(skip).Take(count).ToList();

            var result = new MovieCollection { items = movieList };
            if (movieList.Count >= count)
                result.next = $"http://localhost:5100/movies/{count}/{page + 1}";

            return result;
        }
    }
}
