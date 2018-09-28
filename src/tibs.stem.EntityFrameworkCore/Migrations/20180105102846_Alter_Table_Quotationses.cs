using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class Alter_Table_Quotationses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Lost",
                table: "Quotation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Submitted",
                table: "Quotation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedDate",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Won",
                table: "Quotation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "WonDate",
                table: "Quotation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lost",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "Submitted",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "SubmittedDate",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "Won",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "WonDate",
                table: "Quotation");
        }
    }
}
