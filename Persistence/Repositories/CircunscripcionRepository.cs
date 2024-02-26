using Application.Exceptions;
using Application.Services.Interfaces.IRepository;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Persistence.Repositories
{
    public class CircunscripcionRepository : ICircunscripcionRepository
    {
        private readonly DbConnections _conexion;

        public CircunscripcionRepository(DbConnections conexion, IConfiguration configuration)
        {
            _conexion = conexion;
        }

        public async Task<Circunscripcion> ObtenerCircunscripcionesporNro(int id)
        {
            string query = "select * from circunscripciones where numero_circunscripcion = @Id";
            try
            {
                using (var connection = this._conexion.CreateSqlConnectionLocalDB())
                {
                    var resultado = await connection.QueryFirstOrDefaultAsync<Circunscripcion>(query,new { Id = id });
                    if (resultado != null)
                    {
                        return null;
                    }
                    return resultado;
                }                 
            }
            catch
            (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Circunscripcion>> ObtenerTodasLasCircunscripciones()
        {
            string query = "select * from circunscripciones";
            try
            {
                using (var connection = this._conexion.CreateSqlConnectionLocalDB())
                {
                    var lista = await connection.QueryAsync<Circunscripcion>(query);
                    return lista.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
