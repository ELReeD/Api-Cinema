using ELReedCinema.Data;
using ELReedCinema.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Comments.GetCommentByCinemaId
{
    public class GetCommentByCinemaIdQueryHandler : IRequestHandler<GetCommentByCinemaIdQuery, IEnumerable<Comment>>
    {
        private readonly CommentsDbContext context;

        public GetCommentByCinemaIdQueryHandler(CommentsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Comment>> Handle(GetCommentByCinemaIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await context.FindAsync<IEnumerable<Comment>>(request.CinemaId);

            return comments;
        }
    }
}
