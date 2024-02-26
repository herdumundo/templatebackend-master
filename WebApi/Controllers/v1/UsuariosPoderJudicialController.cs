using Application.Services.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Net;
using static WebApi.Middlewares.ErrorHandlingMiddleware;
using static WebApi.ValidationHandlers.RolAuthorizationHandler;

namespace WebApi.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class UsuariosPoderJudicialController : ControllerBase
{
    private readonly IUsuarioBBDDPoderJudicialService _service;
    private readonly IValidator<UsuarioBasedeDatosPoderJudicial> _validator;
    public UsuariosPoderJudicialController(IUsuarioBBDDPoderJudicialService servcice, IValidator<UsuarioBasedeDatosPoderJudicial> validator)
    {
        _service = servcice;
        _validator = validator;
    }


    [HttpGet]    
    [AuthorizeResource("LeerTodos")]
    [SwaggerOperation(
     Summary = "Nos permite obtener informacion de los Usuarios de la BBDD del Poder Judicial",
     Description = "Obtiene información de Usuarios que pertenecen a la BBDD del Poder Judicial.")]
    public async Task<ActionResult<UsuarioBBDDPoderJudicialDTO>> ListadoUsuarios()
    {

        
        var listado = await _service.ObtenerDatosdeUsuariosPoderJudicial();  
        if (listado.Any())
        {
            return Ok(new ApiResponse<List<UsuarioBBDDPoderJudicialDTO>>
            {
                Success = true,
                Data = (List<UsuarioBBDDPoderJudicialDTO>)listado,
                StatusCode = (int)HttpStatusCode.OK
            });
        }
        else
        {
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Data = null,
                StatusCode = (int)HttpStatusCode.OK,
                Errors = new List<string> { "La lista de elementos está vacía" }
            });
        }
    }

       
    [HttpGet("codigoUsuario")]
    [AuthorizeResource("LeerporCodigo")]
    [SwaggerOperation(
        Summary = "Nos permite obtener informacion de los Usuarios de la BBDD del Poder Judicial por codigo ususario",
        Description = "Obtiene información de Usuarios que pertenecen a la BBDD del Poder Judicial por codigo usuario.")]
    public async Task<ActionResult<IEnumerable<UsuarioBBDDPoderJudicialDTO>>> ListadoUsuariosporCodigo(
        [FromQuery][Description("Código único del usuario")] int codigoUsuario)
    {
        if (codigoUsuario == 0)
        {
            throw new ReglasdeNegocioException("El código de usuario no puede estar vacío o tener valor igual a cero");           
        }
        var listado = await _service.ObtenerDatosdeUsuariosPoderJudicialporCodigoUsuario(codigoUsuario);

        if (listado == null)
        {           
            throw new KeyNotFoundException();
        }
        
        return Ok(new ApiResponse<UsuarioBBDDPoderJudicialDTO>
        {
            Success = true,
            Data = (UsuarioBBDDPoderJudicialDTO)listado,
            StatusCode = (int)HttpStatusCode.OK
        });
    }



    [HttpPost] 
    [AuthorizeResource("AgregarRegistro")]
    [SwaggerOperation(
        Summary = "Nos permite Crear Usuarios en la BBDD del Poder Judicial, Solo Los Autorizados pueden gestionarlos",
        Description = "Creamos Usuarios que pertenecen al Poder Judicial.")]
    public async Task<ActionResult<UsuarioBBDDPoderJudicialDTO>> CrearUsuario([FromBody] UsuarioBasedeDatosPoderJudicial usuario)
    {           
        var validationResult = await _validator.ValidateAsync(usuario);
           
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
         
        var usuarioCreado = await _service.AgregarUsuarioaBBDDPoderJudicial(usuario);            
      
        return Ok(new ApiResponse<UsuarioBBDDPoderJudicialDTO>
        {
            Success = true,
            Data = (UsuarioBBDDPoderJudicialDTO)usuarioCreado,
            StatusCode = (int)HttpStatusCode.Created
        });
    }



    [HttpPut("codigoUsuario")]    
    [AuthorizeResource("ActualizarRegistro")]
    [SwaggerOperation(
            Summary = "Actualiza usuario en la BBDD del Poder Judicial,Solo Los Autorizados pueden gestionarlos",
            Description = "Actualiza la información del usuario perteneciente a la BBDD del Poder Judicial.")]
    public async Task<ActionResult> ActualizarUsuario([FromQuery][Description("Código único del usuario")] int codigoUsuario, 
        [FromBody] UsuarioBasedeDatosPoderJudicial usuario)
    {
        if (codigoUsuario != usuario.Codigo_Usuario)
        {
            throw new ReglasdeNegocioException("El código de usuario en la ruta no coincide con el código de usuario en los datos proporcionados.");
        }

        if (codigoUsuario == 0)
        {
            throw new ReglasdeNegocioException("El código de usuario no puede estar vacío o tener valor igual a cero");
        }

        var validationResult = await _validator.ValidateAsync(usuario);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var filasActualizadas = await _service.ActualizarUsuarioaBBDDPoderJudicial(codigoUsuario, usuario);

        if (filasActualizadas != null)
        {
            return Ok(new ApiResponse<int>
            {
                Success = true,
                Data = (int)filasActualizadas,
                StatusCode = (int)HttpStatusCode.NoContent
            });
        }           

        throw new KeyNotFoundException();
    }
}

