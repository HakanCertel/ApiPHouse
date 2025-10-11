using Microsoft.Extensions.DependencyInjection;
using YayinEviApi.Application.Abstractions.IToken;
using YayinEviApi.Application.Abstractions.Services;
using YayinEviApi.Application.Abstractions.Storage;
using YayinEviApi.Infrastructure.Enums;
using YayinEviApi.Infrastructure.Services;
using YayinEviApi.Infrastructure.Services.Storage;
using YayinEviApi.Infrastructure.Services.Storage.Local;
using YayinEviApi.Infrastructure.Services.TokenS;

namespace YayinEviApi.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IQRCodeService, QRCodeService>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storages , IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection,StorageType storageType) 
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    break;
                case StorageType.AWS:
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
