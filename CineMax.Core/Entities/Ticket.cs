using CineMax.Core.Enums;

namespace CineMax.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public DateTime CreatedOn { get; private set; }
        public TicketStatusEnum Status { get; private set; }
        public int SectionId { get; private set; }
        public int SeatId { get; set; }
        public int ClientId { get; private set; }
        public Section Section { get; private set; }
        public Seat Seat { get; private set; }
        public Client Client { get; private set; }

        public Ticket(int sectionId, int clientId, int seatId)
        {
            SectionId = sectionId;
            CreatedOn= DateTime.Now;
            Status = TicketStatusEnum.Validated;
            ClientId = clientId;
            SeatId = seatId;
        }

       public void Repay() 
        {
            if (Status == TicketStatusEnum.RefundRequest)
                Status = TicketStatusEnum.PaymentRefunded;
        }

        public void RefuseRefund()
        {
            if (Status == TicketStatusEnum.RefundRequest)
                Status = TicketStatusEnum.RefundDeclined;
        }


       public void Validate()
        {
            if (Status == TicketStatusEnum.PaymentAwaiting)
                Status = TicketStatusEnum.Validated;
        }
        public void Cancel()
        {
            if (Status == TicketStatusEnum.PaymentAwaiting)
                Status = TicketStatusEnum.Canceled;
        }
        public void AwaitingRefundRequest()
        {
            if (Status == TicketStatusEnum.Validated)
                Status = TicketStatusEnum.RefundRequest;
        }

    }
}
