using CineMax.Core.Logs;
using CineMax.Core.Repositories;

namespace CineMax.Infra.Persistence.Repositories
{
    public class PaymentRefundLogRepository : Repository<PaymentRefundLog>, IPaymentRefundLogRepository
    {
        public PaymentRefundLogRepository(CineMaxDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<string>> GetLogHistoryByTicket(int ticketId, int clientid)
        {
            return (await GetAsync())
                .Where(l => l.TicketId == ticketId && l.ClientId == clientid)
                .OrderBy(l => l.CreatedOn)
                .Select(l => l.LogMessage)
                .ToList();
        }
    }
}
