using ELReedCinema.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Movies.GetMoviesByName
{
    public class GetMoviesByTitleQuery : IRequest<MovieApiResponce>
    {
        [FromRoute]
        public string Title { get; set; }

        [FromRoute]
        public string Page { get; set; } = "1";
    }
}
