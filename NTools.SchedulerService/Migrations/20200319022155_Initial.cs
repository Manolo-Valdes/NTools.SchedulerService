using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NTools.SchedulerService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchedulerTasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Scheduler = table.Column<string>(nullable: true),
                    TaskEngine = table.Column<string>(nullable: true),
                    LastRunTime = table.Column<DateTime>(nullable: false),
                    NextRunTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerTasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulerTasks");
        }
    }
}
