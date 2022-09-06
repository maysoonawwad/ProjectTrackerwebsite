using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectBusinessLayer.Data.Migrations
{
    public partial class addAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkAttachments",
                columns: table => new
                {
                    WorkAttachmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    FileData = table.Column<byte[]>(nullable: true),
                    WorkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAttachments", x => x.WorkAttachmentId);
                    table.ForeignKey(
                        name: "FK_WorkAttachments_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkAttachments_WorkId",
                table: "WorkAttachments",
                column: "WorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkAttachments");
        }
    }
}
