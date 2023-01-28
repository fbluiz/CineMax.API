using CineMax.Core.Enums;
using MediatR;

namespace CineMax.Application.Commands.UpdateMovie
{
    public class UpdateMovieCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string TrailerURL { get; set; }
        public string Duration { get; set; }
        public MovieStatusEnum Status { get; set; }

    }
}
