using Alunos.Domain.Entities;
using Alunos.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alunos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {

        private readonly Context _context;

        public AlunoController(Context context)
        {
            _context = context;
        }

        // GET: api/<AlunoController>
        [HttpGet]
        public async Task<IEnumerable<Aluno>> Get()
        {
             var alunos = await _context.Alunos
                .ToListAsync();

            return alunos;
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AlunoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST api/<AlunoController>
        [HttpPost("criar-alunos")]
        public async void CriarAlunos()
        {

            var listaAlunos = new List<Aluno>(capacity: 60);
            listaAlunos.Add(new Aluno() { Nome = "Adriana Oliveira", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Adão Villegas", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Aida Fraga", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Aires Acevedo", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Alda Beserril", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Anabela Arruda", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Andreoleto Dias", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Anind Aragón", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Araci Galante", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Arcidres Vergueiro", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Armanda Hipólito", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Beatriz Sabrosa", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Bibiana Salgueiro", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Carina Furtado", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Conceição Coito", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Cora Simón", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Dalila Castella", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Derli López", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Dora Águeda", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Elsa Bonilla", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Ermelinda Grangeia", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Eugénio Saraiva", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Fabiano Villegas", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Fiona Pino", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Flávio Nieves", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Francisco Affonso", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Genoveva Sabrosa", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Gueda Paixão", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Herculano Goulart", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Isilda Paula", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Jaime Terra", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Joel Dinis", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Leopoldo Sampaio", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Liedson Ipiranga", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Mariano Franco", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Moaci Benevides", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Natividade Neto", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Neuza Espargosa", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Noé Amado", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Ofélia Barros", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Patrício Guarabira", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Paula Cabeza de Vaca", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Paulina Meireles", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Quévin Botica", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Derli Vargas", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Eulália Canedo", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Henriqueta Salles", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Filipe Sá", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Hélia Rodovalho", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Jamari Montero", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Magda Figueroa", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Pascoal Nobre", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Liedson Vilariça", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Magda Figueroa", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Nivaldo Bensaúde", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Teodora Norões", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Timóteo Guedez", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Tália Murici", DataCriacao = DateTime.Now });
            listaAlunos.Add(new Aluno() { Nome = "Viridiana Colaço", DataCriacao = DateTime.Now });

            _context.Alunos.AddRange(listaAlunos);
            await _context.SaveChangesAsync();    
        }
    }
}
