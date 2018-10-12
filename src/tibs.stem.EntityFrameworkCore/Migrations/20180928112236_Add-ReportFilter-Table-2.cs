using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tibs.stem.Migrations
{
    public partial class AddReportFilterTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportGenerator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    CustomerType = table.Column<string>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EnquiryCreationTime = table.Column<DateTime>(nullable: false),
                    EnquiryCreationTypeId = table.Column<int>(nullable: false),
                    HiddenColumns = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LostDate = table.Column<DateTime>(nullable: false),
                    LostDateId = table.Column<int>(nullable: false),
                    MileStone = table.Column<string>(nullable: true),
                    MileStoneStatus = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    QuotationCreationTime = table.Column<DateTime>(nullable: false),
                    QuotationCreationTypeId = table.Column<int>(nullable: false),
                    QuotationStatus = table.Column<string>(nullable: true),
                    ReportTypeId = table.Column<int>(nullable: false),
                    Salesperson = table.Column<string>(nullable: true),
                    SubmitttedDate = table.Column<DateTime>(nullable: false),
                    SubmitttedDateId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    WonDate = table.Column<DateTime>(nullable: false),
                    WonDateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportGenerator", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportGenerator");
        }
    }
}
