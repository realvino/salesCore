using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class AlterUsertableaddProfilepic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AbpUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AbpUsers");
        }
    }
}
