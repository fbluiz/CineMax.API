using System.ComponentModel.DataAnnotations;

namespace CineMax.Core.Entities
{
    public class SectionSeat
    {
        [Key]
        public int Id { get; set; }
        public int SeatId { get; private set; }
        public int SectionId { get; private set; }
        public bool IsDisponible { get; private set; }

        public SectionSeat(int seatId, int sectionId, bool isDisponible)
        {
            SeatId = seatId;
            SectionId = sectionId;
            IsDisponible = isDisponible;
        }

        public void ChangeAvailability (bool isDisponible)
        {
            IsDisponible = isDisponible;
        }

    }
}
