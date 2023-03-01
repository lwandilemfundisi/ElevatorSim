using ElevatorSim.Persistence.ElevatorControlModelPersistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSim.Persistence.ElevatorControlModelPersistence
{
    public class ElevatorControlContext : DbContext
    {
        public ElevatorControlContext(DbContextOptions<ElevatorControlContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ElevatorControlModelMap();
        }
    }
}
