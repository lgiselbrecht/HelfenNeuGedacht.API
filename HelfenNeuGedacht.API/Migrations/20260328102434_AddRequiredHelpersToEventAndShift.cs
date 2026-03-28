using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelfenNeuGedacht.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredHelpersToEventAndShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequiredHelpers",
                table: "Shift",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredHelpers",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredHelpers",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "RequiredHelpers",
                table: "Event");
        }
    }
}
