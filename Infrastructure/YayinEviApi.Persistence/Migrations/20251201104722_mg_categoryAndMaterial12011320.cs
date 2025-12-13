using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YayinEviApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg_categoryAndMaterial12011320 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubCategory_1Id",
                table: "Materials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategory_2Id",
                table: "Materials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubCategory_1Id",
                table: "Materials",
                column: "SubCategory_1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_SubCategory_2Id",
                table: "Materials",
                column: "SubCategory_2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_SubCategory_1_SubCategory_1Id",
                table: "Materials",
                column: "SubCategory_1Id",
                principalTable: "SubCategory_1",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_SubCategory_2_SubCategory_2Id",
                table: "Materials",
                column: "SubCategory_2Id",
                principalTable: "SubCategory_2",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_SubCategory_1_SubCategory_1Id",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_SubCategory_2_SubCategory_2Id",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_SubCategory_1Id",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_SubCategory_2Id",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "SubCategory_1Id",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "SubCategory_2Id",
                table: "Materials");
        }
    }
}
