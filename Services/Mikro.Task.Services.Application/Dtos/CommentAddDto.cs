using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Services.Application.Dtos
{
    public class CommentAddDto
    {
        public int MovieId { get; set; }

        [Range(0, 10, ErrorMessage = "Score must be in between 1 and 10")]
        public int Score { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength]
        public string? Comment { get; set; }

    }
}
