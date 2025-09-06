using MassTransit;
using Regis.Pay.Application.Handlers;
using Regis.Pay.Domain.IntegrationEvents;

namespace Regis.Pay.EventConsumer.Consumers
{
    public class PaymentInitiatedConsumer : IConsumer<PaymentInitiated>
    {
        private readonly Mediator.IMediator _mediator;

        public PaymentInitiatedConsumer(Mediator.IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<PaymentInitiated> context)
        {
            await _mediator.Send(new CreatePaymentCommand(context.Message.AggregateId), context.CancellationToken);

            await _mediator.Send(new SettlePaymentCommand(context.Message.AggregateId), context.CancellationToken);

            await _mediator.Send(new CompletePaymentCommand(context.Message.AggregateId), context.CancellationToken);
        }
    }
}
