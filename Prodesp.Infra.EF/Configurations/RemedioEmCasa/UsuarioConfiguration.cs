using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Prodesp.Infra.EF.Configurations.RemedioEmCasa
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Domain.Shared.Entities.Usuario>
    {
        
        public void Configure(EntityTypeBuilder<Domain.Shared.Entities.Usuario> builder)
        {

            builder.HasKey(x => x.IdUsuario);

            builder.Property(x => x.IdUsuario).HasColumnName("ID_USUARIO").ValueGeneratedNever();
            //builder.Property(x => x.IdUsuarioInclusao).HasColumnName("ID_USUARIO_INCLUSAO");
            //builder.Property(x => x.IdUsuarioAlteracao).HasColumnName("ID_USUARIO_ALTERACAO");
            builder.Property(x => x.NomeUsuario).HasColumnName("NM_USUARIO");
            builder.Property(x => x.Ativo).HasColumnName("FL_ATIVO");
            //builder.Property(x => x.CodigoSaltKey).HasColumnName("CD_SALTKEY");
            builder.Property(x => x.DataAlteracao).HasColumnName("DT_ALTERACAO");
            builder.Property(x => x.DataInclusao).HasColumnName("DT_INCLUSAO");

            builder.Ignore(x => x.IdUsuarioInclusao);
            builder.Ignore(x => x.IdUsuarioAlteracao);
            builder.Ignore(x => x.CodigoSaltKey);

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.IsValid);
            builder.ToTable("RC_USUARIO", ConfigurationHelper.SchemaRemedioEmCasa);
        }

    }



}
