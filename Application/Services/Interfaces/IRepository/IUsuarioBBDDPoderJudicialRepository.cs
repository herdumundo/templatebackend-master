using Domain.Entities;

namespace Application.Services.Interfaces.IRepository;

public interface IUsuarioBBDDPoderJudicialRepository
{
    Task<IEnumerable<UsuarioBBDDPoderJudicialDTO>> ObtenerDatosdeUsuariosPoderJudicial();
    Task<UsuarioBBDDPoderJudicialDTO> ObtenerDatosdeUsuariosPoderJudicialporCodigoUsuario(int codigo);
    Task<UsuarioBasedeDatosPoderJudicial> AgregarUsuarioaBBDDPoderJudicial(UsuarioBasedeDatosPoderJudicial usuario);
    Task<int> ActualizarUsuarioaBBDDPoderJudicial(int codigo,UsuarioBasedeDatosPoderJudicial usuario);
}

