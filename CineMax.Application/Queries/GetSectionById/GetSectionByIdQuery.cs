using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetSectionById
{
    public class GetSectionByIdQuery : IRequest<GetSectionViewModel>
    {
        public int Id { get; set; }
    }
}
