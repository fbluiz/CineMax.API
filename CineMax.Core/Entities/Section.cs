using CineMax.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineMax.Core.Entities
{
    public class Section : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int TicketsDisponible { get; private set; }
        public DateTime StartSection { get; private set; }
        public DateTime EndSection { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public SectionStatusEnum Status { get; private set; }
        public List<Ticket> Tickets { get; private set; }
        public int MovieId { get; private set; }
        public int RoomId { get; private set; }
        public Movie Movie { get; private set; }
        public Room Room { get; private set; }

        public Section (string name, string description, DateTime startSection, DateTime endSection, int movieId, int roomId, int ticketsDisponible)
        {
            Name = name;
            Description = description;
            StartSection = startSection;
            EndSection = endSection;
            CreatedOn = DateTime.Now;
            MovieId = movieId;
            RoomId = roomId;
            Status = SectionStatusEnum.Created;
            Tickets = new List<Ticket>();
            TicketsDisponible = ticketsDisponible;
        }

        public Section()
        {
        }

        public void Update(string? name, string? description, DateTime? startSection, DateTime? endSection, SectionStatusEnum? status, int? roomId, int? tickestDisponible)
        {
            Name = name ?? Name;
            Description = description ?? Description;
            StartSection = startSection ?? StartSection;
            EndSection = endSection ?? EndSection;
            Status = status ?? Status;
            RoomId = roomId ?? RoomId;
            TicketsDisponible = tickestDisponible ?? TicketsDisponible;
        }

        public void Progress()
        {
            if (Status == SectionStatusEnum.Created)
                Status = SectionStatusEnum.Inprogress;
        }
        public void Cancel()
        {
            if (Status == SectionStatusEnum.Created)
                Status = SectionStatusEnum.Canceled;
        }
        public void Finish()
        {
            if (Status == SectionStatusEnum.Inprogress)
            {
                Status = SectionStatusEnum.End;
                EndSection = DateTime.Now;
            }
                
        }

        public void SubtractTicketsDisponible()
        {
            TicketsDisponible = TicketsDisponible - 1;
        }

    }
}
