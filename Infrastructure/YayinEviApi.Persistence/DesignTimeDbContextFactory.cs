using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using YayinEviApi.Persistence.Contexts;

namespace YayinEviApi.Persistence
{
    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<YayinEviApiDbContext>
    //{
    //    //public YayinEviApiDbContext CreateDbContext(string[] args)
    //    //{
    //    //    //ConfigurationManager configurationManager = new ();
    //    //    //configurationManager.SetBasePath(Path.Combine( Directory.GetCurrentDirectory (),"../../Presentation/YayinEviApi.API"));
    //    //    //configurationManager.AddJsonFile("appsetting.json");

    //    //    DbContextOptionsBuilder<YayinEviApiDbContext> dbContextOptionsBuilder = new();
    //    //    dbContextOptionsBuilder.UseSqlServer(Configurations.Connectionstring);
    //    //    //dbContextOptionsBuilder.UseNpgsql("User ID = postgres; Password = 12345; Host = localhost; Port = 5432; Database = PublishDBBBBB;");
    //    //    return new(dbContextOptionsBuilder.Options);
    //    //}
    //}
}
