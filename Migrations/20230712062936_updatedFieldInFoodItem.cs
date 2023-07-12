using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborView_Inn.Migrations
{
    /// <inheritdoc />
    public partial class updatedFieldInFoodItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "itemName",
                table: "Fooditems",
                newName: "status");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Fooditems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Fooditems");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Fooditems",
                newName: "itemName");
        }
    }
}
