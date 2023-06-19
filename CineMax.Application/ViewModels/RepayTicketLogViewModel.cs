namespace CineMax.Application.ViewModels
{
    public class RepayTicketLogViewModel
    {
        public int TicketId { get; set; }
        public int ClientId { get; set; }
        public Guid AdminId { get; set; }
        public string LogMessage { get; set; }
    }
}
