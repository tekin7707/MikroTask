using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Dtos
{
    public class TheMovieCollection
    {
        public TheMovieDates dates { get; set; }
        public int page { get; set; }
        public List<TheMovieModel> results { get; set; }
    }

}
