using Application.Services.Interfaces.IRepository;
using Dapper;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Persistence.Repositories;

public class UsuarioBBDDPoderJudicialRepository : IUsuarioBBDDPoderJudicialRepository
{
    private readonly DbConnections _conexion;
    private readonly ILogger<UsuarioBBDDPoderJudicialRepository> _logger;

    public UsuarioBBDDPoderJudicialRepository(DbConnections conexion, ILogger<UsuarioBBDDPoderJudicialRepository> logger)
    {
        _conexion = conexion;
        _logger = logger;
    }

    public async Task<int> ActualizarUsuarioaBBDDPoderJudicial(int codigo, UsuarioBasedeDatosPoderJudicial usuario)
    {
        _logger.LogInformation("Inicio de Proceso de Actualizar valores de Usuarios del Poder Juducial" +
            " Datos enviados {@usuario}", usuario);


        string query = @"
                        UPDATE usuarios_poder_judicialONLYTEST
                            SET 
                            codigo_tipo_usuario = @codigo_tipo_usuario,
                            password_usuario = @password_usuario,
                            usuario_base_datos = @usuario_base_datos,
                            password_base_datos = @password_base_datos,
                            username = @username,codigo_legajo = @codigo_legajo,
                            codigo_despacho = @codigo_despacho,
                            activo = @activo
                        WHERE codigo_usuario = @codigo_usuario";
        try
        {
            using (var connection = this._conexion.CreateSqlConnectionCSJ())
            {
                var parametros = new
                {
                    codigo_tipo_usuario = usuario.Codigo_Tipo_Usuario,
                    password_usuario = usuario.Password_Usuario,
                    usuario_base_datos = usuario.Usuario_Base_Datos,
                    password_base_datos = usuario.Password_Base_Datos,
                    username = usuario.Username,
                    codigo_legajo = usuario.Codigo_Legajo,
                    codigo_despacho = usuario.Codigo_Despacho,
                    activo = usuario.Activo,
                    codigo_usuario = codigo
                };

                var resultado = await connection.ExecuteAsync(query, parametros);

                _logger.LogInformation("Fin de Proceso de Actualizar valores de Usuarios del Poder Juducial" +
                     " Datos enviados {@usuario}", usuario);

                return resultado;
            }
        }
        catch (Exception ex)
        {           
            throw new ObtenerListadoUsuariosRepositoryException("Ocurrio un error al actualizar los datos de Usuarios del Poder Judicial");
        }

    }

    public async Task<UsuarioBasedeDatosPoderJudicial> AgregarUsuarioaBBDDPoderJudicial(UsuarioBasedeDatosPoderJudicial usuario)
    {
        _logger.LogInformation("Inicio de Proceso de Insertar valores de Usuarios del Poder Juducial" +
            " Datos enviados {@usuario}", usuario);


        string query = "INSERT INTO usuarios_poder_judicialONLYTEST " +
                          "(codigo_persona,codigo_tipo_usuario,password_usuario,usuario_base_datos,password_base_datos," +
                          "username,codigo_legajo,codigo_despacho,activo) OUTPUT INSERTED.* VALUES " +
                          "(@codigo_persona,@codigo_tipo_usuario,@password_usuario,@usuario_base_datos,@password_base_datos," +
                          "@username,@codigo_legajo,@codigo_despacho,@activo)";
        try
        {
            using (var connection = this._conexion.CreateSqlConnectionCSJ())
            {
                var resultado = await connection.QuerySingleAsync<UsuarioBasedeDatosPoderJudicial>(query, usuario);

                _logger.LogInformation("Fin de Proceso de Insertar valores de Usuarios del Poder Juducial" +
                     " Datos enviados {@usuario}", usuario);

                return resultado;
            }
        }
        catch (Exception ex)
        {           
            throw new ObtenerListadoUsuariosRepositoryException("Ocurrio un error al actualizar los datos de Usuarios del Poder Judicial");
        }
    }

    public async Task<IEnumerable<UsuarioBBDDPoderJudicialDTO>> ObtenerDatosdeUsuariosPoderJudicial()
    {
        _logger.LogInformation("Inicio de Proceso de Obtener valores de Usuarios del Poder Juducial");
        
        string query = @"select top 10 
                                        u.codigo_usuario,
                                        p.primer_nombre,
                                        p.primer_apellido,
                                        u.Username,
                                        u.password_usuario,
                                        u.usuario_base_datos,
                                        u.Password_Base_Datos,
                                        u.activo
                         from usuarios_poder_judicialONLYTEST u inner join personaONLYTEST p
                         on u.codigo_usuario = p.codigo_persona";
        try
        {
            using (var connection = this._conexion.CreateSqlConnectionCSJ())
            {
                var lista = await connection.QueryAsync<UsuarioBBDDPoderJudicialDTO>(query);

                _logger.LogInformation("Fin de Proceso de Obtener valores de Usuarios del Poder Juducial");
                return lista.ToList();
            }
        }
        catch (Exception ex)
        {
            throw new ObtenerListadoUsuariosRepositoryException("Ocurrio un error al Obtener los datos de Usuarios del Poder Judicial");
        }
    }

    public async Task<UsuarioBBDDPoderJudicialDTO> ObtenerDatosdeUsuariosPoderJudicialporCodigoUsuario(int codigo)
    {
        _logger.LogInformation("Inicio de Proceso de Obtener valores de Usuarios del Poder Juducial por codigo");

        string query = @"select top 10 
                                        u.codigo_usuario,
                                        p.primer_nombre,
                                        p.primer_apellido,
                                        u.Username,
                                        u.password_usuario,
                                        u.usuario_base_datos,
                                        u.Password_Base_Datos,
                                        u.activo
                         from usuarios_poder_judicialONLYTEST u inner join personaONLYTEST p
                         on u.codigo_persona = p.codigo_persona
                         WHERE u.codigo_usuario = @Codigo";
        try
        {
            using (var connection = this._conexion.CreateSqlConnectionCSJ())
            {
                var resultado = await connection.QueryFirstOrDefaultAsync<UsuarioBBDDPoderJudicialDTO>(query, new { Codigo = codigo });
                if (resultado == null)
                {
                    _logger.LogInformation("Fin de Proceso de Obtener valores de Usuarios del Poder Juducial por codigo");
                    return null;
                }
                _logger.LogInformation("Fin de Proceso de Obtener valores de Usuarios del Poder Juducial por codigo");
                return resultado;              
            }
        }
        catch (Exception ex)
        {           
            throw new ObtenerListadoUsuariosRepositoryException("Ocurrio un error al Obtener los datos de Usuarios del Poder Judicial");
        }       
    }
}

