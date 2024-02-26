using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ICircunscripcionesService
    {
        Task<IEnumerable<Circunscripcion>> ObtenerCircunscripciones();
        Task<Circunscripcion> ObtenerCircunscripcionesporNumero(int id);
    }
}
