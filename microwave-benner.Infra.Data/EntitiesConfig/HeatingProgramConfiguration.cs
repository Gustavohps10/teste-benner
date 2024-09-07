using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using microwave_benner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microwave_benner.Infra.Data.EntitiesConfig
{
    internal class HeatingProgramConfiguration : IEntityTypeConfiguration<HeatingProgram>
    {
        public void Configure(EntityTypeBuilder<HeatingProgram> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.food).IsRequired().HasMaxLength(100);
            builder.Property(x => x.time).IsRequired();
            builder.Property(x => x.power).IsRequired();
            builder.Property(x => x.instructions).HasMaxLength(500);
            builder.Property(x => x.custom).IsRequired();
            builder.Property(x => x.heatingChar).IsRequired().HasMaxLength(1);

            builder.ToTable("heatingPrograms");

            builder.HasData(
                new HeatingProgram(1, "Pipoca", "Pipoca (de micro-ondas)", 3 * 60, 7, '*', "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."),
                new HeatingProgram(2, "Leite", "Leite", 5 * 60, 5, '#', "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."),
                new HeatingProgram(3, "Carnes de boi", "Carne em pedaço ou fatias", 14 * 60, 4, '=', "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."),
                new HeatingProgram(4, "Frango", "Frango (qualquer corte)", 8 * 60, 7, '%', "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."),
                new HeatingProgram(5, "Feijão", "Feijão congelado", 8 * 60, 9, '$', "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.")
            );
        }
    }
}
