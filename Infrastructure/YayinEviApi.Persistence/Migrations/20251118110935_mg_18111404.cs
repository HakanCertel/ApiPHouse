using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YayinEviApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg_18111404 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyAgencyFile");

            migrationBuilder.DropTable(
                name: "AgencyConnectionInformations");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignedUserToShippingWork");

            migrationBuilder.DropTable(
                name: "AuthorAuthorFile");

            migrationBuilder.DropTable(
                name: "BasketItem");

            migrationBuilder.DropTable(
                name: "GoodsAcceptItems");

            migrationBuilder.DropTable(
                name: "HubMessages");

            migrationBuilder.DropTable(
                name: "MaterialMaterialFile");

            migrationBuilder.DropTable(
                name: "OrderCollectionItems");

            migrationBuilder.DropTable(
                name: "OrderItemDistributeds");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "ProccessForWorks");

            migrationBuilder.DropTable(
                name: "ProductProductImageFile");

            migrationBuilder.DropTable(
                name: "PublishFileWork");

            migrationBuilder.DropTable(
                name: "Rezervations");

            migrationBuilder.DropTable(
                name: "ShipItems");

            migrationBuilder.DropTable(
                name: "StockCountItems");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "SubAddress");

            migrationBuilder.DropTable(
                name: "Subes");

            migrationBuilder.DropTable(
                name: "TransferDemandItemsBetweenWarehouses");

            migrationBuilder.DropTable(
                name: "WorkAssignedUsers");

            migrationBuilder.DropTable(
                name: "WorkOrderMessages");

            migrationBuilder.DropTable(
                name: "WorkTypes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ShippingOrders");

            migrationBuilder.DropTable(
                name: "GoodsAcceps");

            migrationBuilder.DropTable(
                name: "OrderCollections");

            migrationBuilder.DropTable(
                name: "OrderDistributeds");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "FileManagements");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "StockCounts");

            migrationBuilder.DropTable(
                name: "TransferDemandsBetweenWarehouses");

            migrationBuilder.DropTable(
                name: "ProjectWorkOrders");

            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Proccesses");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProccessCategories");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "CellesofWarehouse");

            migrationBuilder.DropTable(
                name: "MaterialUnits");

            migrationBuilder.DropTable(
                name: "SubCurrent");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "WorkCategories");

            migrationBuilder.DropTable(
                name: "ShelfofWarehouse");

            migrationBuilder.DropTable(
                name: "Currents");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "HallofWarehouses");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
