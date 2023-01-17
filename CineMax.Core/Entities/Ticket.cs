using CineMax.Core.Enums;

namespace CineMax.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public DateTime CreatedOn { get; private set; }
        public TicketStatusEnum Status { get; private set; }
        public int SectionId { get; private set; }
        public int SeatId { get; private set; }
        public int UserId { get; private set; }
        public Section Section { get; private set; }
        public Seat Seat { get; private set; }
        public User User { get; private set; }


#pragma warning disable CS8618 // O propriedade não anulável 'Seat' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning disable CS8618 // O propriedade não anulável 'User' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning disable CS8618 // O propriedade não anulável 'Section' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public Ticket(int sectionId, int seatId)
#pragma warning restore CS8618 // O propriedade não anulável 'Section' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning restore CS8618 // O propriedade não anulável 'User' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning restore CS8618 // O propriedade não anulável 'Seat' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
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
