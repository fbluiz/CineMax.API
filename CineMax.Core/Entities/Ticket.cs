﻿using CineMax.Core.Enums;

namespace CineMax.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public int SectionId { get; private set; }
        public int SeatId { get; private set; }
        public int UserId { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public TicketStatusEnum Status { get; private set; }
        public Section Section { get; private set; }
        public Seat Seat { get; private set; }
        public User User { get; private set; }


        public Ticket(int sectionId, int seatId)
        {
            SectionId = sectionId;
            CreatedOn= DateTime.Now;
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
