using AutoMapper;
using Prodesp.Application.CrossCutting.TO.Response.Medex.Paciente;
using Prodesp.Application.CrossCutting.TO.Response.Usuario;
using Prodesp.Core.Backend.Domain.Models;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Infra.EF.AutoMapper;

public class ResponseToDomainMappingProfile : Profile
{
    public ResponseToDomainMappingProfile()
    {
        CreateMap<UsuarioResponseData, Usuario>();
        CreateMap< PageResult<UsuarioResponseData>,PageResult<Usuario>>();

        CreateMap<PacienteResponseData, Paciente>();
        CreateMap<PageResult<PacienteResponseData>, PageResult<Paciente>>();


    }
}

