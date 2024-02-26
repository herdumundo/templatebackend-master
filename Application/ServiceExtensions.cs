using Application.Services;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<ICircunscripcionesService, CircunscripcionService>();
            services.AddTransient<ILocalidadesService, LocalidadService>();
            services.AddTransient<IUsuarioBBDDPoderJudicialService, UsuarioBBDDPoderJudicialService>();
        }
    }
}
