using ElevatorSim.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ElevatorSim.Persistence
{
    public class ElevatorSimContext : DbContext
    {
        public ElevatorSimContext(DbContextOptions<ElevatorSimContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ElevatorSimModelMap();
        }
    }
}
