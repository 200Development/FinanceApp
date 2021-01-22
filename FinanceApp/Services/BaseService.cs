using FinanceApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FinanceApp.Services
{
    public class BaseService
    {
        public readonly ApplicationDbContext Context;
        protected readonly IConfiguration Config;
        protected readonly string UserName;

        protected BaseService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Config = config;
            UserName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        }
    }
}
