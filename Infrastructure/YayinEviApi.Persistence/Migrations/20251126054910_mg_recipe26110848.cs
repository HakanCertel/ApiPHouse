using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YayinEviApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg_recipe26110848 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RateofOutage",
                table: "Recipes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RateofOutage",
                table: "Recipes");
        }
    }
}
