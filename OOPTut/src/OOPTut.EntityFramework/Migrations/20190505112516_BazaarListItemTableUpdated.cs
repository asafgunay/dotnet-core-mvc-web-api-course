using Microsoft.EntityFrameworkCore.Migrations;

namespace OOPTut.EntityFramework.Migrations
{
    public partial class BazaarListItemTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "BazaarListItems",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "BazaarListItems");
        }
    }
}
