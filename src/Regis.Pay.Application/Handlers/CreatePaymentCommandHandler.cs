using Mediator;
using Microsoft.Extensions.Logging;
using Regis.Pay.Application.Metrics;
using Regis.Pay.Common.ApiClients.Payments;
using Regis.Pay.Domain;

namespace Regis.Pay.Application.Handlers
{
    public class CreatePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            ILogger<CreatePaymentCommandHandler> logger,
            IPaymentsApi paymentsApi,
            IRegisPayMetrics metrics) : ICommandHandler<CreatePaymentCommand>
    {
        public async ValueTask<Unit> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling {command} for paymentId: {paymentId}", nameof(CreatePaymentCommand), command.aggregateId);

            var payment = await paymentRepository.LoadAsync(command.aggregateId, cancellationToken);

            if (payment.IsCreated) return Unit.Value;

            var reponse = await paymentsApi.CreatePaymentAsync(new CreatePaymentRequest(payment.Amount, payment.Currency), cancellationToken);

            await reponse.EnsureSuccessfulAsync();

            payment.Created(reponse.Content!.PaymentId);

            await paymentRepository.SaveAsync(payment, cancellationToken);

            var diff = DateTime.UtcNow - payment.PaymentInitiatedTimestamp;

            metrics.CreatedCumulativeDuration(diff.TotalMilliseconds);

            return Unit.Value;
        }
    }
}
