using Microservice.Framework.Domain.Queries;
using Microservice.Framework.Persistence;
using Microservice.Framework.Persistence.EFCore.Queries.CriteriaQueries;

namespace ElevatorSim.Domain.DomainModel.ElevatorControlModel.Queries
{
    public class GetElevatorControlQuery
        : EFCoreCriteriaDomainQuery<ElevatorControl>, IQuery<ElevatorControl>
    {
        #region Constructors

        public GetElevatorControlQuery(ElevatorControlId id)
        {
            Id = id;
        }

        #endregion

        #region Virtual Members

        protected override bool FailOnNoCriteriaSpecified => true;

        #endregion
    }

    public class GetElevatorControlQueryHandler
        : EFCoreCriteriaDomainQueryHandler<ElevatorControl>, IQueryHandler<GetElevatorControlQuery, ElevatorControl>
    {

        #region Constructors

        public GetElevatorControlQueryHandler(IPersistenceFactory persistenceFactory)
            : base(persistenceFactory)
        {

        }

        #endregion

        #region Virtual Methods

        public Task<ElevatorControl> ExecuteQueryAsync(
            GetElevatorControlQuery query, 
            CancellationToken cancellationToken)
        {
            return Find(query);
        }

        #endregion
    }
}
