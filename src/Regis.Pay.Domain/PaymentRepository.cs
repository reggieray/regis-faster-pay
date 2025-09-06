using Regis.Pay.Common.EventStore;

namespace Regis.Pay.Domain
{
    public class PaymentRepository : IPaymentRepository
    {
        private const string StreamIdPrefix = "pay";
        private readonly IEventStore _eventStore;

        public PaymentRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Payment> LoadAsync(string streamId, CancellationToken cancellationToken)
        {
            var stream = await _eventStore.LoadStreamAsync(streamId, cancellationToken);

            if (stream is null)
            {
                throw new Exception($"Unable to find payment for streamId: {streamId}"); //Add custom exception
            }

            return new Payment(stream!.Events);
        }

        public async Task<bool> SaveAsync(Payment payment, CancellationToken cancellationToken)
        {
            if (payment.Changes.Any())
            {
                var streamId = $"{StreamIdPrefix}:{payment.PaymentId}";

                return await _eventStore.AppendToStreamAsync(
                streamId,
                payment.Version,
                payment.Changes, cancellationToken);
            }

            return true;
        }
    }
}
