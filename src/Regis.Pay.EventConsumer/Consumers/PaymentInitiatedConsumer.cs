using MassTransit;
using Regis.Pay.Application.Handlers;
using Regis.Pay.Application.Metrics;
using Regis.Pay.Domain.IntegrationEvents;
using System.Diagnostics;

namespace Regis.Pay.EventConsumer.Consumers
{
    public class PaymentInitiatedConsumer : IConsumer<PaymentInitiated>
    {
        private readonly Mediator.IMediator _mediator;
        private readonly IRegisPayMetrics _metrics;

        public PaymentInitiatedConsumer(
            Mediator.IMediator mediator,
            IRegisPayMetrics metrics)
        {
            _mediator = mediator;
            _metrics = metrics;
        }

        public async Task Consume(ConsumeContext<PaymentInitiated> context)
        {
            var sw = Stopwatch.StartNew();

            await _mediator.Send(new CreatePaymentCommand(context.Message.AggregateId), context.CancellationToken);

            await _mediator.Send(new SettlePaymentCommand(context.Message.AggregateId), context.CancellationToken);

            await _mediator.Send(new CompletePaymentCommand(context.Message.AggregateId), context.CancellationToken);

            sw.Stop();

            _metrics.PaymentInitiatedConsumerCriticalTime(sw.Elapsed.TotalMilliseconds);
        }
    }
}
