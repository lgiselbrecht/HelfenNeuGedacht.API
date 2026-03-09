using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelfenNeuGedacht.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVerified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Organization");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Organization",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
