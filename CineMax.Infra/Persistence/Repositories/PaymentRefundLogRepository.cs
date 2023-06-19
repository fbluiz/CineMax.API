using CineMax.Core.Entities;
using CineMax.Core.Repositories;

namespace CineMax.Infra.Persistence.Repositories
{
    public class PaymentRefundLogRepository : Repository<PaymentRefundLog>, IPaymentRefundLogRepository
    {
        public PaymentRefundLogRepository(ICineMaxDbContext dbContext) : base(dbContext)
        {
        }
    }
}
