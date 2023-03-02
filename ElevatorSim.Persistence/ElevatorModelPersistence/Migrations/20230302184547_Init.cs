using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevatorSim.Persistence.ElevatorModelPersistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elevator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentFloor = table.Column<long>(type: "bigint", nullable: false),
                    Weightlimit = table.Column<long>(type: "bigint", nullable: false),
                    CurrentWeight = table.Column<long>(type: "bigint", nullable: false),
                    ElevatorStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elevator", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elevator");
        }
    }
}
