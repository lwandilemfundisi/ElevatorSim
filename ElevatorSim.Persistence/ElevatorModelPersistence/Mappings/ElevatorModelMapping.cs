using ElevatorSim.Domain.DomainModel.ElevatorModel;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects.XmlValueObjects;
using ElevatorSim.Persistence.ValueObjectConverters;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSim.Persistence.ElevatorModelPersistence.Mappings
{
    public static class ElevatorModelMapping
    {
        public static ModelBuilder ElevatorModelMap(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Elevator>()
                .Property(o => o.Id)
                .HasConversion(new SingleValueObjectIdentityValueConverter<ElevatorId>());

            modelBuilder
                .Entity<Elevator>()
                .Property(o => o.ElevatorStatus)
                .HasConversion(new ValueObjectValueConverter<ElevatorStatus, ElevatorStatuses>());

            return modelBuilder;
        }
    }
}
