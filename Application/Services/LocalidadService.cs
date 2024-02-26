using Application.Services.Interfaces;
using Application.Services.Interfaces.IRepository;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Persistence.Repositories.IRepository;

namespace Application.Services
{
    public class LocalidadService : ILocalidadesService
    {
        private readonly ILocalidadRepository _repository;
        private readonly IMapper _mapper;
        public LocalidadService(ILocalidadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> ActualizarLocalidad(Localidad localidad)
        {
            return await _repository.ActualizarLocalidad(localidad);
        }

        public async Task<int> InsertarLocalidad(LocalidadDto localidad)
        {
            var mappingLocalidad = _mapper.Map<Localidad>(localidad);
            return await  _repository.IsertarLocalidad(mappingLocalidad);
        }

        public async Task<IEnumerable<LocalidadDto>> ObtenerLocalidades()
        {
            var resultado = await _repository.ObtenerLocalidades();
            return _mapper.Map<IEnumerable<LocalidadDto>>(resultado);
        }

        public async Task<LocalidadDto> ObtenerLocalidadporCodigo(int id)
        {
            var resultado = await _repository.ObtenerLocalidadporCodigo(id);
            return _mapper.Map<LocalidadDto>(resultado);
        }
    }
}
