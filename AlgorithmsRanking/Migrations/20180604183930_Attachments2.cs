using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AlgorithmsRanking.Migrations
{
    public partial class Attachments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_DataSets_DataSetId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_DataSetId",
                table: "Attachments");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Attachments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ContentLength",
                table: "Attachments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Attachments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentLength",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Attachments");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Attachments",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_DataSetId",
                table: "Attachments",
                column: "DataSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_DataSets_DataSetId",
                table: "Attachments",
                column: "DataSetId",
                principalTable: "DataSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
