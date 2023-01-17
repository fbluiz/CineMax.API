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
        public List<Ticket> Tickets { get; private set; }
        public int MovieId { get; private set; }
        public int RoomId { get; private set; }
        public Movie Movie { get; private set; }
        public Room Room { get; private set; }

#pragma warning disable CS8618 // O propriedade não anulável 'Room' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning disable CS8618 // O propriedade não anulável 'Movie' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public Section(string name, string description, DateTime startSection, DateTime endSection, int movieId, int roomId)
#pragma warning restore CS8618 // O propriedade não anulável 'Movie' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning restore CS8618 // O propriedade não anulável 'Room' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
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
