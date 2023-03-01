using ElevatorSim.Persistence.ElevatorModelPersistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSim.Persistence.ElevatorModelPersistence
{
    public class ElevatorContext : DbContext
    {
        public ElevatorContext(DbContextOptions<ElevatorContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ElevatorModelMap();
        }
    }
}
