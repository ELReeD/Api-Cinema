using ELReedCinema.Domain.Movies.GetMoviesById;
using ELReedCinema.Domain.Movies.GetMoviesByName;
using ELReedCinema.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public class CinemaController : ControllerBase
    {

        private readonly IMediator mediator;

        public CinemaController(IMediator mediator)
        {
            this.mediator = mediator;
        }


       
        [HttpGet("{Title}/{Page}")]
        public async Task<ActionResult<MovieApiResponce>> GetMoviesByName([FromRoute] GetMoviesByTitleQuery query)
        {
            return await mediator.Send(query);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Movie>> GetMovieById([FromRoute] GetMoviesByIdQuery query)
        {
            return await mediator.Send(query);
        }
    }
}
