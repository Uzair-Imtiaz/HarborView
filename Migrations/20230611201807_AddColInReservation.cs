﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborView_Inn.Migrations
{
    /// <inheritdoc />
    public partial class AddColInReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bookedRoom",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookedRoom",
                table: "Reservation");
        }
    }
}
