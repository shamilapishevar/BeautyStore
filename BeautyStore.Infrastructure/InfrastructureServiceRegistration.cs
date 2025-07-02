using BeautyStore.Application.Interfaces;
using BeautyStore.Application.Settings;
using BeautyStore.Infrastructure.Persistence;
using BeautyStore.Infrastructure.Repositories;
using BeautyStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;



namespace BeautyStore.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // اتصال به دیتابیس
            services.AddDbContext<BeautyStoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // ثبت تنظیمات JWT از appsettings.json
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // ریجستر کردن ریپازیتوری‌ها
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // ثبت سرویس تولید توکن
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
