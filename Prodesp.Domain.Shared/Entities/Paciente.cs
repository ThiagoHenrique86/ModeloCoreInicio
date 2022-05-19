
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Entities;
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Validations;
using Prodesp.Core.Backend.Domain.Validations;

namespace Prodesp.Domain.Shared.Entities;

public class Paciente : IEntityModel
{
    public string CodigoPaciente { get; set; }
    public string NomePaciente { get; set; }
    public string NomePacienteFonetico { get; set; }
    public DateTime? DataInclusao { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string NomeMae { get; set; }
    public string CodigoSexo { get; set; }
    public decimal CNS { get; set; }
    public string CPF { get; set; }
    public string RG { get; set; }


    public virtual ValidationResult ValidationResult
    {
        get; set;
    }
    public virtual bool IsValid
    {
        get
        {
            var validation = new PacienteValidation();
            this.ValidationResult = validation.Validate(this);
            return this.ValidationResult.IsValid;
        }
    }
}

public class PacienteValidation : Validation<Paciente>
{
    public PacienteValidation()
    {
        base.AddRule(new ValidationRule<Paciente>(new PacienteSpec(), "Paciente é obrigatório!"));
    }
}

public class PacienteSpec : ISpecification<Paciente>
{
    public virtual bool IsSatisfiedBy(Paciente entity)
    {
        return (entity != null);
    }
}