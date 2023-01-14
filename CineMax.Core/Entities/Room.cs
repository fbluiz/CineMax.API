namespace CineMax.Core.Entities
{
    public class Room : BaseEntity
    {
        public string Name { get; private set; }
        public bool IsRoomOcuped { get; private set; }
        public List<Section> ListSections { get; private set; }
        public List<Seat> ListSeats { get; private set; }

        public Room(string name)
        {
            Name = name;
            IsRoomOcuped = false;
            ListSections= new List<Section>();
            ListSeats= new List<Seat>();
        }

        public void Occupy()
        {
            IsRoomOcuped = true;
        }

        public void Release()
        {
            IsRoomOcuped = false;
        }

    }
}
