using CineMax.Core.Enums;


namespace CineMax.Core.Entities
{
    public class Seat : BaseEntity
    {
        public SeatStatusEnum Status { get; private set; }
        public string Position { get; private set; }      
        public int RoomId { get; private set; }
        public Room Room { get; private set; }
       
#pragma warning disable CS8618 // O propriedade não anulável 'Room' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public Seat(string position, int roomId)
#pragma warning restore CS8618 // O propriedade não anulável 'Room' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        {
            Status = SeatStatusEnum.Free;
            Position = position;
            RoomId = roomId;
        }

       public void Occupy()
        {
            if (Status == SeatStatusEnum.Free)
            Status = SeatStatusEnum.Reserved;
        }

        public void Release()
        {
            if (Status == SeatStatusEnum.Reserved)
                Status = SeatStatusEnum.Free;
        }
    }
}
