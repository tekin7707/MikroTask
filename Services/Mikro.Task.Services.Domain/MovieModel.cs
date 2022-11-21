using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mikro.Task.Services.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Domain
{
    public class MovieModel
    {
        [Key]
        public int id { get; set; }
        public int the_movie_id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public int? vote_user { get; set; }
        public virtual IEnumerable<MovieCommentModel> Comments { get; set; }


    }
}
