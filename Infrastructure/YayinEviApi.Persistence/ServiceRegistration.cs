using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.Abstractions.Services.Authentications;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.AgencyFileR;
using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Application.Repositories.HubMessagesR;
using YayinEviApi.Application.Repositories.IAuthorR;
using YayinEviApi.Application.Repositories.IAuthotFileR;
using YayinEviApi.Application.Repositories.ICurrentR;
using YayinEviApi.Application.Repositories.IGoogsAcceptR;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IDepartmentR;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkCategoryR;
using YayinEviApi.Application.Repositories.IHelperEntitiesR.IWorkTypeR;
using YayinEviApi.Application.Repositories.IMaterialR;
using YayinEviApi.Application.Repositories.IProccessCategoryR;
using YayinEviApi.Application.Repositories.IProccessR;
using YayinEviApi.Application.Repositories.IRezervationR;
using YayinEviApi.Application.Repositories.ISaleR;
using YayinEviApi.Application.Repositories.IShipR;
using YayinEviApi.Application.Repositories.IUnitR;
using YayinEviApi.Application.Repositories.IWarehouseR;
using YayinEviApi.Application.Repositories.IWorkOrderR;
using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Application.Repositories.ProccessCategoryR;
using YayinEviApi.Application.Repositories.ProjectR;
using YayinEviApi.Application.Repositories.WorkOrderR;
using YayinEviApi.Application.Services;
using YayinEviApi.Domain.Entities.Identity;
using YayinEviApi.Persistence.Contexts;
using YayinEviApi.Persistence.Repositories;
using YayinEviApi.Persistence.Repositories.AgencyFileR;
using YayinEviApi.Persistence.Repositories.AgencyR;
using YayinEviApi.Persistence.Repositories.AuthorR;
using YayinEviApi.Persistence.Repositories.AuthotFileR;
using YayinEviApi.Persistence.Repositories.CurrentR;
using YayinEviApi.Persistence.Repositories.CustomerR;
using YayinEviApi.Persistence.Repositories.FileMamgementR;
using YayinEviApi.Persistence.Repositories.GoodsAcceptR;
using YayinEviApi.Persistence.Repositories.HelperEntitiesR;
using YayinEviApi.Persistence.Repositories.HelperEntitiesR.DepartmentR;
using YayinEviApi.Persistence.Repositories.HelperEntitiesR.WorkCategoryR;
using YayinEviApi.Persistence.Repositories.HubMessagesR;
using YayinEviApi.Persistence.Repositories.InvoiceFileR;
using YayinEviApi.Persistence.Repositories.MaterialR;
using YayinEviApi.Persistence.Repositories.OrderR;
using YayinEviApi.Persistence.Repositories.ProccessCategoryR;
using YayinEviApi.Persistence.Repositories.ProccessR;
using YayinEviApi.Persistence.Repositories.ProductImageFileR;
using YayinEviApi.Persistence.Repositories.ProductR;
using YayinEviApi.Persistence.Repositories.ProjectR;
using YayinEviApi.Persistence.Repositories.PublishFileR;
using YayinEviApi.Persistence.Repositories.RezervationR;
using YayinEviApi.Persistence.Repositories.SaleR;
using YayinEviApi.Persistence.Repositories.UnitR;
using YayinEviApi.Persistence.Repositories.WarehouseR;
using YayinEviApi.Persistence.Repositories.WorkOrderR;
using YayinEviApi.Persistence.Repositories.WorkR;
using YayinEviApi.Persistence.Services;

