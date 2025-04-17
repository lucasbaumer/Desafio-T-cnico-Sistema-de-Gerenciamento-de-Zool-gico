using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalCareBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alterandoRelacaoAnimalCare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnimalId1",
                table: "AnimalCares",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CareId1",
                table: "AnimalCares",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalCares_AnimalId1",
                table: "AnimalCares",
                column: "AnimalId1");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalCares_CareId1",
                table: "AnimalCares",
                column: "CareId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalCares_Animals_AnimalId1",
                table: "AnimalCares",
                column: "AnimalId1",
                principalTable: "Animals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalCares_Cares_CareId1",
                table: "AnimalCares",
                column: "CareId1",
                principalTable: "Cares",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalCares_Animals_AnimalId1",
                table: "AnimalCares");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalCares_Cares_CareId1",
                table: "AnimalCares");

            migrationBuilder.DropIndex(
                name: "IX_AnimalCares_AnimalId1",
                table: "AnimalCares");

            migrationBuilder.DropIndex(
                name: "IX_AnimalCares_CareId1",
                table: "AnimalCares");

            migrationBuilder.DropColumn(
                name: "AnimalId1",
                table: "AnimalCares");

            migrationBuilder.DropColumn(
                name: "CareId1",
                table: "AnimalCares");
        }
    }
}
