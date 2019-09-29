using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodNews.Migrations
{
    public partial class identity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationInitiatorId",
                table: "Works",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserInitiatorId",
                table: "Works",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fee53301-5aa6-4a87-8d54-b54c6bd8bbc5");

            migrationBuilder.CreateIndex(
                name: "IX_Works_OrganizationInitiatorId",
                table: "Works",
                column: "OrganizationInitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_UserInitiatorId",
                table: "Works",
                column: "UserInitiatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Organization_OrganizationInitiatorId",
                table: "Works",
                column: "OrganizationInitiatorId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_AspNetUsers_UserInitiatorId",
                table: "Works",
                column: "UserInitiatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Organization_OrganizationInitiatorId",
                table: "Works");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_AspNetUsers_UserInitiatorId",
                table: "Works");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Works_OrganizationInitiatorId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_UserInitiatorId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "OrganizationInitiatorId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "UserInitiatorId",
                table: "Works");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e1daa267-6ce6-41d5-a454-b83c93b46686");
        }
    }
}
