using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tibs.stem.Migrations
{
    public partial class AddQuotationPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    QuotationId = table.Column<int>(nullable: false),
                    ScheduledDate = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSchedule_Quotation_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCollection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    BankDate = table.Column<DateTime>(nullable: true),
                    ChequeDate = table.Column<DateTime>(nullable: true),
                    ChequeNo = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DueAmount = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PaymentId = table.Column<int>(nullable: true),
                    QuotationId = table.Column<int>(nullable: false),
                    Received = table.Column<bool>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SalesInvoiceNUmber = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    VoucherNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentCollection_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentCollection_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentCollection_Quotation_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCollection_CurrencyId",
                table: "PaymentCollection",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCollection_PaymentId",
                table: "PaymentCollection",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCollection_QuotationId",
                table: "PaymentCollection",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedule_QuotationId",
                table: "PaymentSchedule",
                column: "QuotationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentCollection");

            migrationBuilder.DropTable(
                name: "PaymentSchedule");

            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
