namespace CineMax.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        public bool? Removed { get; private set; }
        public DateTime? RemovedOn { get; private set; }
        public void delete()
        {
            Removed = true;
            RemovedOn = DateTime.Now;
        }

        public void Restaure()
        {
            if (Removed == true)
                Removed = false;
        }
        protected BaseEntity()
        {
        }
    }
}
