using CineMax.Core.DTOs.PaymentDTOs.Request;
using CineMax.Core.DTOs.PaymentDTOs.Response;
using CineMax.Core.Services.Payment;

namespace CineMax.Infra.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentResponse Process(PaymentRequest request)
        {
            PaymentResponse response = new PaymentResponse { };
            if (request is null)
            {
                response.Success = false;
            }
            response.Success = true;

            return response;
        }
    }
}
