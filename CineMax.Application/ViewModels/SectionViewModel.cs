namespace CineMax.Application.ViewModels
{
    public class SectionViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartSection { get; set; }
        public DateTime EndSection { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
        public List<TicketViewModel> Tickets { get; set; }
        public string NameRoom { get; set; }
    }
}
