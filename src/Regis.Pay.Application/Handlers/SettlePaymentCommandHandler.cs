using Mediator;
using Microsoft.Extensions.Logging;
using Regis.Pay.Application.Metrics;
using Regis.Pay.Common.ApiClients.Payments;
using Regis.Pay.Domain;

namespace Regis.Pay.Application.Handlers
{
    public class SettlePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            ILogger<SettlePaymentCommandHandler> logger,
            IPaymentsApi paymentsApi,
            IRegisPayMetrics metrics) : ICommandHandler<SettlePaymentCommand>
    {
        public async ValueTask<Unit> Handle(SettlePaymentCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling {command} for paymentId: {paymentId}", nameof(SettlePaymentCommand), command.aggregateId);

            var payment = await paymentRepository.LoadAsync(command.aggregateId, cancellationToken);

            if(payment.IsSettled) return Unit.Value;

            var resonse = await paymentsApi.SettlePaymentAsync(payment.ThridPartyReference!.Value, cancellationToken);

            resonse.EnsureSuccessStatusCode();

            payment.Settled();

            await paymentRepository.SaveAsync(payment, cancellationToken);

            var diff = DateTime.UtcNow - payment.PaymentInitiatedTimestamp;

            metrics.SettledCumulativeDuration(diff.TotalMilliseconds);

            return Unit.Value;
        }
    }
}
