using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelfenNeuGedacht.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEventIdToShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Event_HelpingEventsId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Shift_HelpingEventsId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "HelpingEventsId",
                table: "Shift");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Shift",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shift_EventId",
                table: "Shift",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Event_EventId",
                table: "Shift",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Event_EventId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Shift_EventId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Shift");

            migrationBuilder.AddColumn<int>(
                name: "HelpingEventsId",
                table: "Shift",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shift_HelpingEventsId",
                table: "Shift",
                column: "HelpingEventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Event_HelpingEventsId",
                table: "Shift",
                column: "HelpingEventsId",
                principalTable: "Event",
                principalColumn: "Id");
        }
    }
}
