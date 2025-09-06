using Regis.Pay.Common.EventStore;
using Regis.Pay.Domain.Events;

namespace Regis.Pay.Domain
{
    public class Payment : AggregateBase
    {
        public bool IsCreated { get; private set; }
        public bool IsSettled { get; private set; }
        public bool IsCompleted { get; private set; }

        public Guid PaymentId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public DateTime? PaymentCreatedTimestamp { get; private set; }
        public Guid? ThridPartyReference { get; private set; }
        public DateTime? PaymentCompletedTimestamp { get; private set; }

        public Payment(IEnumerable<IDomainEvent> events) : base(events)
        {
        }

        public Payment(Guid paymentId, decimal amount, string currency) : base()
        {
            Apply(new PaymentInitiated()
            {
                PaymentId = paymentId,
                Amount = amount,
                Currency = currency
            });
        }

        public void Created(Guid paymentReference)
        {
            Apply(new PaymentCreated() { PaymentReference = paymentReference });
        }

        public void Settled()
        {
            Apply(new PaymentSettled());
        }

        public void Complete()
        {
            Apply(new PaymentCompleted());
        }

        public void When(PaymentInitiated @event)
        {
            PaymentId = @event.PaymentId;
            Amount = @event.Amount;
            Currency = @event.Currency;
        }

        public void When(PaymentCreated @event)
        {
            IsCreated = true;
            PaymentCreatedTimestamp = @event.Timestamp;
            ThridPartyReference = @event.PaymentReference;
        }

        public void When(PaymentSettled @event) 
        {
            IsSettled = true;
        }

        public void When(PaymentCompleted @event)
        {
            IsCompleted = true;
            PaymentCompletedTimestamp = @event.Timestamp;
        }
    }
}
