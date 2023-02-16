using CineMax.Core.Enums;


namespace CineMax.Core.Entities
{
    public class Seat : BaseEntity
    {
        public SeatStatusEnum Status { get; private set; }
        public string Position { get; private set; }      
        public int RoomId { get; private set; }
        public Room Room { get; private set; }
       
        public Seat(string position, int roomId)
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
