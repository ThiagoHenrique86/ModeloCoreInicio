using Prodesp.Core.Backend.Domain.Interfaces.Domain.Entities;
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Validations;
using Prodesp.Core.Backend.Domain.Validations;

namespace Prodesp.Domain.Shared.Entities;

public class Login : IEntityModel
{
    public string IdLogin { get; set; }
    public string IdUsuario { get; set; }
    public string CodigoLogin { get; set; }
    public string CodigoSenha { get; set; }
    public DateTime DataValidade { get; set; }
    public int FlagBloqueado { get; set; }
    public int FlagAtivo { get; set; }
    public virtual int NumeroTentativaLogin { get; set; }
    public DateTime DataInclusao { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string? IdUsuarioInclusao { get; set; }
    public string? IdUsuarioAlteracao { get; set; }
    public virtual Usuario Usuario { get; set; }
    public bool PrimeiroAcesso { get; set; }
    public Sessao SessaoAtiva { get; set; }

    public virtual ValidationResult ValidationResult
    {
        get; set;
    }
    public virtual bool IsValid
    {
        get
        {
            var validation = new LoginValidation();
            this.ValidationResult = validation.Validate(this);
            return this.ValidationResult.IsValid;
        }
    }
}


public class LoginValidation : Validation<Login>
{
    public LoginValidation()
    {
        base.AddRule(new ValidationRule<Login>(new LoginSpec(), "Usuário é obrigatório!"));
    }
}

public class LoginSpec : ISpecification<Login>
{
    public virtual bool IsSatisfiedBy(Login entity)
    {
        return (entity != null);
    }
}