namespace YayinEviApi.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services) 
        {
            //services.AddDbContext<YayinEviApiDbContext>(options => 
            //options.UseSqlServer(@"Server=DESKTOP-DNL1VKI;Database=YayinEviDB;Trusted_Connection=True;TrustServerCertificate=True"));
            services.AddDbContext<YayinEviApiDbContext>(options => options.UseSqlServer(Configurations.Connectionstring));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit=false;
                options.Password.RequireLowercase=false;
                options.Password.RequireUppercase=false;
                //options.User.RequireUniqueEmail=true;
            }).AddEntityFrameworkStores<YayinEviApiDbContext>();
           
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IFileManagementReadRepository, FileManagementReadRepository>();
            services.AddScoped<IFileManagementWriteRepository, FileManagementWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWritedRepository>();
            services.AddScoped<IPublishFileReadRepository, PublishFileReadRepository>();
            services.AddScoped<IPublishFileWriteRepository, PublishFileWriteRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();
            services.AddScoped<IWorkTypeReadRepository, WorkTypeReadRepository>();
            services.AddScoped<IWorkTypeWriteRepository, WorkTypeWriteRepository>();
            services.AddScoped<IWorkCategoryWriteRepository, WorkCategoryWriteRepository>();
            services.AddScoped<IWorkCategoryReadRepository, WorkCategoryReadRepository>();
            services.AddScoped<IAgencyReadRepository, AgencyReadRepository>();
            services.AddScoped<IAgencyWriteRepository, AgencyWriteRepository>();
            services.AddScoped<IAgencyFileReadRepository, AgencyFileReadRepository>();
            services.AddScoped<IAgencyFileWriteRepository, AgencyFileWriteRepository>();
            services.AddScoped<IAgencyConnectionInformationReadRepository, AgencyConnectionInformationReadRepository>();
            services.AddScoped<IAgencyConnectionInformationWriteRepository, AgencyConnectioInformationWriteRepository>();
            services.AddScoped<IAuthorReadRepository, AuthorReadRepository>();
            services.AddScoped<IAuthorWriteRepository, AuthorWriteRepository>();
            services.AddScoped<IAuthorFileReadRepository, AuthorFileReadRepository>();
            services.AddScoped<IAuthorFileWriteRepository, AuthorFileWriteRepository>();
            services.AddScoped<IProccessReadRepository, ProccessReadRepository>();    services.AddScoped<IProccessCategoryWriteRepository, ProccessCategoryWriteRepository>(); services.AddScoped<IProccessCategoryReadRepository, ProccessCategoryReadRepository>();
            services.AddScoped<IProccessWriteRepository, ProccessWriteRepository>();
            services.AddScoped<IWorkWriteRepository, WorkWriteRepository>();
            services.AddScoped<IWorkReadRepository, WorkReadRepository>();
            services.AddScoped<IProccessForWorkReadRepository, ProccessForWorkReadRepository>();
            services.AddScoped<IProccessForWorkWriteRepository, ProccessForWorkWriteRepository>();
            services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
            services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
            services.AddScoped<IProjectReadRepository, ProjectReadRepository>();
            services.AddScoped<IProjectWriteRepository, ProjectWriteRepository>();
            services.AddScoped<IWorkOrderWriteRepository, WorkOrderWriteRepository>();
            services.AddScoped<IWorkOrderReadRepository, WorkOrderReadRepository>();
            services.AddScoped<IWorkAssignedUsersReadRepository, WorkAssignedUsersReadRepository>();
            services.AddScoped<IWorkAssignedUsersWriteRepository, WorkAssignedUsersWriteRepository>();
            services.AddScoped<IWorkOrderMessagesWriteRepository, WorkOrderMessagesWriteRepository>();
            services.AddScoped<IWorkOrderMessagesReadRepository, WorkOrderMessagesReadRepository>();
            services.AddScoped<IHubMessageWriteRepository, HubMessageWriteRepository>();
            services.AddScoped<IHubMessageReadRepository, HubMessageReadRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IHallofWarehouseRepository, HallofWarehouseRepository>();
            services.AddScoped<IShelfofWarehouseRepository, ShelfofWarehouseRepository>();
            services.AddScoped<ICellofWareHouseRepository, CellofWarehouseRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IMaterialFileRepository, MaterialFileRepository>();
            services.AddScoped<IUnitRpository, UnitRepository>();
            services.AddScoped<IStockCountRepository, StockCountRepository>();
            services.AddScoped<IStockCountItemsRepository, StockCountItemsRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
            services.AddScoped<ICurrentRepository, CurrentRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleItemRepository, SaleItemRepository>();
            services.AddScoped<IShippingOrderRepository, ShippingOrderRepository>();
            services.AddScoped<IAssignedUserToShippingWorkRepository, AssignedUserToShippingWorkRepository>();
            services.AddScoped<IOrderCollectionRepository, OrderCollectionRepository>();
            services.AddScoped<IOrderCollectionItemRepository, OrderCollectionItemRepository>();
            services.AddScoped<IOrderDistributedRepository, OrderDistributedRepository>();
            services.AddScoped<IOrderItemDistributedRepository, OrderItemDistributedRepository>();
            services.AddScoped<IGoodsAcceptRepository, GoodsAcceptRepository>();
            services.AddScoped<IGoodsAccepItemRepository, GoodsAcceptItemRepository>();
            services.AddScoped<IShipRepository, ShipRepository>();
            services.AddScoped<IShipItemRepository, ShipItemRepository>();
            services.AddScoped<IRezervationRepository, RezervationRepository>();
            services.AddScoped<ITransferDemandBetweenWarehousesRepository, TransferDemandBetweenWarehousesRepository>();
            services.AddScoped<ITransferDemandItemBetweenWarehousesRepository, TransferDemandItemBetweenWarehousesRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IRezervationService, RezervationService>();
            services.AddScoped<IProductService, ProductService>();

        }
    }
}
