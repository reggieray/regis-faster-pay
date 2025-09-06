namespace Regis.Pay.Common.EventStore
{
    public interface IEventStore
    {
        Task<EventStream> LoadStreamAsync(string streamId, CancellationToken cancellationToken);

        Task<bool> AppendToStreamAsync(
            string streamId,
            int expectedVersion,
            IEnumerable<IDomainEvent> events, CancellationToken cancellationToken);
    }
}
