using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RailwayApp.Migrations
{
    public partial class ChangeTrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChangeTrainId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ChangeTrainId",
                table: "Orders",
                column: "ChangeTrainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Trains_ChangeTrainId",
                table: "Orders",
                column: "ChangeTrainId",
                principalTable: "Trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Trains_ChangeTrainId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ChangeTrainId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ChangeTrainId",
                table: "Orders");
        }
    }
}
