using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Comments.CreateComment
{
    public class CreateCommentCommand : IRequest
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string FilmId { get; set; }
        public string UserId{ get; set; }
    }
}
