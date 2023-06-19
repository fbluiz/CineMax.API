namespace CineMax.Core.Entities
{
    public class PaymentRefundLog : BaseEntity
    {
      
        public int TicketId { get; set; }
        public int ClientId { get; set; }
        public Guid? AdminId { get; set; } = null;
        public string LogMessage { get; set; }

        public PaymentRefundLog(int ticketId, int clientId, string logMessage, Guid? adminId = null)
        {
            TicketId = ticketId;
            ClientId = clientId;
            AdminId = adminId;
            LogMessage = logMessage;
        }
    }

}
