using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodesp.Infra.EF.Configurations.Medex;

public class PacienteConfiguration: IEntityTypeConfiguration<Prodesp.Domain.Shared.Entities.Paciente>
{

    public void Configure(EntityTypeBuilder<Prodesp.Domain.Shared.Entities.Paciente> builder)
    {
        builder.HasKey(x => x.CodigoPaciente);
        
        builder.Property(x => x.CodigoPaciente).HasColumnName("CD_PACIENTE").ValueGeneratedNever().IsUnicode(false);
        builder.Property(x => x.NomePaciente).HasColumnName("NM_PACIENTE");
        builder.Property(x => x.NomePacienteFonetico).HasColumnName("FN_NM_PACIENTE");
        builder.Property(x => x.DataInclusao).HasColumnName("DT_INCLPAC");
        builder.Property(x => x.DataNascimento).HasColumnName("DT_NASCPAC");
        builder.Property(x => x.NomeMae).HasColumnName("NM_MAEPAC");
        builder.Property(x => x.CodigoSexo).HasColumnName("CD_SEXOPAC");
        builder.Property(x => x.CNS).HasColumnName("NR_CNSPAC");
        builder.Property(x => x.CPF).HasColumnName("NR_CPFPAC");
        builder.Property(x => x.RG).HasColumnName("NR_RGPAC");


        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.IsValid);
        builder.ToTable("PACIENTE", ConfigurationHelper.SchemaMedex);
        //  builder.ToTable("PV_LOGIN", ConfigurationHelper.Schema);

    }

}

