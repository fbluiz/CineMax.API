using CineMax.Core.Logs;

namespace CineMax.Core.Repositories
{
    public interface IPaymentRefundLogRepository : IRepository<PaymentRefundLog>
    {
        Task<List<string>> GetLogHistoryByTicket(int ticketId, int clientid);
    }
}