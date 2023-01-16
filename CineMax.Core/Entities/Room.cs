namespace CineMax.Core.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; private set; }
        public bool IsRoomOcuped { get; private set; }
        public List<Section> Sections { get; private set; }
        public List<Seat> Seats { get; private set; }

        public Room(string name)
        {
            Name = name;
            IsRoomOcuped = false;
            Sections= new List<Section>();
            Seats= new List<Seat>();
        }

        public void Occupy()
        {
            if (IsRoomOcuped == false)
            IsRoomOcuped = true;
        }

        public void Release()
        {
            if (IsRoomOcuped == true)
            IsRoomOcuped = false;
        }

    }
}
