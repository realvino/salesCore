using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterQuotationAddvat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Vat",
                table: "Quotation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "VatAmount",
                table: "Quotation",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VatPercentage",
                table: "Quotation",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vat",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "VatAmount",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "VatPercentage",
                table: "Quotation");
        }
    }
}
