using ELReedCinema.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ELReedCinema.Domain.Movies.GetMoviesById
{
    public class GetMoviesByIdQueryHandler : IRequestHandler<GetMoviesByIdQuery, Movie>
    {
        private HttpClient httpClient;
        public GetMoviesByIdQueryHandler(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
        }

        public async Task<Movie> Handle(GetMoviesByIdQuery request, CancellationToken cancellationToken)
        {
            var responce = await httpClient.GetAsync($"http://omdbapi.com/?apikey=1a6d3a48&i={request.Id}");
            var json = await responce.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Movie>(json);

            return result;
        }
    }
}
