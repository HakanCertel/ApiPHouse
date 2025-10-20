using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using YayinEviApi.Application;
using YayinEviApi.Infrastructure;
using YayinEviApi.Infrastructure.Services.Storage.Local;
using YayinEviApi.Persistence;
using YayinEviApi.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();//Client'tan gelen request neticvesinde oluþturulan HttpContext nesnesine katmanlardaki class'lar üzerinden(busineess logic) eriþebilmemizi saðlayan bir servistir.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);//uygulama genelinde paylaşılan anahtarları ve değerleri yönetmeyi sağlar. bu radaki kullanım amacı postgreSql ' datetime kayıtlarının yapılabilmesini sağlamaktır. daha sonra bu yapı tüm kontrollerdaki datetime ları utc'ye çevrilerek giderilecektir

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddStorage<LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();
builder.Services.AddSignalRServices();
const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var allowedOrigins = new[]
{
    "https://publishhouse.netlify.app", 
    "http://localhost:4200",
    "https://apiphouse.onrender.com"
};
//builder.Services.AddCors(options => options.AddDefaultPolicy( policy =>
//    policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials())
//);
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials())
);
// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // 1. Yayınlanan (Published) .dll dosyasının adını al
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    // 2. Uygulamanın çalıştığı mevcut dizin ile XML dosyasını birleştir
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // KRİTİK: Dosyanın varlığını kontrol edin ve yoksa uyarıyı atlayın (hata vermez)
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, //Oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanýcý belirlediðimiz deðerdir. -> www.bilmemne.com
            ValidateIssuer = true, //Oluþturulacak token deðerini kimin daðýttýný ifade edeceðimiz alandýr. -> www.myapi.com
            ValidateLifetime = true, //Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
            ValidateIssuerSigningKey = true, //Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden suciry key verisinin doðrulanmasýdýr.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

            NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karþýlýk gelen deðeri User.Identity.Name propertysinden elde edebiliriz.
        };
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseCors();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHubs();

app.Run();
