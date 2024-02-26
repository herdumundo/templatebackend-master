using Application.Services.Interfaces;
using AutoMapper;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CircunscripcionesController : ControllerBase
    {
        private readonly ICircunscripcionesService _circunscripcionesService;
        private readonly IMapper _mapper;

        public CircunscripcionesController(ICircunscripcionesService circunscripcionesService, IMapper mapper)
        {
            _circunscripcionesService = circunscripcionesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CircunscripcionDto>>> ListadoCircunscripciones()
        {          
            var listado = await _circunscripcionesService.ObtenerCircunscripciones();
            var resultado = _mapper.Map<IEnumerable<CircunscripcionDto>>(listado);
            return Ok(resultado);
        }

    }
}
