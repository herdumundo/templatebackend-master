using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.IRepository
{
    public interface ILocalidadRepository
    {
        Task<IEnumerable<Localidad>> ObtenerLocalidades();
        Task<Localidad> ObtenerLocalidadporCodigo(int id);
        Task<int> IsertarLocalidad(Localidad localidades);
        Task<int> ActualizarLocalidad(Localidad localidades);
    }
}
