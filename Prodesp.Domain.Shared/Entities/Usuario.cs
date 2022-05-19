
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Entities;
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Validations;
using Prodesp.Core.Backend.Domain.Validations;

namespace Prodesp.Domain.Shared.Entities;

public class Usuario : IEntityModel
{
    public Usuario()
    {
        this.ValidationResult = new ValidationResult();
    }

    public string IdUsuario { get; set; }
    public string NomeUsuario { get; set; }
    public string CodigoSaltKey { get; set; }
    public int Ativo { get; set; }
    public DateTime DataInclusao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string? IdUsuarioInclusao { get; set; }
    public string? IdUsuarioAlteracao { get; set; }

    public virtual ValidationResult ValidationResult
    {
        get; set;
    }
    public virtual bool IsValid
    {
        get
        {
            var validation = new UsuarioValidation();
            this.ValidationResult = validation.Validate(this);
            return this.ValidationResult.IsValid;
        }
    }

}

public class UsuarioValidation : Validation<Usuario>
{
    public UsuarioValidation()
    {
        base.AddRule(new ValidationRule<Usuario>(new UsuarioSpec(), "Usuário é obrigatório!"));
    }
}

public class UsuarioSpec : ISpecification<Usuario>
{
    public virtual bool IsSatisfiedBy(Usuario entity)
    {
        return (entity != null);
    }
}