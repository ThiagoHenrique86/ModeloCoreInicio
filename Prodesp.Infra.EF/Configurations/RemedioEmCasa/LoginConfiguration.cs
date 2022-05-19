using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Prodesp.Infra.EF.Configurations.RemedioEmCasa
{
    public class LoginConfiguration : IEntityTypeConfiguration<Domain.Shared.Entities.Login>
    {
        
        public void Configure(EntityTypeBuilder<Domain.Shared.Entities.Login> builder)
        {
            builder.HasKey(x => x.IdLogin);
            builder.Property(x => x.IdLogin).HasColumnName("ID_LOGIN").ValueGeneratedNever();

            builder.Property(x => x.IdUsuario).HasColumnName("ID_USUARIO");
            builder.Property(x => x.CodigoLogin).HasColumnName("CD_LOGIN");
            builder.Property(x => x.CodigoSenha).HasColumnName("CD_SENHA");
            builder.Property(x => x.DataValidade).HasColumnName("DT_VALIDADE");
            builder.Property(x => x.FlagBloqueado).HasColumnName("FL_BLOQUEADO");
            builder.Property(x => x.FlagAtivo).HasColumnName("FL_ATIVO");
            builder.Property(x => x.NumeroTentativaLogin).HasColumnName("NR_TENTATIVA_LOGIN");
            builder.Property(x => x.DataInclusao).HasColumnName("DT_INCLUSAO");
            builder.Property(x => x.DataAlteracao).HasColumnName("DT_ALTERACAO");
            builder.Property(x => x.IdUsuarioInclusao).HasColumnName("ID_USUARIO_INCLUSAO");
            builder.Property(x => x.IdUsuarioAlteracao).HasColumnName("ID_USUARIO_ALTERACAO");

            
            builder.Ignore(x => x.PrimeiroAcesso);
            builder.Ignore(x => x.SessaoAtiva);
            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.IsValid);
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.IdUsuario);
          //  builder.ToTable("PV_LOGIN", ConfigurationHelper.Schema);

        }

    }



}
