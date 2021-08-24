using ELReedCinema.Areas.Users.Models;
using ELReedCinema.Data;
using ELReedCinema.Domain.Comments.CreateComment;
using ELReedCinema.Domain.Comments.DeleteComment;
using ELReedCinema.Domain.Comments.GetCommentByCinemaId;
using ELReedCinema.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly CommentsDbContext dbContext;
        private readonly UserManager<AppUser> userManager;

        public CommentsController(IMediator mediator,CommentsDbContext dbContext,UserManager<AppUser> userManager)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> AddComments([FromForm] CreateCommentCommand command)
        {
            var user = userManager.FindByNameAsync(User.Identity.Name);
            command.UserId = user.Result.Id;

            var comment = await mediator.Send(command);
            return Ok();
        }

        [HttpGet("{CinemaId}")]
        public IEnumerable<Comment> GetCommentsByFilmId([FromRoute] GetCommentByCinemaIdQuery request)
        {
            var test = dbContext.Comment.Where(x => x.FilmId == request.CinemaId).ToArray();
            return test;
        }

        [Authorize]
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> DeleteCommentAsync([FromRoute] DeleteCommentCommand command )
        {
            await mediator.Send(command);            
            return Ok();
        }


    }
}
