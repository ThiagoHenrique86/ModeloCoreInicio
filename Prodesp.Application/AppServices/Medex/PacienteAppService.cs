

using AutoMapper;
using Prodesp.Application.CrossCutting.TO.Response.Medex.Paciente;
using Prodesp.Application.Validation.Medex;
using Prodesp.Core.Backend.Application.Implementations;
using Prodesp.Core.Backend.Application.Interfaces;
using Prodesp.Core.Backend.Domain.Validations;
using Prodesp.Domain.Services.Implementations.Medex;
using Prodesp.Domain.Shared.Entities;

namespace Prodesp.Application.AppServices.Medex;

public interface IPacienteAppService : IAppService<Domain.Shared.Entities.Paciente>
{
    PacienteValidationResult GetPacientePorCNS(string cns);
    PacienteValidationResult GetPacientePorId(string id);
    
}

public class PacienteAppService : AppService<Domain.Shared.Entities.Paciente, IPacienteService>, IPacienteAppService
{
    public PacienteAppService(IPacienteService service, IMapper mapper) : base(service)
    {
        _Mapper = mapper;
    }

    public IMapper _Mapper { get; }

    public PacienteValidationResult GetPacientePorCNS(string cns)
    {
        
        PacienteValidationResult result = new PacienteValidationResult();
        try
        {
            var pacienteData = _service.GetPacientePorCNS(cns);

            var pacienteResponseData = new PacienteResponseData();
            this._Mapper.Map(pacienteData, pacienteResponseData);
            result.Data = pacienteResponseData;
            
        }
        catch (ApplicationException ex)
        {
            result.Add(ex.Message);
        }
        catch (Exception ex)
        {
            result.Add("Não foi possível consultar este CNS");
        }
        return result;
    }

    public PacienteValidationResult GetPacientePorId(string id)
    {
        PacienteValidationResult result = new PacienteValidationResult();
        try
        {
            var pacienteData = _service.GetPacientePorId(id);
            
            var pacienteResponseData = new PacienteResponseData();
            this._Mapper.Map(pacienteData, pacienteResponseData);
            result.Data = pacienteResponseData;
            
            
        }
        catch (ApplicationException ex)
        {
            result.Add(ex.Message);
        }
        catch (Exception ex)
        {
            result.Add("Não foi possível consultar este paciente");
        }
        return result;
    }

    public override ValidationResult ValidateDelete(Paciente entity)
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateInsert(Paciente entity)
    {
        throw new NotImplementedException();
    }

    public override ValidationResult ValidateUpdate(Paciente entity)
    {
        throw new NotImplementedException();
    }
}