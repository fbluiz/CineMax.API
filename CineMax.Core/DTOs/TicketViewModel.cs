using Newtonsoft.Json;

namespace CineMax.Application.ViewModels
{
    public class TicketViewModel
    {
        public string NameMovie { get; set; }
        public int TicketId { get; set; }
        public string SeatPosition { get; set; }
        public string Status { get; set;}
        public string NameSection { get; set; }
        public int SectionId { get; set; }
        public string NameRoom { get; set; }
        [JsonIgnore]
        public DateTime StartSectionBase { get; set; }
        [JsonIgnore]
        public DateTime EndSectionBase { get; set; }
        public string StartSection
        {
            get => StartSectionBase.ToString("dd/MM/yyyy");
            set { }
        }
        public string EndSection
        {
            get => EndSectionBase.ToString("dd/MM/yyyy");
            set { }
        }

    }
}
