using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mikro.Task.Services.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Dtos
{
    public class MovieCommentModel
    {
        public int Id { get; set; }

        [ForeignKey("MovieModel")]
        public int MovieId { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength]
        public string? Comment { get; set; }

        [Range(0, 10, ErrorMessage = "Score must be in between 1 and 10")]
        public int Score { get; set; }

        public virtual MovieModel Movie { get; set; }   

    }
}
