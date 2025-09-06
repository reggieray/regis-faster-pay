namespace Regis.Pay.Application.Metrics
{
    public interface IRegisPayMetrics
    {
        void CreatedDuration(double durationMs);

        void SettledDuration(double durationMs);

        void CompletedDuration(double durationMs);

        void PaymentInitiatedConsumerCriticalTime(double durationMs);
    }
}
