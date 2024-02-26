using Application.Exceptions;
using Google.Protobuf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using WebApi.Protos;

namespace WebApi.ValidationHandlers;

public class TokenSesionAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "TokenSesion";
    public string Scheme => DefaultScheme;
    public string AuthenticationType = DefaultScheme;
}
public class TokenSesionAuthenticationHandler : AuthenticationHandler<TokenSesionAuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TokenSesionAuthenticationHandler> _logger;

    private const string AuthHeaderName = "authorization";
    private const string RolHeaderName = "usuario-rol";

    public TokenSesionAuthenticationHandler(IOptionsMonitor<TokenSesionAuthenticationSchemeOptions> options,
        ILoggerFactory loggerFactory, UrlEncoder encoder, ISystemClock clock, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor) : base(options, loggerFactory, encoder, clock)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _logger = loggerFactory.CreateLogger<TokenSesionAuthenticationHandler>(); ;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            _logger.LogInformation("Inicio de proceso de Gestion de la logica de Autenticacion Token Sesion");
            if (!Request.Headers.ContainsKey("usuario-sesion") || !Request.Headers.ContainsKey(RolHeaderName))
            {
                _logger.LogWarning("Ocurrio un Error en la Autenticacion Token Sesion,Cabecera no puede estar vacia");
                throw new ErrorAutorizacionUsuarioException("Usuario no Autorizado para consumir el recurso seleccionado");
            }

            var rolSeleccionado = int.Parse(Request.Headers["Usuario-Rol"].ToString());

            var datosUsuario = new RespuestaDatosUsuario();
            var value = _configuration.GetSection("Mark").Value;

            if (string.IsNullOrEmpty(value))
            {
                var sesionBase64 = Request.Headers["usuario-sesion"]!;
                datosUsuario = RespuestaDatosUsuario.Parser.ParseFrom(Base64UrlTextEncoder.Decode(sesionBase64));
            }
            else
            {
               //Esta Marca usamos  cuando realizamos Test Rapidos sin usar Bytes
                RespuestaDatosUsuario respuestaDatosUsuario = new RespuestaDatosUsuario();
                datosUsuario = JsonParser.Default.Parse<RespuestaDatosUsuario>(estructuraJson());
            }

            if (datosUsuario != null)
            {              
                var appRoles = datosUsuario.Roles.FirstOrDefault(rol =>
                {
                    return rol.AppId == int.Parse(_configuration.GetSection("Settings:AppId").Value);
                });

                if (appRoles != null)
                {
                    var rol = appRoles.Roles.FirstOrDefault(rol => rol.RolId == rolSeleccionado);

                    if (rol != null)
                    {
                        var claims = new[]
             {
                        new Claim(ClaimTypes.Name,datosUsuario.NombreUsuario),
                        new Claim("selectedRolId",rolSeleccionado.ToString()),
                        new Claim("NumeroDocumento", datosUsuario.NumeroDocumento)
                };

                        var claimsIdentity = new ClaimsIdentity(claims, nameof(TokenSesionAuthenticationHandler));
                        var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);

                        return AuthenticateResult.Success(ticket);
                    }
                    else
                    {
                        return AuthenticateResult.Fail("Usuario no Autorizado para consumir el recurso seleccionado");
                    }
                }
                else
                {
                    return AuthenticateResult.Fail("Roles de Usuario no encontrado");
                }             
            }            
        }
        catch
        {            
            throw ;
        }

        _logger.LogInformation("Fin de proceso de Gestion de la logica de Generar JWT");
        return AuthenticateResult.Fail("Usuario no Autorizado para consumir el recurso seleccionado");
    }    

    private string estructuraJson()
    {
        string json = @"{
          ""nombre_usuario"": ""Informatico, Administrador"",
          ""numero_documento"": ""20000000"",
          ""codigo_usuario"": 37593,
          ""roles"": [
            {
              ""app_id"": 26,
              ""roles"": [
                {
                  ""rol_id"": 16,
                  ""descripcion_rol"": ""Abogados""
                }
              ]
            },
            {
              ""app_id"": 49,
              ""roles"": [
                {
                  ""rol_id"": 16,
                  ""descripcion_rol"": ""Abogados""
                },
                {
                  ""rol_id"": 25,
                  ""descripcion_rol"": ""Fiscal Penal""
                }
              ]
            },
            {
              ""app_id"": 50,
              ""roles"": [
                {
                  ""rol_id"": 16,
                  ""descripcion_rol"": ""Abogados""
                }
              ]
            },
            {
              ""app_id"": 3062,
              ""roles"": [
                {
                  ""rol_id"": 16,
                  ""descripcion_rol"": ""Abogados""
                },
                {
                  ""rol_id"": 25,
                  ""descripcion_rol"": ""Fiscal Penal""
                },
                {
                  ""rol_id"": 4159,
                  ""descripcion_rol"": ""Asistente""
                }
              ]
            },
            {
              ""app_id"": 3065,
              ""roles"": [
                {
                  ""rol_id"": 16,
                  ""descripcion_rol"": ""Abogados""
                },
                {
                  ""rol_id"": 25,
                  ""descripcion_rol"": ""Fiscal Penal""
                }
              ]
            }
          ],        
          ""sesion_id"": """"        
        }";

        return json;
    }
}

