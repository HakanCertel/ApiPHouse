using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YayinEviApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg_work13101745 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPrintingDate",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "LastPrintingQuantity",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "LasttPrintingDate",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "NameDrawing",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "NameReading",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "NameReducting",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "NameTranslating",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "NameTypeSetting",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "PrintingHouse",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Works");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FirstPrintingDate",
                table: "Works",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastPrintingQuantity",
                table: "Works",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LasttPrintingDate",
                table: "Works",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameDrawing",
                table: "Works",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameReading",
                table: "Works",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameReducting",
                table: "Works",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameTranslating",
                table: "Works",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameTypeSetting",
                table: "Works",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PrintingHouse",
                table: "Works",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Works",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Works",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
