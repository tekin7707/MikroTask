using Mikro.Task.Services.Application.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Task.Test.TestDatas
{
    public class AddCommentTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new CommentAddDto {
                     Comment = "Test comment. Good.",
                      MovieId = 10, 
                       Score = 9
                }, "Test comment. Good."};

            yield return new object[] {
                new CommentAddDto {
                     Comment = "Test comment. Good.",
                      MovieId = 0,
                       Score = 9
                }, "Movie not found"};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
