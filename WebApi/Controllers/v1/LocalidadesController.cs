using Application.Services.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.v1
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocalidadesController : ControllerBase
    {
        private readonly ILocalidadesService _service;
        private readonly IMapper _mapper;

        public LocalidadesController(ILocalidadesService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Nos permite Obtener informacion de las localidades ", 
            Description = "Obtiene información basada en un ID.")]
        public async Task<ActionResult<IEnumerable<LocalidadDto>>> ListadodeLocalidades()
        {
            var listado = await _service.ObtenerLocalidades();
            var resultado = _mapper.Map<IEnumerable<LocalidadDto>>(listado);
            return Ok(resultado);
        }

    }
}
