using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalCareBackend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class arrumandodatepraString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Animals");

            migrationBuilder.AddColumn<string>(
                name: "birthDate",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthDate",
                table: "Animals");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Animals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
