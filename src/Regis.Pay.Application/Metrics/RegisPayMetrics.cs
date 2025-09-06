using System.Diagnostics.Metrics;

namespace Regis.Pay.Application.Metrics
{
    public class RegisPayMetrics : IRegisPayMetrics
    {
        private const string unitMs = "ms";

        private static readonly Meter _meter = new("Regis.Pay.Metrics", "1.0.0");

        private static InstrumentAdvice<double> histogramBucketBoundaries = new() { HistogramBucketBoundaries = [0.1, 1, 5, 10, 50, 100, 500, 1000, 5000, 10000, 30000, 60000, 900000, 1800000, 3600000] };

        private static readonly Histogram<double> _completedDurationMetric =
        _meter.CreateHistogram(
            name: "payment.completed.cumulative.duration", 
            unit: unitMs, 
            advice: histogramBucketBoundaries);
        
        private static readonly Histogram<double> _createdDurationMetric =
        _meter.CreateHistogram(
            "payment.created.cumulative.duration",
            unit: unitMs,
            advice: histogramBucketBoundaries);
        
        private static readonly Histogram<double> _settledDurationMetric =
        _meter.CreateHistogram(
            "payment.settled.cumulative.duration",
            unit: unitMs,
            advice: histogramBucketBoundaries);
        
        private static readonly Histogram<double> _initiatedCriticalTimeMetric =
        _meter.CreateHistogram(
            "payment.initiated.consumer.critical.time",
            unit: unitMs,
            advice: histogramBucketBoundaries);

        public void CompletedCumulativeDuration(double durationMs)
        {
            _completedDurationMetric.Record(durationMs);
        }

        public void CreatedCumulativeDuration(double durationMs)
        {
            _createdDurationMetric.Record(durationMs);
        }

        public void PaymentInitiatedConsumerCriticalTime(double durationMs)
        {
            _initiatedCriticalTimeMetric.Record(durationMs);
        }

        public void SettledCumulativeDuration(double durationMs)
        {
            _settledDurationMetric.Record(durationMs);
        }
    }
}
