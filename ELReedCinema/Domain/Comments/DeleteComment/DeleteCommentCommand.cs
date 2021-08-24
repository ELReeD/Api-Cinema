using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Comments.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        [FromRoute]
        public string Id { get; set; }
    }
}
