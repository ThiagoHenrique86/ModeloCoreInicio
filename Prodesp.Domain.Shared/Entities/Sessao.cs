using Prodesp.Core.Backend.Domain.Interfaces.Domain.Entities;
using Prodesp.Core.Backend.Domain.Interfaces.Domain.Validations;
using Prodesp.Core.Backend.Domain.Validations;

namespace Prodesp.Domain.Shared.Entities;

    public class Sessao : IEntityModel
    {
        public string IdAccessToken { get; set; }
        public string CodigoAccessToken { get; set; }
        public int QtdDuracaoAccessToken { get; set; }
        public DateTime DataValidadeAccessToken { get; set; }
        public string IdUsuario { get; set; }
        public string IdLogin { get; set; }
        public string NumeroIp { get; set; }
        public string NomeHost { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string FlagRefreshAccessToken { get; set; }
    
        public virtual Login Login { get; set; }
       

        public virtual ValidationResult ValidationResult
        {
            get; set;
        }
        public virtual bool IsValid
        {
            get
            {
                var validation = new SessaoValidation();
                this.ValidationResult = validation.Validate(this);
                return this.ValidationResult.IsValid;
            }
        }
    }


public class SessaoValidation : Validation<Sessao>
{
    public SessaoValidation()
    {
        base.AddRule(new ValidationRule<Sessao>(new SessaoSpec(), "Sessao é obrigatório!"));
    }
}
public class SessaoSpec : ISpecification<Sessao>
{
    public virtual bool IsSatisfiedBy(Sessao entity)
    {
        return (entity != null);
    }
}