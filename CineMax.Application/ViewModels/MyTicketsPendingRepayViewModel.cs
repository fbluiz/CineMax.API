namespace CineMax.Application.ViewModels
{
    public class MyTicketsPendingRepayViewModel
    {
        public int ticketId { get; set; }
        public List<string> StatusHistory { get; set; }
        public DateTime BuyDateTime { get; set; }
    }
}
