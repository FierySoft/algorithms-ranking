using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AlgorithmsRanking.Migrations
{
    public partial class Rates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccuracyRate",
                table: "Researches");

            migrationBuilder.DropColumn(
                name: "EfficiencyRate",
                table: "Researches");

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResearchId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.AddColumn<float>(
                name: "AccuracyRate",
                table: "Researches",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EfficiencyRate",
                table: "Researches",
                nullable: true);
        }
    }
}
