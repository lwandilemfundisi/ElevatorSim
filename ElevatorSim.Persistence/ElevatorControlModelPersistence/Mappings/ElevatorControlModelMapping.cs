using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities;
using ElevatorSim.Persistence.ValueObjectConverters;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSim.Persistence.ElevatorControlModelPersistence.Mappings
{
    public static class ElevatorControlModelMapping
    {
        public static ModelBuilder ElevatorControlModelMap(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ElevatorControl>()
                .Property(o => o.Id)
                .HasConversion(new SingleValueObjectIdentityValueConverter<ElevatorControlId>());

            modelBuilder
                .Entity<ManagedElevator>()
                .Property(o => o.Id)
                .HasConversion(new SingleValueObjectIdentityValueConverter<ManagedElevatorId>());

            modelBuilder
                .Entity<ManagedElevator>()
                .HasOne<ElevatorControl>()
                .WithMany(c => c.Elevators);

            return modelBuilder;
        }
    }
}
