using Refit;
using System.Threading;

namespace Regis.Pay.Common.ApiClients.Payments
{
    public interface IPaymentsApi
    {
        [Post("/psp/api/payments/create")]
        Task<ApiResponse<CreatePaymentResponse>> CreatePaymentAsync([Body] CreatePaymentRequest request, CancellationToken cancellationToken);

        [Post("/psp/api/payments/{paymentId}/settle")]
        Task<HttpResponseMessage> SettlePaymentAsync(Guid paymentId, CancellationToken cancellationToken);
    }
}
