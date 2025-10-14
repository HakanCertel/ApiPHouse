using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YayinEviApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg_workCategory14101434 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkCategories_WorkTypes_WorkTypeId",
                table: "WorkCategories");

            migrationBuilder.DropIndex(
                name: "IX_WorkCategories_WorkTypeId",
                table: "WorkCategories");

            migrationBuilder.DropColumn(
                name: "WorkTypeId",
                table: "WorkCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkCategories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkCategories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "WorkCategories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CreatingUserId",
                table: "WorkCategories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatingUserId",
                table: "WorkCategories",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatingUserId",
                table: "WorkCategories");

            migrationBuilder.DropColumn(
                name: "UpdatingUserId",
                table: "WorkCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkCategories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkCategories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "WorkCategories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkTypeId",
                table: "WorkCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkCategories_WorkTypeId",
                table: "WorkCategories",
                column: "WorkTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkCategories_WorkTypes_WorkTypeId",
                table: "WorkCategories",
                column: "WorkTypeId",
                principalTable: "WorkTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
