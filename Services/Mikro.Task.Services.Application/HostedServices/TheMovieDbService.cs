using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Services;
using MikroTask.Services.Application.HostedServices;
using System.Net.Http.Json;

namespace MikroTask.Services.Api.HostedServices
{
    public class TheMovieDbService : HostedService
    {
        private readonly HttpClient _httpClient;
        private readonly ITheMovieService _movieService;
        private const string Api_Url = "https://api.themoviedb.org/3/movie/now_playing?api_key=b7c93b8bf743d79f1827cc38e0b396fd&language=en-US";

        public TheMovieDbService(HttpClient httpClient, ITheMovieService movieService)
        {
            _httpClient = httpClient;
            _movieService = movieService;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                var response = await _httpClient.GetAsync(Api_Url, cToken);
                if (response.IsSuccessStatusCode)
                {
                    var movieModel = await response.Content.ReadFromJsonAsync<TheMovieModel>();
                    foreach (var movie in movieModel.results)
                    {
                        //var r = await _movieService.AddAsync(movie);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(60), cToken);
            }
        }
    }
}
