using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterQuotationAddDiscountUSD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OverallDiscountinUSD",
                table: "Quotation",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverallDiscountinUSD",
                table: "Quotation");
        }
    }
}
