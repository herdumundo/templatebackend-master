using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IUsuarioBBDDPoderJudicialService
{
    Task<IEnumerable<UsuarioBBDDPoderJudicialDTO>> ObtenerDatosdeUsuariosPoderJudicial();
    Task<UsuarioBBDDPoderJudicialDTO> ObtenerDatosdeUsuariosPoderJudicialporCodigoUsuario(int codigo);
    Task<UsuarioBBDDPoderJudicialDTO> AgregarUsuarioaBBDDPoderJudicial(UsuarioBasedeDatosPoderJudicial usuario);
    Task<int> ActualizarUsuarioaBBDDPoderJudicial(int codigo, UsuarioBasedeDatosPoderJudicial usuario);
}

