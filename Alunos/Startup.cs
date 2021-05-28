using Alunos.Domain.Entities;
using Alunos.Infra.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alunos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            services.AddControllers().AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Alunos", Version = "v1" });
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContextPool<Context>(options =>
            {
                // Banco de dados em Memória para Testes
                // options.UseInMemoryDatabase("Context");
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Alunos;Trusted_Connection=True;MultipleActiveResultSets=true");
                options.UseLazyLoadingProxies();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alunos v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Método Principal
            FluxoPrograma(context);
        }

        // ToDo -> Criar uma extensions pra popular o banco de dados.
        private void FluxoPrograma(Context context)
        {
            Seed(context);

            // 2. Gerar uma simulação das notas nas 3 provas para cada aluno.

            var alunos = context.Alunos.ToList();

            foreach (var aluno in alunos)
            {

                aluno.NotaProva1 = GerarNota();
                aluno.NotaProva2 = GerarNota();
                aluno.NotaProva3 = GerarNota();
                var mediaPonderada = aluno.CalculaMediaPonderada();
                aluno.MediaFinal = mediaPonderada;


                // Verificar se o aluno está aprovado
                if (mediaPonderada > 6)
                    aluno.Aprovado = true;

                // Verifica se ficou de recuperação
                if (mediaPonderada >= 4 && mediaPonderada < 6)
                {
                    aluno.NotaRecuperacao = GerarNota();

                    // Se a media de recuperação for maior ou igual a 5
                    var mediaFinal = (mediaPonderada + aluno.NotaRecuperacao) / 2;
                    if (mediaFinal >= 5)
                        aluno.Aprovado = true;
                }
                context.SaveChanges();
            }

            var alunosExibicao =  context.Alunos.OrderByDescending(c => c.Nome).ToList();

            foreach (var alunoExibicao in alunosExibicao)
            {
                Console.WriteLine("Aluno: {0}", alunoExibicao.Nome);
                Console.WriteLine("Nota 1: {0}", alunoExibicao.NotaProva1);
                Console.WriteLine("Nota 2: {0}", alunoExibicao.NotaProva2);
                Console.WriteLine("Nota 3: {0}", alunoExibicao.NotaProva3);
                Console.WriteLine("Nota Recuperação: {0}", alunoExibicao.NotaRecuperacao);
                Console.WriteLine("Aprovado: {0}", alunoExibicao.Aprovado ? "Aprovado" : "Reprovado");
                Console.WriteLine("MediaFinal: {0}", alunoExibicao.MediaFinal);
                Console.WriteLine("");
                Console.WriteLine("");
            }



            // Pegar os 5 alunos que obtiveram as melhores médias em cada turma

            var alunosComMaioresMedias = context.Alunos.OrderByDescending(al => al.MediaFinal).Take(5).ToList();

            Console.WriteLine("ALUNOS SELECIONADOS PARA COMPETIÇÃO");
            Console.WriteLine("");
            foreach (var alunoSelecionado in alunosComMaioresMedias)
            {
                alunoSelecionado.NotaCompeticao = GerarNota();
                alunoSelecionado.MediaCompeticao = alunoSelecionado.CalcularMediaCompeticao();

                Console.WriteLine("Aluno: {0} ", alunoSelecionado.Nome);
                Console.WriteLine("Nota da Competição: {0} ", alunoSelecionado.MediaCompeticao);
                Console.WriteLine("Media Final das provas: {0}", alunoSelecionado.MediaFinal);
                Console.WriteLine("");
                Console.WriteLine("");

            }

             context.SaveChanges();

            var alunoVencedor =  context.Alunos.OrderByDescending(c => c.MediaCompeticao).FirstOrDefault();

            Console.WriteLine("Aluno Vencedor da Competição {0} com a nota {1} : ", alunoVencedor.Nome, alunoVencedor.MediaCompeticao);

        }

        double GerarNota()
        {
            var rand = new Random();
            var notaGerada = Math.Round(rand.NextDouble() * 10, 2);
            return notaGerada;
        }

        public  bool Seed(Context context)
        {
            if (!context.Database.EnsureCreated())
                context.Database.Migrate();

            

            var success = false;
            // Gerando as 3 Turmas

            if (!context.Turmas.Any())
            {
                var listaDeTurmas = new List<Turma>(capacity: 3);

                listaDeTurmas.Add(new Turma() { Nome = "Turma 1", DataCriacao = DateTime.Now });
                listaDeTurmas.Add(new Turma() { Nome = "Turma 2", DataCriacao = DateTime.Now });
                listaDeTurmas.Add(new Turma() { Nome = "Turma 3", DataCriacao = DateTime.Now });

                context.Turmas.AddRange(listaDeTurmas);
                context.SaveChanges();
            }


            if (!context.Alunos.Any())
            {
                // Gerar 20 alunos para cada uma das 3 turmas.
                var listaAlunos = new List<Aluno>(capacity: 60);

                listaAlunos.Add(new Aluno() { Nome = "Adriana Oliveira", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Adão Villegas", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Aida Fraga", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Aires Acevedo", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Alda Beserril", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Anabela Arruda", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Andreoleto Dias", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Anind Aragón", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Araci Galante", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Arcidres Vergueiro", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Armanda Hipólito", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Beatriz Sabrosa", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Bibiana Salgueiro", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Carina Furtado", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Conceição Coito", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Cora Simón", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Dalila Castella", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Derli López", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Dora Águeda", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Elsa Bonilla", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Ermelinda Grangeia", DataCriacao = DateTime.Now, TurmaId = 1 });
                listaAlunos.Add(new Aluno() { Nome = "Eugénio Saraiva", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Fabiano Villegas", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Fiona Pino", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Flávio Nieves", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Francisco Affonso", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Genoveva Sabrosa", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Gueda Paixão", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Herculano Goulart", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Isilda Paula", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Jaime Terra", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Joel Dinis", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Leopoldo Sampaio", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Liedson Ipiranga", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Mariano Franco", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Moaci Benevides", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Natividade Neto", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Neuza Espargosa", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Noé Amado", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Ofélia Barros", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Patrício Guarabira", DataCriacao = DateTime.Now, TurmaId = 2 });
                listaAlunos.Add(new Aluno() { Nome = "Paula Cabeza de Vaca", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Paulina Meireles", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Quévin Botica", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Derli Vargas", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Eulália Canedo", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Henriqueta Salles", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Filipe Sá", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Hélia Rodovalho", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Jamari Montero", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Magda Figueroa", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Pascoal Nobre", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Liedson Vilariça", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Magda Figueroa", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Nivaldo Bensaúde", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Teodora Norões", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Timóteo Guedez", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Tália Murici", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Viridiana Colaço", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Cleber Gonzaga", DataCriacao = DateTime.Now, TurmaId = 3 });
                listaAlunos.Add(new Aluno() { Nome = "Gabriel Valorant", DataCriacao = DateTime.Now, TurmaId = 3 });

                context.Alunos.AddRange(listaAlunos);
                context.SaveChanges();
            }
            success = true;
            return success;
        }
    }
}
