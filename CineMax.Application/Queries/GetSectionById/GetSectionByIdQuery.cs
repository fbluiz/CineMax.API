using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetSectionById
{
    public class GetSectionByIdQuery : IRequest<SectionViewModel>
    {
        public int Id { get; set; }
    }
}
