using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class altertagettenentalterdatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidityDate",
                table: "Targets",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidityDate",
                table: "Targets",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
