using Application.Interfaces;
using Application.Services;

namespace IdentitySampleRole.StartUp
{
    public static class InternalDependenciesRegistration
    {
        public static void AddInternalDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register internal dependencies here
            services.AddScoped<IUserService, UserService>();
        }
    }
}
