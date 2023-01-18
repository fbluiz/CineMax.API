using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetMovieById
{
    public class GetMovieByIdQuery : IRequest<MovieViewModel>
    {
        public int Id { get; set; }

        public GetMovieByIdQuery(int id)
        {
            Id = id;
        }
    }
}
