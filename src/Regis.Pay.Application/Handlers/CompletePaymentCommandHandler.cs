using Mediator;
using Microsoft.Extensions.Logging;
using Regis.Pay.Application.Metrics;
using Regis.Pay.Common.ApiClients.Notifications;
using Regis.Pay.Domain;
using System.Text.Json;

namespace Regis.Pay.Application.Handlers
{
    public class CompletePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            ILogger<CompletePaymentCommandHandler> logger,
            INotificationsApi notificationsApi,
            IRegisPayMetrics metrics) : ICommandHandler<CompletePaymentCommand>
    {
        public async ValueTask<Unit> Handle(CompletePaymentCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling {command} for paymentId: {paymentId}", nameof(CompletePaymentCommand), command.aggregateId);

            var payment = await paymentRepository.LoadAsync(command.aggregateId, cancellationToken);

            if(payment.IsCompleted) return Unit.Value;

            var response = await notificationsApi.SendNotificationAsync(new NotificationRequest(payment.PaymentId, JsonSerializer.Serialize(payment)), cancellationToken);

            response.EnsureSuccessStatusCode();

            payment.Complete();

            await paymentRepository.SaveAsync(payment, cancellationToken);

            var diff = DateTime.UtcNow - payment.PaymentInitiatedTimestamp;

            metrics.CompletedCumulativeDuration(diff.TotalMilliseconds);

            logger.LogInformation("Payment {PaymentId} processed in {Duration}ms", command.aggregateId, diff.TotalMilliseconds);

            return Unit.Value;
        }
    }
}
