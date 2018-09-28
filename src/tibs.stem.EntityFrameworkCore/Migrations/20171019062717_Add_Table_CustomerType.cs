using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tibs.stem.Migrations
{
    public partial class Add_Table_CustomerType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryCode = table.Column<string>(nullable: true),
                    CountryName = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ISDCode = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    ConversionRatio = table.Column<decimal>(nullable: false),
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
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Company = table.Column<bool>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfoType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactName = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Info = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DeliveryCode = table.Column<string>(nullable: true),
                    DeliveryName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MileStone",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MileStone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MileStoneStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MileStoneStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packing",
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
                    PackingCode = table.Column<string>(nullable: true),
                    PackingName = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceLevel",
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
                    PriceLevelCode = table.Column<string>(nullable: true),
                    PriceLevelName = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
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
                    Path = table.Column<string>(nullable: true),
                    ProductGroupCode = table.Column<string>(nullable: true),
                    ProductGroupName = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QPayment",
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
                    PaymentCode = table.Column<string>(nullable: true),
                    PaymentName = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QPayment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuotationStatus",
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
                    QuotationStatusCode = table.Column<string>(nullable: true),
                    QuotationStatusName = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reason",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
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
                    ServiceCode = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TargetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_TargetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantTargets",
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
                    TargetDate = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantTargets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_TenantType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TitleOfCourtesy",
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
                    table.PrimaryKey("PK_TitleOfCourtesy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TitleOfQuotation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleOfQuotation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Validity",
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
                    TenantId = table.Column<int>(nullable: false),
                    ValidityCode = table.Column<string>(nullable: true),
                    ValidityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warranty",
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
                    TenantId = table.Column<int>(nullable: false),
                    WarrantyCode = table.Column<string>(nullable: true),
                    WarrantyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    ConversionRatio = table.Column<decimal>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Online = table.Column<bool>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomCurrency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomCurrency_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountManagerId = table.Column<long>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    CustomerTypeId = table.Column<int>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_AbpUsers_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_CustomerType_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MileStoneStatusDetail",
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
                    MileStoneId = table.Column<int>(nullable: false),
                    MileStoneStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MileStoneStatusDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MileStoneStatusDetail_MileStone_MileStoneId",
                        column: x => x.MileStoneId,
                        principalTable: "MileStone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MileStoneStatusDetail_MileStoneStatus_MileStoneStatusId",
                        column: x => x.MileStoneStatusId,
                        principalTable: "MileStoneStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSubGroup",
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
                    Path = table.Column<string>(nullable: true),
                    ProductGroupId = table.Column<int>(nullable: false),
                    ProductSubGroupCode = table.Column<string>(nullable: true),
                    ProductSubGroupName = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubGroup_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Targets",
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
                    TargetAmount = table.Column<int>(nullable: false),
                    TargetTypeId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    ValidityDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Targets_TargetTypes_TargetTypeId",
                        column: x => x.TargetTypeId,
                        principalTable: "TargetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Targets_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CustomerTypeId = table.Column<int>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    TitleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_CustomerType_CustomerTypeId",
                        column: x => x.CustomerTypeId,
                        principalTable: "CustomerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_TitleOfCourtesy_TitleId",
                        column: x => x.TitleId,
                        principalTable: "TitleOfCourtesy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Discontinued = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    ProductSubGroupId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductSubGroup_ProductSubGroupId",
                        column: x => x.ProductSubGroupId,
                        principalTable: "ProductSubGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    ContacId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    InfoTypeId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressInformation_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformation_Contact_ContacId",
                        column: x => x.ContacId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressInformation_InfoType_InfoTypeId",
                        column: x => x.InfoTypeId,
                        principalTable: "InfoType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(nullable: true),
                    ContacId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    InfoData = table.Column<string>(nullable: true),
                    InfoTypeId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInformation_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactInformation_Contact_ContacId",
                        column: x => x.ContacId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactInformation_InfoType_InfoTypeId",
                        column: x => x.InfoTypeId,
                        principalTable: "InfoType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enquiry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CloseDate = table.Column<DateTime>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EnquiryNo = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MileStoneId = table.Column<int>(nullable: false),
                    MileStoneStatusId = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enquiry_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enquiry_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enquiry_MileStone_MileStoneId",
                        column: x => x.MileStoneId,
                        principalTable: "MileStone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enquiry_MileStoneStatus_MileStoneStatusId",
                        column: x => x.MileStoneStatusId,
                        principalTable: "MileStoneStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceLevelProduct",
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
                    Price = table.Column<double>(nullable: false),
                    PriceLevelId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLevelProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceLevelProduct_PriceLevel_PriceLevelId",
                        column: x => x.PriceLevelId,
                        principalTable: "PriceLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceLevelProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityTypeId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EnquiryId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ScheduleTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_ActivityType_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activity_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activity_Enquiry_EnquiryId",
                        column: x => x.EnquiryId,
                        principalTable: "Enquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quotation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Archived = table.Column<bool>(nullable: true),
                    ClosureDate = table.Column<DateTime>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    ContactId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    CustomerPONumber = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DeliveryId = table.Column<int>(nullable: true),
                    EnquiryId = table.Column<int>(nullable: true),
                    ExchangeRate = table.Column<decimal>(nullable: false),
                    FreightId = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LostDate = table.Column<DateTime>(nullable: true),
                    OverallDiscount = table.Column<decimal>(nullable: false),
                    PackingId = table.Column<int>(nullable: true),
                    PaymentId = table.Column<int>(nullable: true),
                    ProjectRef = table.Column<string>(nullable: true),
                    ProposalNumber = table.Column<string>(nullable: true),
                    QuotationTitleId = table.Column<int>(nullable: true),
                    ReasonId = table.Column<int>(nullable: true),
                    Revised = table.Column<bool>(nullable: true),
                    SalesOrderNumber = table.Column<string>(nullable: true),
                    SalesmanId = table.Column<long>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    SubjectName = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    ValidityId = table.Column<int>(nullable: true),
                    WarrantyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotation_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Delivery_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Delivery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Enquiry_EnquiryId",
                        column: x => x.EnquiryId,
                        principalTable: "Enquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Freight_FreightId",
                        column: x => x.FreightId,
                        principalTable: "Freight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Packing_PackingId",
                        column: x => x.PackingId,
                        principalTable: "Packing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_QPayment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "QPayment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_TitleOfQuotation_QuotationTitleId",
                        column: x => x.QuotationTitleId,
                        principalTable: "TitleOfQuotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Reason_ReasonId",
                        column: x => x.ReasonId,
                        principalTable: "Reason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_AbpUsers_SalesmanId",
                        column: x => x.SalesmanId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_QuotationStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "QuotationStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Validity_ValidityId",
                        column: x => x.ValidityId,
                        principalTable: "Validity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quotation_Warranty_WarrantyId",
                        column: x => x.WarrantyId,
                        principalTable: "Warranty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityCommand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityId = table.Column<int>(nullable: false),
                    Commands = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityCommand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityCommand_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Discount = table.Column<bool>(nullable: true),
                    EstimatedPrice = table.Column<decimal>(nullable: false),
                    EstimatedPriceUSD = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Optional = table.Column<bool>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PriceLevelProductId = table.Column<int>(nullable: true),
                    PriceUSD = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    QuotationId = table.Column<int>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationProduct_PriceLevelProduct_PriceLevelProductId",
                        column: x => x.PriceLevelProductId,
                        principalTable: "PriceLevelProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationProduct_Quotation_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationService",
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
                    Price = table.Column<decimal>(nullable: false),
                    QuotationId = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationService_Quotation_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "Quotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityCommand_ActivityId",
                table: "ActivityCommand",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ActivityTypeId",
                table: "Activity",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ContactId",
                table: "Activity",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_EnquiryId",
                table: "Activity",
                column: "EnquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomCurrency_CurrencyId",
                table: "CustomCurrency",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformation_CompanyId",
                table: "AddressInformation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformation_ContacId",
                table: "AddressInformation",
                column: "ContacId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressInformation_InfoTypeId",
                table: "AddressInformation",
                column: "InfoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_AccountManagerId",
                table: "Company",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CountryId",
                table: "Company",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CurrencyId",
                table: "Company",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CustomerTypeId",
                table: "Company",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CompanyId",
                table: "Contact",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CustomerTypeId",
                table: "Contact",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_TitleId",
                table: "Contact",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_CompanyId",
                table: "ContactInformation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_ContacId",
                table: "ContactInformation",
                column: "ContacId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_InfoTypeId",
                table: "ContactInformation",
                column: "InfoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiry_CompanyId",
                table: "Enquiry",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiry_ContactId",
                table: "Enquiry",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiry_MileStoneId",
                table: "Enquiry",
                column: "MileStoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiry_MileStoneStatusId",
                table: "Enquiry",
                column: "MileStoneStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MileStoneStatusDetail_MileStoneId",
                table: "MileStoneStatusDetail",
                column: "MileStoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MileStoneStatusDetail_MileStoneStatusId",
                table: "MileStoneStatusDetail",
                column: "MileStoneStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLevelProduct_PriceLevelId",
                table: "PriceLevelProduct",
                column: "PriceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLevelProduct_ProductId",
                table: "PriceLevelProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductSubGroupId",
                table: "Product",
                column: "ProductSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubGroup_ProductGroupId",
                table: "ProductSubGroup",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationProduct_PriceLevelProductId",
                table: "QuotationProduct",
                column: "PriceLevelProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationProduct_ProductId",
                table: "QuotationProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationProduct_QuotationId",
                table: "QuotationProduct",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_CompanyId",
                table: "Quotation",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_ContactId",
                table: "Quotation",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_CurrencyId",
                table: "Quotation",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_DeliveryId",
                table: "Quotation",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_EnquiryId",
                table: "Quotation",
                column: "EnquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_FreightId",
                table: "Quotation",
                column: "FreightId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_PackingId",
                table: "Quotation",
                column: "PackingId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_PaymentId",
                table: "Quotation",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_QuotationTitleId",
                table: "Quotation",
                column: "QuotationTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_ReasonId",
                table: "Quotation",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_SalesmanId",
                table: "Quotation",
                column: "SalesmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_StatusId",
                table: "Quotation",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_ValidityId",
                table: "Quotation",
                column: "ValidityId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_WarrantyId",
                table: "Quotation",
                column: "WarrantyId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationService_QuotationId",
                table: "QuotationService",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationService_ServiceId",
                table: "QuotationService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Targets_TargetTypeId",
                table: "Targets",
                column: "TargetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Targets_UserId",
                table: "Targets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityCommand");

            migrationBuilder.DropTable(
                name: "CustomCurrency");

            migrationBuilder.DropTable(
                name: "AddressInformation");

            migrationBuilder.DropTable(
                name: "ContactInformation");

            migrationBuilder.DropTable(
                name: "MileStoneStatusDetail");

            migrationBuilder.DropTable(
                name: "QuotationProduct");

            migrationBuilder.DropTable(
                name: "QuotationService");

            migrationBuilder.DropTable(
                name: "Targets");

            migrationBuilder.DropTable(
                name: "TenantTargets");

            migrationBuilder.DropTable(
                name: "TenantType");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "InfoType");

            migrationBuilder.DropTable(
                name: "PriceLevelProduct");

            migrationBuilder.DropTable(
                name: "Quotation");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "TargetTypes");

            migrationBuilder.DropTable(
                name: "ActivityType");

            migrationBuilder.DropTable(
                name: "PriceLevel");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "Enquiry");

            migrationBuilder.DropTable(
                name: "Packing");

            migrationBuilder.DropTable(
                name: "QPayment");

            migrationBuilder.DropTable(
                name: "TitleOfQuotation");

            migrationBuilder.DropTable(
                name: "Reason");

            migrationBuilder.DropTable(
                name: "QuotationStatus");

            migrationBuilder.DropTable(
                name: "Validity");

            migrationBuilder.DropTable(
                name: "Warranty");

            migrationBuilder.DropTable(
                name: "ProductSubGroup");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "MileStone");

            migrationBuilder.DropTable(
                name: "MileStoneStatus");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "TitleOfCourtesy");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "CustomerType");
        }
    }
}
