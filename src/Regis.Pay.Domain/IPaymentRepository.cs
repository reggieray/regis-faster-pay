namespace Regis.Pay.Domain
{
    public interface IPaymentRepository
    {
        Task<Payment> LoadAsync(string streamId, CancellationToken cancellationToken);

        Task<bool> SaveAsync(Payment payment, CancellationToken cancellationToken);
    }
}
