using Microsoft.Extensions.DependencyInjection;
using Regis.Pay.Application.Handlers;

namespace Regis.Pay.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediator(
                    (Mediator.MediatorOptions options) =>
                    {
                        options.Assemblies = [typeof(CreatePaymentCommand)];
                    }
                );
        }
    }
}
