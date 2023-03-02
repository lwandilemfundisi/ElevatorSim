using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevatorSim.Persistence.ElevatorControlModelPersistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElevatorControl",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElevatorControl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManagedElevator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ElevatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElevatorControlId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedElevator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagedElevator_ElevatorControl_ElevatorControlId",
                        column: x => x.ElevatorControlId,
                        principalTable: "ElevatorControl",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagedElevator_ElevatorControlId",
                table: "ManagedElevator",
                column: "ElevatorControlId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagedElevator");

            migrationBuilder.DropTable(
                name: "ElevatorControl");
        }
    }
}
