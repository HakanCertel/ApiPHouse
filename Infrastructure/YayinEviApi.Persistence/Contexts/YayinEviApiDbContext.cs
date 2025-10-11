using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YayinEviApi.Domain.Entities.Identity;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.HelperEntities;
using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Domain.Entities.Şube;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessE;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Domain.Entities.ProjectE;
using YayinEviApi.Domain.Entities.HelperEntities.ProccessCategoryE;
using YayinEviApi.Domain.Entities.WorkOrderE;
using YayinEviApi.Domain.Entities.HubMessagesE;
using YayinEviApi.Domain.Entities.WarehouseE;
using YayinEviApi.Domain.Entities.MaterialE;
using YayinEviApi.Domain.Entities.UnitE;
using YayinEviApi.Domain.Entities.GoodsAcceptE;
using YayinEviApi.Domain.Entities.GoodsAccepE;
using YayinEviApi.Domain.Entities.CurrentE;
using YayinEviApi.Domain.Entities.SalesE;
using YayinEviApi.Domain.Entities.RezervationE;
using YayinEviApi.Domain.Entities.ShipE;

namespace YayinEviApi.Persistence.Contexts
{
    public class YayinEviApiDbContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public YayinEviApiDbContext(DbContextOptions options):base(options)
        {          
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FileManagement> FileManagements { get; set; }
        public DbSet<PublishFile> PublishFiles { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<WorkCategory> WorkCategories { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AgencyConnectionInformation> AgencyConnectionInformations { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Sube> Subes { get; set; }
        public DbSet<Proccess> Proccesses { get; set; }
        public DbSet<ProccessCategory> ProccessCategories { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProccessForWork> ProccessForWorks { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<WorkOrder> ProjectWorkOrders { get; set; }
        public DbSet<WorkAssignedUsers> WorkAssignedUsers { get; set; }
        public DbSet<WorkOrderMessages> WorkOrderMessages { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<HallofWarehouse> HallofWarehouses { get; set; }
        public DbSet<CellofWarehouse> CellesofWarehouse { get; set; }
        public DbSet<ShelfofWarehouse> ShelfofWarehouse { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialUnit> MaterialUnits { get; set; }
        public DbSet<StockCount> StockCounts { get; set; }
        public DbSet<StockCountItems> StockCountItems { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Current> Currents { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<ShippingOrder> ShippingOrders { get; set; }
        public DbSet<AssignedUserToShippingWork> AssignedUserToShippingWork { get; set; }
        public DbSet<OrderCollection> OrderCollections { get; set; }
        public DbSet<OrderCollectionItem> OrderCollectionItems { get; set; }
        public DbSet<OrderDistributed> OrderDistributeds { get; set; }
        public DbSet<OrderItemDistributed> OrderItemDistributeds { get; set; }
        public DbSet<GoodsAccep> GoodsAcceps { get; set; }
        public DbSet<GoodsAcceptItems> GoodsAcceptItems { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipItem> ShipItems { get; set; }
        public DbSet<Rezervation> Rezervations { get; set; }
        public DbSet<TransferDemandBetweenWarehouses> TransferDemandsBetweenWarehouses { get; set; }
        public DbSet<TransferDemandItemBetweenWarehouses> TransferDemandItemsBetweenWarehouses { get; set; }
        public DbSet<HubMessage> HubMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Order>()
                .HasKey(b => b.Id);

            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.Id);
            
            //builder.Entity<Order>()
            //    .HasOne(o => o.CompletedOrder)
            //    .WithOne(c => c.Order)
            //    .HasForeignKey<CompletedOrder>(c => c.OrderId);


            base.OnModelCreating(builder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);

        }
    }
}
