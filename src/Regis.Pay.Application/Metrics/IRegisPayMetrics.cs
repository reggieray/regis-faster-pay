namespace Regis.Pay.Application.Metrics
{
    public interface IRegisPayMetrics
    {
        void CreatedCumulativeDuration(double durationMs);

        void SettledCumulativeDuration(double durationMs);

        void CompletedCumulativeDuration(double durationMs);

        void PaymentInitiatedConsumerCriticalTime(double durationMs);
    }
}
