using CineMax.Core.Enums;

namespace CineMax.Core.Entities
{
    public class Section : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartSection { get; private set; }
        public DateTime EndSection { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public SectionStatusEnum Status { get; private set; }
        public int MovieId { get; private set; }
        public int RoomId { get; private set; }
        public Movie Movie { get; private set; }
        public Room Room { get; private set; }
        public List<Ticket> ListTickets { get; private set;}

        public Section(string name, string description, DateTime startSection, DateTime endSection, int idMovie, int idRoom)
        {
            Name = name;
            Description = description;
            StartSection = startSection;
            EndSection = endSection;
            CreatedOn = DateTime.Now;
            IdMovie = idMovie;
            IdRoom = idRoom;
            Status = SectionStatusEnum.Created;
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

    }
}
