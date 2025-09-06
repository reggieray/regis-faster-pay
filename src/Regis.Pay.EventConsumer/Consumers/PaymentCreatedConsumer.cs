using MassTransit;
using Regis.Pay.Domain.IntegrationEvents;

namespace Regis.Pay.EventConsumer.Consumers
{
    public class PaymentCreatedConsumer(ILogger<PaymentCreatedConsumer> logger) : IConsumer<PaymentCreated>
    {
        public Task Consume(ConsumeContext<PaymentCreated> context)
        {
            logger.LogInformation("Consuming {event} for paymentId: {paymentId}", nameof(PaymentCreated), context.Message.AggregateId);

            return Task.CompletedTask; 
        }
    }
}