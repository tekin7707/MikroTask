using Microsoft.Extensions.DependencyInjection;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Services;
using Mikro.Task.Services.Db;
using Mikro.Task.Services.Domain;
using MikroTask.Services.Application.HostedServices;
using System.Net.Http.Json;

namespace MikroTask.Services.Api.HostedServices
{
    public class TheMovieDbService : HostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly HttpClient _httpClient;
        private const string Api_Url = "https://api.themoviedb.org/3/movie/now_playing?api_key=b7c93b8bf743d79f1827cc38e0b396fd&language=en-US";

        public TheMovieDbService(HttpClient httpClient, IServiceScopeFactory serviceScopeFactory)
        {
            _httpClient = httpClient;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            MovieDbContext _db;
            ITheMovieService _movieService;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _db = scope.ServiceProvider.GetService<MovieDbContext>();
                _movieService = new TheMovieService(_db);
                while (!cToken.IsCancellationRequested)
                {
                    var response = await _httpClient.GetAsync(Api_Url, cToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var theMovieModel = await response.Content.ReadFromJsonAsync<TheMovieModel>();
                        var movies = theMovieModel.results.Where(x => !_db.Movies.Select(p => p.the_movie_id).Contains(x.id)).ToList();
                        await _movieService.AddRangeAsync(movies);
                    }
                    await Task.Delay(TimeSpan.FromSeconds(3600), cToken);
                }
            }
        }
    }
}
