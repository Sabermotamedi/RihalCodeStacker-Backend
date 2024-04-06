using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rihal.ReelRise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Extension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "MemoryPhoto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Size",
                table: "MemoryPhoto",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UniqueName",
                table: "MemoryPhoto",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "MemoryPhoto");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "MemoryPhoto");

            migrationBuilder.DropColumn(
                name: "UniqueName",
                table: "MemoryPhoto");
        }
    }
}
