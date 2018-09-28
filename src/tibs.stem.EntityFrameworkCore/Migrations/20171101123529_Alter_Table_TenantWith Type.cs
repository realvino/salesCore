using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tibs.stem.Migrations
{
    public partial class Alter_Table_TenantWithType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantTypeId",
                table: "AbpTenants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_TenantTypeId",
                table: "AbpTenants",
                column: "TenantTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTenants_TenantType_TenantTypeId",
                table: "AbpTenants",
                column: "TenantTypeId",
                principalTable: "TenantType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpTenants_TenantType_TenantTypeId",
                table: "AbpTenants");

            migrationBuilder.DropIndex(
                name: "IX_AbpTenants_TenantTypeId",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "TenantTypeId",
                table: "AbpTenants");
        }
    }
}
