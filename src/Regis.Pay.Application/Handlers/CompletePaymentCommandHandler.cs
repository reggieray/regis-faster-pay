using Mediator;
using Microsoft.Extensions.Logging;
using Regis.Pay.Common.ApiClients.Notifications;
using Regis.Pay.Domain;
using System.Text.Json;

namespace Regis.Pay.Application.Handlers
{
    public class CompletePaymentCommandHandler(
            IPaymentRepository paymentRepository,
            ILogger<CompletePaymentCommandHandler> logger,
            INotificationsApi notificationsApi) : ICommandHandler<CompletePaymentCommand>
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

            return Unit.Value;
        }
    }
}
