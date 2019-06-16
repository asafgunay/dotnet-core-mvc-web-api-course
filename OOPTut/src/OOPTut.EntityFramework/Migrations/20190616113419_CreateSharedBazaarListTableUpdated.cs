using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OOPTut.EntityFramework.Migrations
{
    public partial class CreateSharedBazaarListTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharedBazaarLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BazaarListId = table.Column<int>(nullable: true),
                    AllowedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedBazaarLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedBazaarLists_AspNetUsers_AllowedUserId",
                        column: x => x.AllowedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SharedBazaarLists_BazaarLists_BazaarListId",
                        column: x => x.BazaarListId,
                        principalTable: "BazaarLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedBazaarLists_AllowedUserId",
                table: "SharedBazaarLists",
                column: "AllowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedBazaarLists_BazaarListId",
                table: "SharedBazaarLists",
                column: "BazaarListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedBazaarLists");
        }
    }
}
