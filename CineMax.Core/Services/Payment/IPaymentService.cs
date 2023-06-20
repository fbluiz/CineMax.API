﻿using CineMax.Core.DTOs.PaymentDTOs.Request;
using CineMax.Core.DTOs.PaymentDTOs.Response;

namespace CineMax.Core.Services.Payment
{
    public interface IPaymentService
    {
        PaymentResponse Process(PaymentRequest request);
        PaymentResponse RequestRefund(RefundRequest request);
    }
}
