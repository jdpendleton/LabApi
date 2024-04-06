using LabTec.Infrastructure.Repositories;
using LabTec.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LabTec.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ISampleRepository, SampleRepository>();

            return services;
        }
    }
}
