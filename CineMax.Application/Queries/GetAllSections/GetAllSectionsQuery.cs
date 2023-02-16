using CineMax.Application.ViewModels;
using MediatR;

namespace CineMax.Application.Queries.GetAllSections
{
    public class GetAllSectionsQuery : IRequest<List<SectionViewModel>>
    {
    }
}
