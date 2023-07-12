using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborView_Inn.Migrations
{
    /// <inheritdoc />
    public partial class updatedTablesOfDining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Fooditems");

            migrationBuilder.DropColumn(
                name: "bookedTable",
                table: "DiningTable");

            migrationBuilder.DropColumn(
                name: "noOfGuest",
                table: "DiningTable");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DiningTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "DiningTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time",
                table: "DiningTable",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "DiningTable");

            migrationBuilder.DropColumn(
                name: "status",
                table: "DiningTable");

            migrationBuilder.DropColumn(
                name: "time",
                table: "DiningTable");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Fooditems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "bookedTable",
                table: "DiningTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "noOfGuest",
                table: "DiningTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
