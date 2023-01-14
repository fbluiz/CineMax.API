using CineMax.Core.Enums;

namespace CineMax.Core.Entities
{
    public class Movie : BaseEntity
    {   
        public string Title { get; set; }
        public string Description { get; private set; }
        public string ImageURL { get; private set; }
        public string TrailerURL { get; private set; }
        public DateTime Duration { get; private set; }
        public MovieStatusEnum Status { get; private set; }
        public List<Section> Sections { get; private set; }

        public Movie(string title, string description, string imageURL, string trailerURL, DateTime duration, MovieStatusEnum status)
        {
            Title = title;
            Description = description;
            ImageURL = imageURL;
            TrailerURL = trailerURL;
            Duration = duration;
            Status = status;
            Sections = new List<Section>();
        }

        public void Available()
        {
            if (Status == MovieStatusEnum.InPoster || Status == MovieStatusEnum.Canceled)
                Status = MovieStatusEnum.Disponible;
        }
        public void LeaveOnPoster()
        {
            if (Status == MovieStatusEnum.InSoon)
                Status = MovieStatusEnum.InPoster;
        }
        public void Cancel()
        {
            if (Status != MovieStatusEnum.Indisponible)
                Status = MovieStatusEnum.Canceled;
        }

        public void Extend()
        {
            if (Status == MovieStatusEnum.InPoster)
                Status = MovieStatusEnum.Extended;
        }

        public void Unavailable()
        {
            if (Status == MovieStatusEnum.Disponible)
                Status = MovieStatusEnum.Indisponible;
        }
    }
}
