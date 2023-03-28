using CineMax.Application.ViewModels;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.UpdateSection
{
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, int>
    {
        private readonly ISectionRepository _sectionRepository;

        public UpdateSectionCommandHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<int> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(s => s.Id == request.Id);
            
            section.Update(request.Name, request.Description, request.StartSection, request.EndSection, request.Status, request.RoomId, request.MaximumTickets);
            
            await _sectionRepository.UpdateSectionAsync(section);

            return section.Id;

        }
    }
}
