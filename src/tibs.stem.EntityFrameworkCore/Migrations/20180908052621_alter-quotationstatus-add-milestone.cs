using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterquotationstatusaddmilestone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Lost",
                table: "QuotationStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MileStoneId",
                table: "QuotationStatus",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "New",
                table: "QuotationStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Revised",
                table: "QuotationStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Submitted",
                table: "QuotationStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Won",
                table: "QuotationStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationStatus_MileStoneId",
                table: "QuotationStatus",
                column: "MileStoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationStatus_MileStone_MileStoneId",
                table: "QuotationStatus",
                column: "MileStoneId",
                principalTable: "MileStone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationStatus_MileStone_MileStoneId",
                table: "QuotationStatus");

            migrationBuilder.DropIndex(
                name: "IX_QuotationStatus_MileStoneId",
                table: "QuotationStatus");

            migrationBuilder.DropColumn(
                name: "Lost",
                table: "QuotationStatus");

            migrationBuilder.DropColumn(
                name: "MileStoneId",
                table: "QuotationStatus");

            migrationBuilder.DropColumn(
                name: "New",
                table: "QuotationStatus");

            migrationBuilder.DropColumn(
                name: "Revised",
                table: "QuotationStatus");

            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "QuotationStatus");

            migrationBuilder.DropColumn(
                name: "Won",
                table: "QuotationStatus");
        }
    }
}
