using MassTransit;
using Regis.Pay.Domain.IntegrationEvents;

namespace Regis.Pay.EventConsumer.Consumers
{
    public class PaymentSettledConsumer(ILogger<PaymentSettledConsumer> logger) : IConsumer<PaymentSettled>
    {
        public Task Consume(ConsumeContext<PaymentSettled> context)
        {
            logger.LogInformation("Consuming {event} for paymentId: {paymentId}", nameof(PaymentSettled), context.Message.AggregateId);

            return Task.CompletedTask;
        }
    }
}