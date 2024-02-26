using Domain.DTOs;
using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface ILocalidadesService
    {
        Task<IEnumerable<LocalidadDto>> ObtenerLocalidades();
        Task<LocalidadDto> ObtenerLocalidadporCodigo(int id);
        Task<int> InsertarLocalidad(LocalidadDto localidad);
        Task<int> ActualizarLocalidad(Localidad localidad);
    }
}
