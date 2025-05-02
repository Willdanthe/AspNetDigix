using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aula05_SistemaEscolaAPI.Models;
using Aula05_SistemaEscolaAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Aula05_SistemaEscolaAPI.DB;
using Microsoft.EntityFrameworkCore;

namespace Aula05_SistemaEscolaAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunoController(AppDbContext context)
        {
            this._context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get()
        // async para deixar a operação assíncronar e não bloquear o thread
        // Task<ActionResult<IEnumerable<AlunoDTO>>> é para retornar uma lista de DTOs de alunos
        // IEnumerable<AlunoDTO> é uma interface que representa uma coleção de objetos do tipo AlunoDto
        // ActionResult é uma classe base para resultados de ação em controladores ASP.NET
        {
            var alunos = await _context.Alunos
            .Include(a => a.Curso)
            .Select(alunos => new AlunoDTO { Id = alunos.Id, Nome = alunos.Nome, Curso = alunos.Curso.Descricao })
            .ToListAsync();

            return Ok(alunos);
        }
        #endregion

        #region GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<AlunoDTO>> GetById(int id)
        {
            // var aluno = await _context.Alunos
            // .Include(a => a.Curso)
            // .FirstOrDefaultAsync(a => a.Id == id);

            var aluno = await _context.Alunos
            .Include(a => a.Curso)
            .Select(aluno => new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Curso = aluno.Curso.Descricao
            }).FirstOrDefaultAsync(a => a.Id == id);

            // FirstOrDefaultAsync é um método assíncrono que retorna o primeiro elemento que atende a condição atribuida a ele, ou valor padrão se nenhum dele for encontrado, que é 500
            // O Include é um método que inclui entidades relacionadas na consulta, permitindo carregar dados relacionados (como o curso do aluno) junto com o aluno
            if (aluno == null) return NotFound("Aluno não encontrado");

            return Ok(aluno);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AlunoDTO alunoDto)
        {
            var Curso = await _context.Cursos.FirstOrDefaultAsync(C => C.Descricao == alunoDto.Curso);

            if (Curso == null) return BadRequest("Curso não encontrado");

            var aluno = new Aluno { Nome = alunoDto.Nome, CursoId = Curso.Id };

            this._context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Aluno Cadastrado com sucesso!" }); // Mensagem é uma propriedade anonima que contém algum valor atribuido a ela
        }
        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] AlunoDTO alunoDTO)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null) return NotFound("Aluno não encontrado"); // Caso caia neste if Vai dar erro 404

            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == alunoDTO.Curso);

            if (curso == null) return BadRequest("Curso não encontrado"); // Caso caia neste if vai dar erro 400 (Erro 400 é o erro que tem, mas falta alguma coisa e o 404 é o que não tem nada)

            aluno.Nome = alunoDTO.Nome;
            aluno.CursoId = curso.Id;

            _context.Alunos.Update(aluno); // Update é um método que atualiza a entidade no banco

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Aluno alterado com Sucesso!" });
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null) return NotFound("Aluno não encontrado");

            this._context.Alunos.Remove(aluno);

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Aluno excluído com sucesso!" });
        }
        #endregion


    }
}