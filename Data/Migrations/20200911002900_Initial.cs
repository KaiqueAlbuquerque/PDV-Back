using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateRegister = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    NoteDescription = table.Column<string>(type: "varchar(17)", nullable: true),
                    ExternalId = table.Column<string>(type: "varchar(100)", nullable: false),
                    MeasuredUnit = table.Column<string>(type: "varchar(100)", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    BarCode = table.Column<string>(type: "varchar(100)", nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateRegister = table.Column<DateTime>(nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    FormOfPayment = table.Column<int>(nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(65,2)", nullable: false),
                    Change = table.Column<decimal>(type: "decimal(65,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    SaleId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => new { x.SaleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductSales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSales_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductId",
                table: "ProductSales",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
