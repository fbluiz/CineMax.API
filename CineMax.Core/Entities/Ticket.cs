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

        public Ticket(int sectionId)
        {
            SectionId = sectionId;
            CreatedOn= DateTime.Now;
            Status = TicketStatusEnum.PaymentAwaiting;
        }

       public void Repay() 
        {
            if (Status == TicketStatusEnum.Validated)
                Status = TicketStatusEnum.PaymentRefunded;
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


    }
}
