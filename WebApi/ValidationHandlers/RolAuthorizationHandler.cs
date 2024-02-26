using Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;

namespace WebApi.ValidationHandlers;

public class RolRequirement : IAuthorizationRequirement
{
    public string Recurso { get; }
    public RolRequirement(string recurso)
    {
        Recurso = recurso;
    }
}
public class RolAuthorizationHandler : AuthorizationHandler<RolRequirement>
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<RolAuthorizationHandler> _logger;

    public RolAuthorizationHandler(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger<RolAuthorizationHandler> logger)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolRequirement requirement)
    {
        _logger.LogInformation("Inicio de proceso para validar al usuario logeado");

        try
        {
            var selectedRolId = _httpContextAccessor.HttpContext?.Request.Headers["Usuario-rol"].ToString().Trim();         

            if (string.IsNullOrEmpty(selectedRolId))
            {
                _logger.LogWarning("Ocurrio un error al momento de validar al usuario logeado");
                throw new ErrorAutorizacionUsuarioException("Usuario no Autorizado para consumir el recurso seleccionado");
            }
       
            if (context.User.HasClaim(c => c.Value == selectedRolId) && RecursoAutorizado(selectedRolId, requirement.Recurso))
            {
                _logger.LogInformation($"Fin de proceso para validar al usuario logeado - Usuario autorizado para el recurso: {requirement.Recurso}");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation($"Fin de proceso para validar al usuario logeado - Usuario no autorizado para el recurso: {requirement.Recurso}");
                context.Fail();
            }

            return Task.CompletedTask;    
        }
        catch (ErrorAutorizacionUsuarioException)
        {           
            throw;                
        }       
    }

    private bool RecursoAutorizado(string id, string recurso)
    {
        var rolesConfiguracion = _configuration.GetSection("Settings:Roles").GetChildren();
  
        var recursos = rolesConfiguracion
            .Select(role => role.Get<RolConfiguracion>())
            .FirstOrDefault(role => role.Id.ToString() == id);

        if (recursos != null)
        {           
            if (recursos.Recursos.Contains(recurso))
            {
                return true;
            }
            else
            {
               return false;
            }
        }
        else
        {
            return false;
        }       
    }

    public class RolConfiguracion
    {
        public int Id { get; set; }
        public List<string> Recursos { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AuthorizeResourceAttribute : AuthorizeAttribute
    {       
        public AuthorizeResourceAttribute(string recurso)
        {
            Policy = $"{recurso}";
        }
    }
}
