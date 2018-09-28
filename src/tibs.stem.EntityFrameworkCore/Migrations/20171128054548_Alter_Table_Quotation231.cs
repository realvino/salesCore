using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class Alter_Table_Quotation231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MileStoneId",
                table: "Quotation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_MileStoneId",
                table: "Quotation",
                column: "MileStoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_MileStone_MileStoneId",
                table: "Quotation",
                column: "MileStoneId",
                principalTable: "MileStone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_MileStone_MileStoneId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_MileStoneId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "MileStoneId",
                table: "Quotation");
        }
    }
}
