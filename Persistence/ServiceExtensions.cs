using Application.Services.Interfaces.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Persistence.Repositories.IRepository;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<DbConnections>();
            services.AddTransient<ICircunscripcionRepository, CircunscripcionRepository>();
            services.AddTransient<ILocalidadRepository, LocalidadRepository>();
            services.AddTransient<IUsuarioBBDDPoderJudicialRepository, UsuarioBBDDPoderJudicialRepository>();
        }
    }
}
