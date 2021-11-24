using Microsoft.EntityFrameworkCore.Migrations;

namespace RailwayApp.Migrations
{
    public partial class NewSeatsAtTrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatsCount",
                table: "Trains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Routes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatsCount",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Routes");
        }
    }
}
