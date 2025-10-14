using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YayinEviApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg_workType14101614 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TypeName",
                table: "WorkTypes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "TypeCode",
                table: "WorkTypes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkTypes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CreatingUserId",
                table: "WorkTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatingUserId",
                table: "WorkTypes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkCategoryId",
                table: "WorkTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypes_WorkCategoryId",
                table: "WorkTypes",
                column: "WorkCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTypes_WorkCategories_WorkCategoryId",
                table: "WorkTypes",
                column: "WorkCategoryId",
                principalTable: "WorkCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTypes_WorkCategories_WorkCategoryId",
                table: "WorkTypes");

            migrationBuilder.DropIndex(
                name: "IX_WorkTypes_WorkCategoryId",
                table: "WorkTypes");

            migrationBuilder.DropColumn(
                name: "CreatingUserId",
                table: "WorkTypes");

            migrationBuilder.DropColumn(
                name: "UpdatingUserId",
                table: "WorkTypes");

            migrationBuilder.DropColumn(
                name: "WorkCategoryId",
                table: "WorkTypes");

            migrationBuilder.AlterColumn<string>(
                name: "TypeName",
                table: "WorkTypes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TypeCode",
                table: "WorkTypes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WorkTypes",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
