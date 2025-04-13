using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalCareBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class arrumandoCare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalCares",
                table: "AnimalCares");

            migrationBuilder.DropIndex(
                name: "IX_AnimalCares_AnimalId",
                table: "AnimalCares");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalCares",
                table: "AnimalCares",
                columns: new[] { "AnimalId", "CareId" });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalCares_CareId",
                table: "AnimalCares",
                column: "CareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalCares",
                table: "AnimalCares");

            migrationBuilder.DropIndex(
                name: "IX_AnimalCares_CareId",
                table: "AnimalCares");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalCares",
                table: "AnimalCares",
                column: "CareId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalCares_AnimalId",
                table: "AnimalCares",
                column: "AnimalId");
        }
    }
}
