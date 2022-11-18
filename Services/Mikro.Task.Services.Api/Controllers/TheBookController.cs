using Microsoft.AspNetCore.Mvc;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Services;
using MikroTask.Services.Api.HostedServices;
using System.Net.Http;

namespace Mikro.Task.Services.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheBookController : ControllerBase
    {
        private readonly ILogger<TheBookController> _logger;
        private readonly ITheMovieService _theMovieService;
        private readonly HttpClient _httpClient;
        private const string Api_Url = "https://api.themoviedb.org/3/movie/now_playing?api_key=b7c93b8bf743d79f1827cc38e0b396fd&language=en-US";

        public TheBookController(ILogger<TheBookController> logger, ITheMovieService theMovieService, HttpClient httpClient)
        {
            _logger = logger;
            _theMovieService = theMovieService;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<string> GetAsync()
        {
            var response = await _httpClient.GetAsync(Api_Url);
            if (response.IsSuccessStatusCode)
            {
                var movieModel = await response.Content.ReadFromJsonAsync<TheMovieModel>();
                foreach (var movie in movieModel.results)
                {
                    var r = await _theMovieService.AddAsync(movie);
                }
            }
            return "";
        }
    }
}