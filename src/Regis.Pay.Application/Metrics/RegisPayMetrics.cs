using System.Diagnostics.Metrics;

namespace Regis.Pay.Application.Metrics
{
    public class RegisPayMetrics : IRegisPayMetrics
    {
        private static readonly Meter _meter = new("Regis.Pay.Metrics", "1.0.0");
        private static readonly Histogram<double> _completedDurationMetric =
        _meter.CreateHistogram<double>(name: "payment.completed.duration", unit: "ms", description: "Duration payment completed", advice: new InstrumentAdvice<double> { HistogramBucketBoundaries = [0.1, 1, 5, 10, 50, 100, 500, 1000, 5000, 10000, 30000, 60000, 900000, 1800000, 3600000] });
        private static readonly Histogram<double> _createdDurationMetric =
        _meter.CreateHistogram<double>("payment.created.duration", "ms", "Duration payment created", advice: new InstrumentAdvice<double> { HistogramBucketBoundaries = [0.1, 1, 5, 10, 50, 100, 500, 1000, 5000, 10000, 30000, 60000, 900000, 1800000, 3600000] });
        private static readonly Histogram<double> _settledDurationMetric =
        _meter.CreateHistogram<double>("payment.settled.duration", "ms", "Duration payment settled", advice: new InstrumentAdvice<double> { HistogramBucketBoundaries = [0.1, 1, 5, 10, 50, 100, 500, 1000, 5000, 10000, 30000, 60000, 900000, 1800000, 3600000] });
        private static readonly Histogram<double> _initiatedCriticalTimeMetric =
        _meter.CreateHistogram<double>("payment.initiated.consumer.critical.time", "ms", "Critical time PaymentInitiated was consumed", advice: new InstrumentAdvice<double> { HistogramBucketBoundaries = [0.1, 1, 5, 10, 50, 100, 500, 1000, 5000, 10000, 30000, 60000, 900000, 1800000, 3600000] });

        public void CompletedDuration(double durationMs)
        {
            _completedDurationMetric.Record(durationMs);
        }

        public void CreatedDuration(double durationMs)
        {
            _createdDurationMetric.Record(durationMs);
        }

        public void PaymentInitiatedConsumerCriticalTime(double durationMs)
        {
            _initiatedCriticalTimeMetric.Record(durationMs);
        }

        public void SettledDuration(double durationMs)
        {
            _settledDurationMetric.Record(durationMs);
        }
    }
}
