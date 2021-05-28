using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alunos.Domain.Entities
{
    public class Turma : IEntityTypeConfiguration<Turma>
    {

        public Turma()
        {
            Alunos = new HashSet<Aluno>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }

        public virtual ICollection<Aluno> Alunos { get; set; }

        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(al => al.Id);
            builder.Property(al => al.Nome).HasMaxLength(200);
        }
    }





}
