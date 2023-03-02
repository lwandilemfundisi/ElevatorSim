using Microservice.Framework.Common;
using Newtonsoft.Json;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class ManagedElevatorId : Identity<ManagedElevatorId>
    {
        #region Constructors

        public ManagedElevatorId(string value)
            : base(value)
        {

        }

        #endregion
    }
}
