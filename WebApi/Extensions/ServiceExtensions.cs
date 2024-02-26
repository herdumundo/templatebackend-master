using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using WebApi.ValidationHandlers;
namespace WebApi.Extensions;

public static class ServiceExtensions
{
    public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<IValidator<UsuarioBasedeDatosPoderJudicial>, UsuarioBBDDPoderJudicialValidator>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = TokenSesionAuthenticationSchemeOptions.DefaultScheme;
            options.DefaultChallengeScheme = TokenSesionAuthenticationSchemeOptions.DefaultScheme;
        })
            .AddScheme<TokenSesionAuthenticationSchemeOptions, TokenSesionAuthenticationHandler>
            (TokenSesionAuthenticationSchemeOptions.DefaultScheme, options => {});

        
        services.AddSingleton<IAuthorizationHandler, RolAuthorizationHandler>();  
        services.AddAuthorization(options =>
        {         
            options.AddPolicy("LeerTodos", policy =>
            {
                policy.Requirements.Add(new RolRequirement("LeerTodos"));
            });
            options.AddPolicy("LeerporCodigo", policy =>
            {
                policy.Requirements.Add(new RolRequirement("LeerporCodigo"));
            });
            options.AddPolicy("AgregarRegistro", policy =>
            {
                policy.Requirements.Add(new RolRequirement("AgregarRegistro"));
            });
            options.AddPolicy("ActualizarRegistro", policy =>
            {
                policy.Requirements.Add(new RolRequirement("ActualizarRegistro"));
            });
            options.AddPolicy("OtraCosa", policy =>
            {
                policy.Requirements.Add(new RolRequirement("OtraCosa"));
            });
        });



    }
}

