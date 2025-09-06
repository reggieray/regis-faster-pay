using MassTransit;

namespace Regis.Pay.EventConsumer.Consumers
{
    public class PaymentInitiatedConsumerDefinition : ConsumerDefinition<PaymentInitiatedConsumer>
    {
        public PaymentInitiatedConsumerDefinition()
        {
            ConcurrentMessageLimit = 1000;
        }

        protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<PaymentInitiatedConsumer> consumerConfigurator)
        {
            if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rmq)
            {
                rmq.PrefetchCount = 1000;
            }
        }
    }
}
