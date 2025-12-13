using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YayinEviApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg_categoryAndMaterial12011319 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainCategoryId",
                table: "Materials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityType = table.Column<byte>(type: "tinyint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory_1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategory_1_MainCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MainCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory_2",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory_2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategory_2_SubCategory_1_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SubCategory_1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MainCategoryId",
                table: "Materials",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_1_ParentId",
                table: "SubCategory_1",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_2_ParentId",
                table: "SubCategory_2",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MainCategory_MainCategoryId",
                table: "Materials",
                column: "MainCategoryId",
                principalTable: "MainCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MainCategory_MainCategoryId",
                table: "Materials");

            migrationBuilder.DropTable(
                name: "SubCategory_2");

            migrationBuilder.DropTable(
                name: "SubCategory_1");

            migrationBuilder.DropTable(
                name: "MainCategory");

            migrationBuilder.DropIndex(
                name: "IX_Materials_MainCategoryId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Materials");
        }
    }
}
