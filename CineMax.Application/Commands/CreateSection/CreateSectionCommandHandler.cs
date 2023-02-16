using CineMax.Core.Entities;
using CineMax.Core.Repositories;
using MediatR;

namespace CineMax.Application.Commands.CreateSection
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Section>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateSectionCommandHandler(ISectionRepository sectionRepository, IRoomRepository roomRepository)
        {
            _sectionRepository = sectionRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Section> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var section = new Section(request.Name, request.Description, request.StartSection, request.EndSection, request.MovieId, request.RoomId);

            await _sectionRepository.AddAsync(section);

            var room = await _roomRepository.GetByIdAsync(r => r.Id == request.RoomId);
            
            var ticketsCount = room.Seats.Count();

            for (int i = 1; i <= ticketsCount; i++)
            {
                await _sectionRepository.AddNewTicketAsync(section.Id);
            };

            return section;
        }
    }
}
