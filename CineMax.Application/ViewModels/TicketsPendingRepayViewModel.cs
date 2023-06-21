namespace CineMax.Application.ViewModels
{
    public class TicketsPendingRepayViewModel
    {
        public int ticketId { get; set; }
        public int clientId { get; set; }
        public List<string> StatusHistory { get; set; }
        public DateTime BuyDateTime { get; set; }
    }
}
