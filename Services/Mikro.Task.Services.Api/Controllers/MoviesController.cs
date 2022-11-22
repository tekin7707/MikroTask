using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Application.Services;
using Mikro.Task.Services.Application.Services.Interfaces;

namespace Mikro.Task.Services.Api.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result =  await _movieService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _movieService.GetAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("/Recommend")]
        public async Task<IActionResult> RecommendAsync(RecommendMovieDto recommendMovieDto)
        {
            var result = await _movieService.RecommendMovieAsync(recommendMovieDto);
            return Ok(result ? "The movie recommended.":"Unexpected error");
        }

        [HttpPost]
        [Route("/Comment")]
        public async Task<IActionResult> AddCommentAsync(CommentAddDto commentAddDto)
        {
            var result = await _movieService.AddCommentAsync(commentAddDto);
            return Ok(result);
        }

    }
}
