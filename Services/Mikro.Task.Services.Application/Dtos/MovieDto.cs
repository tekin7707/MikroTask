using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Dtos
{
    public class MovieDto:MovieListDto
    {
        public List<CommentDto>? Comments { get; set; }

    }
}
