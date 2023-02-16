using CineMax.Core.Entities;
using CineMax.Core.Enums;
using MediatR;

namespace CineMax.Application.Commands.CreateSection
{
    public class CreateSectionCommand : IRequest<Section>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartSection { get; set; }
        public DateTime EndSection { get; set; }
        public DateTime CreatedOn { get; set; }
        public SectionStatusEnum Status { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
    }
}
