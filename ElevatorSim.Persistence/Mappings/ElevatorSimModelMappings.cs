using ElevatorSim.Domain.DomainModel.ElevatorControlModel.Entities;
using ElevatorSim.Domain.DomainModel.ElevatorControlModel;
using ElevatorSim.Persistence.ValueObjectConverters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorSim.Domain.DomainModel.ElevatorModel.ValueObjects.XmlValueObjects;
using ElevatorSim.Domain.DomainModel.ElevatorModel;

namespace ElevatorSim.Persistence.Mappings
{
    public static class ElevatorSimModelMappings
    {
        public static ModelBuilder ElevatorSimModelMap(this ModelBuilder modelBuilder)
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
                .Entity<Elevator>()
                .Property(o => o.Id)
                .HasConversion(new SingleValueObjectIdentityValueConverter<ElevatorId>());

            modelBuilder
                .Entity<Elevator>()
                .Property(o => o.ElevatorStatus)
                .HasConversion(new ValueObjectValueConverter<ElevatorStatus, ElevatorStatuses>());

            modelBuilder
                .Entity<ManagedElevator>()
                .HasOne<ElevatorControl>()
                .WithMany(c => c.Elevators);

            return modelBuilder;
        }
    }
}
