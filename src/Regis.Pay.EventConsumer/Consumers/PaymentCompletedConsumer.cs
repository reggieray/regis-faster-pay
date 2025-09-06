using MassTransit;
using Regis.Pay.Domain.IntegrationEvents;

namespace Regis.Pay.EventConsumer.Consumers
{
    public class PaymentCompletedConsumer(ILogger<PaymentCompletedConsumer> logger) : IConsumer<PaymentCompleted>
    {
        public Task Consume(ConsumeContext<PaymentCompleted> context)
        {
            logger.LogInformation("Consuming {event} for paymentId: {paymentId}", nameof(PaymentCompleted), context.Message.AggregateId);

            return Task.CompletedTask;
        }
    }
}