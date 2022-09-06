using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectBusinessLayer.Data.Migrations
{
    public partial class addingeNewmClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Band_BandId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Band",
                table: "Band");

            migrationBuilder.RenameTable(
                name: "Band",
                newName: "Bands");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bands",
                table: "Bands",
                column: "BandId");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTitle = table.Column<string>(nullable: true),
                    ProjectDescription = table.Column<string>(nullable: true),
                    DeadLine = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "projectsMembers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectsMembers", x => new { x.ProjectId, x.Id });
                    table.ForeignKey(
                        name: "FK_projectsMembers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_projectsMembers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    SprintId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    TeamLeaderId = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.SprintId);
                    table.ForeignKey(
                        name: "FK_Sprints_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sprints_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sprints_AspNetUsers_TeamLeaderId",
                        column: x => x.TeamLeaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Duties",
                columns: table => new
                {
                    DutyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DutyName = table.Column<string>(nullable: true),
                    DutyDescription = table.Column<string>(nullable: true),
                    TeamMemberId = table.Column<string>(nullable: true),
                    SprintId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duties", x => x.DutyId);
                    table.ForeignKey(
                        name: "FK_Duties_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "SprintId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duties_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duties_AspNetUsers_TeamMemberId",
                        column: x => x.TeamMemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    WorkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkDescription = table.Column<string>(nullable: true),
                    DutyId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.WorkId);
                    table.ForeignKey(
                        name: "FK_Works_Duties_DutyId",
                        column: x => x.DutyId,
                        principalTable: "Duties",
                        principalColumn: "DutyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Works_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkHistories",
                columns: table => new
                {
                    WorkHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(nullable: false),
                    WorkId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHistories", x => x.WorkHistoryId);
                    table.ForeignKey(
                        name: "FK_WorkHistories_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkHistories_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "WorkId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Duties_SprintId",
                table: "Duties",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Duties_StatusId",
                table: "Duties",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Duties_TeamMemberId",
                table: "Duties",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StatusId",
                table: "Projects",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_projectsMembers_Id",
                table: "projectsMembers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectId",
                table: "Sprints",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_StatusId",
                table: "Sprints",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_TeamLeaderId",
                table: "Sprints",
                column: "TeamLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistories_StatusId",
                table: "WorkHistories",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistories_WorkId",
                table: "WorkHistories",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_DutyId",
                table: "Works",
                column: "DutyId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_StatusId",
                table: "Works",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bands_BandId",
                table: "AspNetUsers",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bands_BandId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "projectsMembers");

            migrationBuilder.DropTable(
                name: "WorkHistories");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Duties");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bands",
                table: "Bands");

            migrationBuilder.RenameTable(
                name: "Bands",
                newName: "Band");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Band",
                table: "Band",
                column: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Band_BandId",
                table: "AspNetUsers",
                column: "BandId",
                principalTable: "Band",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
