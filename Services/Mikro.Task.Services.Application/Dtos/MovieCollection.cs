using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Dtos
{
    public class MovieCollection
    {
        public string next { get; set; }
        public List<MovieListDto> items { get; set; }
    }

}
