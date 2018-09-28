using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class alterQuotationaddCompatitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompetitorId",
                table: "Quotation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_CompetitorId",
                table: "Quotation",
                column: "CompetitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_Company_CompetitorId",
                table: "Quotation",
                column: "CompetitorId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_Company_CompetitorId",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_CompetitorId",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "CompetitorId",
                table: "Quotation");
        }
    }
}
