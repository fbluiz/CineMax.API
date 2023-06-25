

namespace CineMax.Application.ViewModels
{
    public class GetSectionViewModel
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartSection { get; set; }
        public DateTime EndSection { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
        public string NameRoom { get; set; }
        public int TicketDisponibles { get; set; }
        public string NameMovie { get; set; }
        public List<SeatViewModel> Seats { get; set; }
        
    }
}
