using AutoMapper;
using Mikro.Task.Services.Application.Dtos;
using Mikro.Task.Services.Domain;

namespace Mikro.Task.Services.Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MovieModel, MovieDto>().ReverseMap();
            CreateMap<MovieModel, MovieListDto>().ReverseMap();
            CreateMap<MovieCommentModel, CommentDto>().ReverseMap();
            CreateMap<MovieCommentModel, CommentAddDto>().ReverseMap();
        }
    }
}
