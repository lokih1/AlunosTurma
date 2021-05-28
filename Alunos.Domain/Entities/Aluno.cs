using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Alunos.Domain.Entities
{
    public class Aluno : IEntityTypeConfiguration<Aluno>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Aprovado { get; set; }
        public int? TurmaId {get; set;}
        public virtual Turma Turma { get; set; }
        public double? NotaProva1 { get; set; }
        public double? NotaProva2 { get; set; }
        public double? NotaProva3 { get; set; }
        public double? MediaFinal { get; set; }
        public double? NotaCompeticao { get; set; }

        public double? MediaCompeticao { get; set; }
        public double? NotaRecuperacao { get; set; }



        public double CalculaMediaPonderada()
        {
            var mediaFinal = ((NotaProva1.Value * 1) + (NotaProva2.Value * 1.2) + (NotaProva3.Value * 1.4)) / 3.6;
            return Math.Round(mediaFinal,2);
        }

        public double CalcularMediaCompeticao()
        {
            var mediaCompeticao = ((NotaProva1.Value * 1) + (NotaProva2.Value * 1) + (NotaProva3.Value * 1) + (NotaCompeticao * 2)) / 5;
            return Math.Round(mediaCompeticao.Value,2);
        }

        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(al => al.Id);
            builder.Property(al => al.Nome).HasMaxLength(200);
        }
    }
}
