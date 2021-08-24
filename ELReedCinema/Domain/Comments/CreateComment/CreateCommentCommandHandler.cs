
using ELReedCinema.Data;
using ELReedCinema.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Comments.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly CommentsDbContext dbContext;

        public CreateCommentCommandHandler(CommentsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {

            Comment commentTest = new Comment(request.UserId, request.FilmId, request.Title, request.Text);

     
            try
            {
                await dbContext.Comment.AddAsync(commentTest);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
                     

            return Unit.Value;
        }
    }
}
