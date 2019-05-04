using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OOPTut.EntityFramework.Migrations
{
    public partial class CreateBazaarListModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BazaarLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 70, nullable: false),
                    Description = table.Column<string>(maxLength: 160, nullable: true),
                    CreatorUserId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BazaarLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BazaarListItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    BazaarListId = table.Column<int>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BazaarListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BazaarListItems_BazaarLists_BazaarListId",
                        column: x => x.BazaarListId,
                        principalTable: "BazaarLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BazaarListItems_BazaarListId",
                table: "BazaarListItems",
                column: "BazaarListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BazaarListItems");

            migrationBuilder.DropTable(
                name: "BazaarLists");
        }
    }
}
