using AutoMapper;
using Prodesp.Application.CrossCutting.TO.Response.Medex.Paciente;
using Prodesp.Application.CrossCutting.TO.Response.Usuario;
using Prodesp.Core.Backend.Domain.Models;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Infra.EF.AutoMapper;

public class DomainToResponseMappingProfile : Profile
{
    public DomainToResponseMappingProfile()
    {
        CreateMap<Usuario, UsuarioResponseData>();
        CreateMap<PageResult<Usuario>, PageResult<UsuarioResponseData>>();

        CreateMap<Paciente, PacienteResponseData>();
        CreateMap<PageResult<Paciente>, PageResult<PacienteResponseData>>();
    }
}

