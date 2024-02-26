using Domain.Entities;

namespace Application.Services.Interfaces.IRepository
{
    public interface ICircunscripcionRepository
    {
        Task<IEnumerable<Circunscripcion>> ObtenerTodasLasCircunscripciones();
        Task<Circunscripcion> ObtenerCircunscripcionesporNro(int id);
    }
}
