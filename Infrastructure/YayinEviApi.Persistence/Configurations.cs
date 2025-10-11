using Microsoft.Extensions.Configuration;

namespace YayinEviApi.Persistence
{
    static class Configurations
    {
        static public string Connectionstring
        {

            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/YayinEviApi.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("DefaultConnection");
            }
        }
    }
}
