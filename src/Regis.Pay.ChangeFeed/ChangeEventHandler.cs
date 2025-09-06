using MassTransit;
using Regis.Pay.Common.EventStore;
using Regis.Pay.Domain;

namespace Regis.Pay.ChangeFeed
{
    public interface IChangeEventHandler
    {
        Task HandleAsync(IReadOnlyCollection<EventWrapper> events, CancellationToken cancellationToken);
    }

    public class ChangeEventHandler(
        IBus bus,
        ILogger<ChangeEventHandler> logger) : IChangeEventHandler
    {
        private readonly IBus _bus = bus;
        private readonly ILogger<ChangeEventHandler> _logger = logger;

        public async Task HandleAsync(IReadOnlyCollection<EventWrapper> events, CancellationToken cancellationToken)
        {
            var integrationEvents = events
                    .Select(e =>
                    {
                        var ev = IntegrationEventResolver.Resolve(e);
                        if (ev is null)
                        {
                            _logger.LogWarning("No integration event found for event {eventId}", e.Id);
                        }
                        return ev;
                    })
                    .Where(e => e is not null)
                    .ToList();

            if (integrationEvents.Any())
            {
                const int batchSize = 500;
                foreach (var batch in integrationEvents.Chunk(batchSize))
                {
                    await _bus.PublishBatch(batch!, cancellationToken);
                }
            }
            else 
            {
                _logger.LogInformation("No integration events to publish");
            }

            _logger.LogInformation("Finished handling changes.");
        }
    }
}
