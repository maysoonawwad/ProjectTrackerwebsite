using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectBusinessLayer.Data.Migrations
{
    public partial class addingMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BandId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Band",
                columns: table => new
                {
                    BandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BandTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Band", x => x.BandId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BandId",
                table: "AspNetUsers",
                column: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Band_BandId",
                table: "AspNetUsers",
                column: "BandId",
                principalTable: "Band",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Band_BandId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Band");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BandId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
