using ELReedCinema.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Comments.GetCommentByCinemaId
{
    public class GetCommentByCinemaIdQuery : IRequest<IEnumerable<Comment>>
    {
        [FromRoute]
        public string CinemaId { get; set; }
    }
}
