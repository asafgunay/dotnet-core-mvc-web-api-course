using Microsoft.EntityFrameworkCore.Migrations;

namespace OOPTut.EntityFramework.Migrations
{
    public partial class NavBarTableAddedOrderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "NavBarMenuItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "NavBarMenuItems");
        }
    }
}
