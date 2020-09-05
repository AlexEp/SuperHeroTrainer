using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperHeroTrainer.Core.Migrations.AppData
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Ability = table.Column<int>(nullable: false),
                    SuitColor = table.Column<int>(nullable: false),
                    StartingPower = table.Column<double>(nullable: false),
                    Power = table.Column<double>(nullable: false),
                    StartedtoTrain = table.Column<DateTime>(nullable: true),
                    LastTrainDate = table.Column<DateTime>(nullable: true),
                    LastTrainDateCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Heroes");
        }
    }
}
