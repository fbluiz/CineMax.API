using CineMax.Core.Enums;
using MediatR;

namespace CineMax.Application.Commands.UpdateSection
{
    public class UpdateSectionCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartSection { get; set; }
        public DateTime? EndSection { get; set;}
        public SectionStatusEnum? Status { get; set; }
        public int? RoomId { get; set; }
        public int? MaximumTickets { get; set; }
    }
}
