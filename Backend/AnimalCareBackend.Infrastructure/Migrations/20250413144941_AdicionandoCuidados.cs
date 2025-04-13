using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalCareBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCuidados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "careName",
                table: "Cares",
                newName: "CareName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CareName",
                table: "Cares",
                newName: "careName");
        }
    }
}
