using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfService.Migrations
{
    /// <inheritdoc />
    public partial class init123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "MedekForms");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MedekForms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MedekForms");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "MedekForms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
