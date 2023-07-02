using CineMax.Core.Entities;
using CineMax.Core.Enums;
using CineMax.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CineMax.Infra.Persistence.Repositories
{
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(CineMaxDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Section>> GetSectionsViewModelAsync(bool? disponible)
        {
            return await _dbContext.Sections
                .Where(s => disponible != true && (s.Removed == false || s.Removed == null) || s.Status == SectionStatusEnum.Created && (s.Removed == false || s.Removed == null))
                .Include(s => s.Room)
                .Include(s => s.Movie)
                .ToListAsync();
        }

        public async Task<Section> GetSectionViewModelByIdAsync(int sectionId)
        {
            return await _dbContext.Sections
                .Include(s => s.Room)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.Id == sectionId);
        }


        public async Task UpdateSectionAsync(Section section)
        {
            _dbContext.Update(section);
            await SaveChangesAsync();
        }
      }
    }
