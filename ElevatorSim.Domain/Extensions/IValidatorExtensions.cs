using Microservice.Framework.Common;
using Microservice.Framework.Domain.Rules.Notifications;
using Microservice.Framework.Domain.Rules.RuleValidator;

namespace ElevatorSim.Domain.Extensions
{
    public static class IValidatorExtensions
    {
        public static Task<Notification> Validate(
            this IValidator validator,
            object instance,
            object context,
            CancellationToken cancellationToken)
        {
            return validator.Validate(
                instance,
                context,
                SystemCulture.Default(),
                typeof(IValidatorExtensions).Assembly,
                cancellationToken
                );
        }
    }
}
