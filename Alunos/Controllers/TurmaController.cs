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
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {

        private readonly Context _context;

        public TurmaController(Context context)
        {
            _context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Turma>> Get()
        {
            var turmas = await _context.Turmas
                .AsNoTracking()
                .ToListAsync();

            return turmas;

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<Turma> Get(int id)
        {
            var turma = await _context.Turmas.Where(c => c.Id == id).FirstOrDefaultAsync();

            return turma;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("criar-turmas")]
        public async void CriarTurmas()
        {
            var listaDeTurmas = new List<Turma>(capacity: 3);

            listaDeTurmas.Add(new Turma() { Nome = "Turma 1", DataCriacao = DateTime.Now });
            listaDeTurmas.Add(new Turma() { Nome = "Turma 2", DataCriacao = DateTime.Now });
            listaDeTurmas.Add(new Turma() { Nome = "Turma 3", DataCriacao = DateTime.Now });

            _context.Turmas.AddRange(listaDeTurmas);
            await _context.SaveChangesAsync();
        }

        // Preenche cada turma com 20 alunos
        [HttpGet("preencher-turmas")]
        public async void PreencherTurma()
        {
            var turmas = await _context.Turmas.ToListAsync();

            foreach (var turma in turmas)
            {
                var alunos = await _context.Alunos.Where(al => al.TurmaId == null).Take(20).ToListAsync();
                turma.Alunos = alunos;
                await _context.SaveChangesAsync();
            }
        }
    }
}
