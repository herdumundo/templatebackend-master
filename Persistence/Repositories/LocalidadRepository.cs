using Application.Exceptions;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Persistence.Repositories.IRepository;

namespace Persistence.Repositories
{
    public class LocalidadRepository : ILocalidadRepository
    {
        private readonly DbConnections _conexion;
        private readonly ILogger<LocalidadRepository> _logger;

        public LocalidadRepository(DbConnections conexion, ILogger<LocalidadRepository> logger = null)
        {
            _conexion = conexion;
            _logger = logger;
        }

        public async Task<Localidad> ObtenerLocalidadporCodigo(int id)
        {
            _logger.LogInformation("Proceso de Inicio para ontener las Localidades");
            string query = "SELECT * FROM Localidades L WHERE L.codigo_circunscripcion = @Id";
            try
            {
                using (var connection = this._conexion.CreateSqlConnectionCSJ())
                {
                    var resultado = await connection.QueryFirstOrDefaultAsync<Localidad>(query,new { Id = id });
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

        public async Task<IEnumerable<Localidad>> ObtenerLocalidades()
        {
            string query = "SELECT * FROM Localidades";
            try
            {
                using (var connection = this._conexion.CreateSqlConnectionCSJ())
                {
                    var lista = await connection.QueryAsync<Localidad>(query);
                    return lista.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> IsertarLocalidad(Localidad localidades)
        {
            string query = "INSERT INTO Localidades" +
                           "(codigo_circunscripcion,nombre) VALUES " +
                           "(@codigo_circunscripcion,@nombre)";
            try
            {
                using (var connection = this._conexion.CreateSqlConnectionCSJ())
                {
                    var resultado = await connection.ExecuteAsync(query,localidades);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> ActualizarLocalidad(Localidad localidades)
        {
            string query = "UPDATE Localidades " +
                           "SET(codigo_circunscripcion = codigo_circunscripcion, nombre = @nombre) " +
                           "WHERE id = @id";
            try
            {
                using (var connection = this._conexion.CreateSqlConnectionCSJ())
                {
                    var resultado = await connection.ExecuteAsync(query, localidades);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
             
    }
}
