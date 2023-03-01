using Microservice.Framework.Domain;
using Microservice.Framework.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ElevatorSim.Domain.Extensions
{
    public static class ElevatorSimDomainExtensions
    {
        public static Assembly Assembly { get; } = 
            typeof(ElevatorSimDomainExtensions).Assembly;

        public static IDomainContainer ConfigureElevatorSimDomain(
            this IServiceCollection services,
            IConfiguration configuration = null)
        {
            return DomainContainer.New(services)
                .AddDefaults(Assembly);
        }
    }
}
