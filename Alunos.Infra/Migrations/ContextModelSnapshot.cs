﻿// <auto-generated />
using System;
using Alunos.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alunos.Infra.Migrations
{
    [DbContext(typeof(Context.Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alunos.Domain.Entities.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aprovado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<double?>("MediaCompeticao")
                        .HasColumnType("float");

                    b.Property<double?>("MediaFinal")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double?>("NotaCompeticao")
                        .HasColumnType("float");

                    b.Property<double?>("NotaProva1")
                        .HasColumnType("float");

                    b.Property<double?>("NotaProva2")
                        .HasColumnType("float");

                    b.Property<double?>("NotaProva3")
                        .HasColumnType("float");

                    b.Property<double?>("NotaRecuperacao")
                        .HasColumnType("float");

                    b.Property<int?>("TurmaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TurmaId");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("Alunos.Domain.Entities.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("Alunos.Domain.Entities.Aluno", b =>
                {
                    b.HasOne("Alunos.Domain.Entities.Turma", "Turma")
                        .WithMany("Alunos")
                        .HasForeignKey("TurmaId");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Alunos.Domain.Entities.Turma", b =>
                {
                    b.Navigation("Alunos");
                });
#pragma warning restore 612, 618
        }
    }
}
