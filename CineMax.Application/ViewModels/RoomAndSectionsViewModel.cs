using CineMax.Core.Entities;

namespace CineMax.Application.ViewModels
{
    public class RoomAndSectionsViewModel
    {
        public string Name { get; set; }
        public bool IsRoomOcuped { get;  set; }
        public int MaximumCapacity { get;  set; }
        public List<DetailsSectionViewModel> DetailsSections { get; set; }
    }
}
