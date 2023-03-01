using Microservice.Framework.Common;
using Newtonsoft.Json;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class ElevatorControlId 
        : Identity<ElevatorControlId>
    {
        #region Constructors

        public ElevatorControlId(string value)
            : base(value)
        {

        }

        #endregion
    }
}
