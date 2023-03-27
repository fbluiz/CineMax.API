using CineMax.Core.Entities;

namespace CineMax.Core.Repositories
{
    public interface ISectionRepository : IRepository<Section>
    {
        Task AddNewTicketAsync(int idSection);
        Task<List<Section>> GetSectionViewModelAsync(bool? disponible = false);
    }
}
