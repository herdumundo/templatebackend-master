using Application.Services.Interfaces;
using Application.Services.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class CircunscripcionService : ICircunscripcionesService
    {
        private readonly ICircunscripcionRepository _repository;
        private readonly IMapper _mapper;
        public CircunscripcionService(ICircunscripcionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<Circunscripcion> ObtenerCircunscripcionesporNumero(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Circunscripcion>> ObtenerCircunscripciones()
        {
           return await _repository.ObtenerTodasLasCircunscripciones();
        }
    }
}
