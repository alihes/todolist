using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListFinalFinal.Data.Migrations
{
    public partial class jobAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    priority = table.Column<int>(type: "int", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    datecreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ownerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.id);
                    table.ForeignKey(
                        name: "FK_jobs_AspNetUsers_ownerId",
                        column: x => x.ownerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_jobs_ownerId",
                table: "jobs",
                column: "ownerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jobs");
        }
    }
}
