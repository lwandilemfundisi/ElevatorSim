using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Persistence;
using Microservice.Framework.Persistence.EFCore.Queries.CriteriaQueries;

namespace ElevatorSim.Domain.DomainModel.ElevatorModel.Queries
{
    public class GetElevatorQuery
        : EFCoreCriteriaDomainQuery<Elevator>, IQuery<Elevator>
    {
        #region Constructors

        public GetElevatorQuery(ElevatorId elevatorId)
        { 
            Id = elevatorId;
        }

        #endregion
    }

    public class GetElevatorQueryHandler
        : EFCoreCriteriaDomainQueryHandler<Elevator>, IQueryHandler<GetElevatorQuery, Elevator>
    {
        #region Constructors

        public GetElevatorQueryHandler(IPersistenceFactory persistenceFactory)
            : base(persistenceFactory)
        {
        }

        #endregion

        #region Virtual Methods

        public Task<Elevator> ExecuteQueryAsync(
            GetElevatorQuery query,
            CancellationToken cancellationToken)
        {
            return Find(query);
        }

        #endregion
    }
}
