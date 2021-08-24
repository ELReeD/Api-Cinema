using ELReedCinema.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Movies.GetMoviesById
{
    public class GetMoviesByIdQuery : IRequest<Movie>
    {
        [FromRoute]
        public string Id { get; set; }
    }
}
