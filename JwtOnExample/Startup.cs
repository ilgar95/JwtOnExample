using JwtOnExample.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace JwtOnExample
{
    public static class Startup
    {

        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
         
            services.AddScoped<ValidationFilterAttribute>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
       
    }
}
