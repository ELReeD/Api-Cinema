using ELReedCinema.Data;
using ELReedCinema.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Comments.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly CommentsDbContext context;

        public DeleteCommentCommandHandler(CommentsDbContext context)
        {
            this.context = context;
        }
        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                context.Comment.Remove(new Comment() { Id = request.Id });
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if(!context.Comment.Any(x=>x.Id == request.Id))
                {
                    throw new Exception("Not Found!");
                }

                throw ex;
            }


            return Unit.Value;
        }
    }
}
