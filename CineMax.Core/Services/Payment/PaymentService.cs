using CineMax.Core.DTOs.PaymentDTOs.Request;
using CineMax.Core.DTOs.PaymentDTOs.Response;
using CineMax.Core.Interfaces;

namespace CineMax.Core.Services
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

        public PaymentResponse RequestRefund(RefundRequest request)
        {
            PaymentResponse response = new PaymentResponse { };

            if (request is null)
               response.Success = false;

            response.Success = true;

            return response;
        }
        public PaymentResponse ApproveRefund(RefundRequest request)
        {
            PaymentResponse response = new PaymentResponse { };
            
            if (request is null)
             response.Success = false;

            response.Success = true;

            return response;
        }
    }
}
