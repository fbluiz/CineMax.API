using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetAllMovies
{
    public class GetAllMoviesQuery : IRequest<List<MovieViewModel>>
    {
    }
}
