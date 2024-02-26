using Application.Services.Interfaces;
using Application.Services.Interfaces.IRepository;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Services;

public class UsuarioBBDDPoderJudicialService : IUsuarioBBDDPoderJudicialService
{
    private readonly IUsuarioBBDDPoderJudicialRepository _repository;
    private readonly IMapper _mapper;

    public UsuarioBBDDPoderJudicialService(IUsuarioBBDDPoderJudicialRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> ActualizarUsuarioaBBDDPoderJudicial(int codigo, UsuarioBasedeDatosPoderJudicial usuario)
    {
        return await _repository.ActualizarUsuarioaBBDDPoderJudicial(codigo,usuario);
    }

    public async Task<UsuarioBBDDPoderJudicialDTO> AgregarUsuarioaBBDDPoderJudicial(UsuarioBasedeDatosPoderJudicial usuario)
    {
        var resultado = await _repository.AgregarUsuarioaBBDDPoderJudicial(usuario);
        return await _repository.ObtenerDatosdeUsuariosPoderJudicialporCodigoUsuario(resultado.Codigo_Usuario);        
    }

    public async Task<IEnumerable<UsuarioBBDDPoderJudicialDTO>> ObtenerDatosdeUsuariosPoderJudicial()
    {
        return await _repository.ObtenerDatosdeUsuariosPoderJudicial();       
    }

    public async Task<UsuarioBBDDPoderJudicialDTO> ObtenerDatosdeUsuariosPoderJudicialporCodigoUsuario(int codigo)
    {
        return await _repository.ObtenerDatosdeUsuariosPoderJudicialporCodigoUsuario(codigo);
    }
}

