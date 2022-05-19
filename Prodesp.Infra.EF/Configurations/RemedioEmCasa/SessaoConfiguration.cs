using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prodesp.Infra.EF.Configurations.RemedioEmCasa
{
    public class SessaoConfiguration : IEntityTypeConfiguration<Domain.Shared.Entities.Sessao>
    {
        public void Configure(EntityTypeBuilder<Domain.Shared.Entities.Sessao> builder)
        {



            builder.HasKey(x => x.IdAccessToken);
            builder.Property(x => x.IdAccessToken).HasColumnName("ID_ACCESS_TOKEN").ValueGeneratedNever();

            builder.Property(x => x.CodigoAccessToken).HasColumnName("CD_ACCESS_TOKEN");
            builder.Property(x => x.QtdDuracaoAccessToken).HasColumnName("QT_DURACAO_ACCESS_TOKEN");
            builder.Property(x => x.DataValidadeAccessToken).HasColumnName("DT_VALIDADE_ACCESS_TOKEN");
            builder.Property(x => x.IdUsuario).HasColumnName("ID_USUARIO");
            builder.Property(x => x.IdLogin).HasColumnName("ID_LOGIN").IsRequired();
            builder.Property(x => x.NumeroIp).HasColumnName("NR_IP");
            builder.Property(x => x.NomeHost).HasColumnName("NM_HOST");
            builder.Property(x => x.DataInclusao).HasColumnName("DT_INCLUSAO");
            builder.Property(x => x.DataAlteracao).HasColumnName("DT_ALTERACAO");
            builder.Property(x => x.FlagRefreshAccessToken).HasColumnName("FL_REFRESH_ACCESS_TOKEN");

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.IsValid);

            builder.HasOne(x => x.Login).WithMany().HasForeignKey(x => x.IdLogin);
            //builder.HasMany();
           // builder.ToTable("PV_SESSAO", ConfigurationHelper.Schema);
        }

    }



}
