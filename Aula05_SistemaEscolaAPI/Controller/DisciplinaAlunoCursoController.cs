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
    public class DisciplinaAlunoCursoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaAlunoCursoController(AppDbContext context)
        {
            this._context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaAlunoCursoDTO>>> Get()
        // async para deixar a operação assíncronar e não bloquear o thread
        // Task<ActionResult<IEnumerable<DisciplinaAlunoCursoDTO>>> é para retornar uma lista de DTOs de disAluCur
        // IEnumerable<DisciplinaAlunoCursoDTO> é uma interface que representa uma coleção de objetos do tipo AlunoDto
        // ActionResult é uma classe base para resultados de ação em controladores ASP.NET
        {
            // var disAluCur = await _context.DisciplinasAlunosCursos
            // .Include(aluno => aluno.Aluno)
            // .Include(c => c.Curso)
            // .Include(d => d.Disciplina)
            // .Select(disAluCur => new DisciplinaAlunoCursoDTO { Aluno = , Curso = disAluCur.Curso.Descricao })
            // .ToListAsync();

            var disAluCur = await _context.DisciplinasAlunosCursos
            .Include(d => d.Aluno) // O include é para podermos usar as propriedades dentro de aluno
            .Include(d => d.Curso)
            .Include(d => d.Disciplina)
            .Select(d => new DisciplinaAlunoCursoDTO
            {
                Id = d.AlunoId + d.CursoId + d.DisciplinaId,
                AlunoId = d.AlunoId,
                AlunoNome = d.Aluno.Nome,
                CursoId = d.CursoId,
                CursoDescricao = d.Curso.Descricao,
                DisciplinaId = d.DisciplinaId,
                DisciplinaDescricao = d.Disciplina.Descricao
            })
            .ToListAsync();

            if (disAluCur == null) return NotFound("Relação não encontrada");

            return Ok(disAluCur);
        }
        #endregion

        #region GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplinaAlunoCursoDTO>> GetById(int id)
        {
            var disAluCur = await _context.DisciplinasAlunosCursos
            .Include(d => d.Aluno)
            .Include(d => d.Curso)
            .Include(f => f.Disciplina) // pode usar qualquer letra
            .ToListAsync();

            var relacao = disAluCur.FirstOrDefault(d => id == (d.AlunoId + d.CursoId + d.DisciplinaId));

            if (relacao == null) return NotFound("Relação não encontrada");

            var dto = new DisciplinaAlunoCursoDTO
            {
                Id = relacao.AlunoId + relacao.CursoId + relacao.DisciplinaId,
                AlunoId = relacao.AlunoId,
                AlunoNome = relacao.Aluno.Nome,
                CursoId = relacao.CursoId,
                CursoDescricao = relacao.Curso.Descricao,
                DisciplinaId = relacao.DisciplinaId,
                DisciplinaDescricao = relacao.Disciplina.Descricao
            };

            return Ok(disAluCur);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
        {
            var entidade = new DisciplinaAlunoCurso
            {
                AlunoId = disciplinaAlunoCursoDTO.AlunoId,
                CursoId = disciplinaAlunoCursoDTO.CursoId,
                DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId
            };
            _context.DisciplinasAlunosCursos.Add(entidade);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Relação Criada" });
        }
        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaAlunoCursoDTO disciplinaAlunoCursoDTO)
        {

            var entidade = await _context.DisciplinasAlunosCursos.FindAsync(id);
            if (entidade == null)
            {
                return NotFound();
            }

            entidade.AlunoId = disciplinaAlunoCursoDTO.AlunoId;
            entidade.CursoId = disciplinaAlunoCursoDTO.CursoId;
            entidade.DisciplinaId = disciplinaAlunoCursoDTO.DisciplinaId;

            _context.DisciplinasAlunosCursos.Update(entidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entidade = await _context.DisciplinasAlunosCursos.FindAsync(id);
            if (entidade == null)
            {
                return NotFound();
            }

            _context.DisciplinasAlunosCursos.Remove(entidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}