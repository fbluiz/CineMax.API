using System.ComponentModel.DataAnnotations;

namespace CineMax.Core.Logs
{
    public class PaymentRefundLog 
    {
        [Key]
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ClientId { get; set; }
        public string LogMessage { get; set; }
        public DateTime CreatedOn { get; set; }

        public PaymentRefundLog(int ticketId, int clientId, string logMessage)
        {
            TicketId = ticketId;
            ClientId = clientId;
            LogMessage = logMessage;
            CreatedOn = DateTime.Now;
        }
    }
}
