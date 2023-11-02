using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingOnBoard.Migrations
{
    /// <inheritdoc />
    public partial class DeletetCurrentActiveSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveSlots",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "CurrentSlots",
                table: "Streets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveSlots",
                table: "Streets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentSlots",
                table: "Streets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
