using Microsoft.AspNetCore.Http;

namespace YayinEviApi.Infrastructure.Operations
{
    public static class GetUserName
    {
        //private IHttpContextAccessor _httpContextAccessor=new HttpContextAccessor();
        public static string? UserName(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        }
    }
}
