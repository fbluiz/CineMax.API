using CineMax.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMax.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public Section Section { get; private set; }
        public Seat Seat { get; private set; }
        public TicketStatusEnum Status { get; private set; }
        public int SectionId { get; private set; }
        public int SeatId { get; private set; }

        public Ticket(int sectionId, int seatId)
        {
            SectionId = sectionId;
            SeatId = seatId;
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
