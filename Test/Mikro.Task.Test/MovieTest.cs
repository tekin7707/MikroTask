using Mikro.Task.Services.Application.Services.Interfaces;
using Mikro.Task.Services.Application.Services;
using Mikro.Task.Services.Db;
using Mikro.Task.Test.Helpers;
using AutoMapper;
using Mikro.Task.Services.Application.Helpers;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Test.TestDatas;

namespace MikroTask.Test
{
    public class MovieTest
    {
        private readonly IMovieService _movieService;
        private readonly MovieDbContext _contextMock;

        public MovieTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            _contextMock = MockDBContext.Get();
            _movieService = new MovieService(_contextMock, mapper,null);
        }

        [Fact]
        public async Task GetAllMovieAsync()
        {
            var response = await _movieService.GetAllAsync();
            Assert.NotEmpty(response);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public async Task GetMovieAsync(int id)
        {
            try
            {
                var response = await _movieService.GetAsync(id);
                Assert.Equal(response.id,id);
            }
            catch (AppException ex)
            {
                Assert.Equal("Movie not found", ex.Message);
            }
            catch (Exception ex)
            {
                throw new Xunit.Sdk.XunitException(ex.Message);
            }
        }

        [Theory]
        [ClassData(typeof(AddCommentTest))]
        public async Task AddCommentTestAsync(CommentAddDto commentAddDto,string comment)
        {
            try
            {
                var response = await _movieService.AddCommentAsync(commentAddDto);
                Assert.Equal(response.Comment, comment);
            }
            catch (AppException ex)
            {
                Assert.Equal("Movie not found", ex.Message);
            }
            catch (Exception ex)
            {
                throw new Xunit.Sdk.XunitException(ex.Message);
            }

        }
    }
}