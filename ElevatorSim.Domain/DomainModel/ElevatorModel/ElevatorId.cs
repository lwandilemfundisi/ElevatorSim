using Microservice.Framework.Common;
using Newtonsoft.Json;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class ElevatorId 
        : Identity<ElevatorId>
    {
        #region Constructors

        public ElevatorId(string value)
            : base(value)
        {
        }


        #endregion
    }
}
