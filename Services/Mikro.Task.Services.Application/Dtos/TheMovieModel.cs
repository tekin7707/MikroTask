using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Dtos
{
    public class TheMovieModel
    {
        public Dates dates { get; set; }
        public int page { get; set; }
        public List<Result> results { get; set; }
    }

}
