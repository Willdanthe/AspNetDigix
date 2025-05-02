using Aula05_SistemaEscolaAPI.Models;
using Aula05_SistemaEscolaAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Aula05_SistemaEscolaAPI.DB;
using Microsoft.EntityFrameworkCore;

namespace Aula05_SistemaEscolaAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinaController(AppDbContext context)
        {
            this._context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisciplinaDTO>>> Get()
        // async para deixar a operação assíncronar e não bloquear o thread
        // Task<ActionResult<IEnumerable<DisciplinaDTO>>> é para retornar uma lista de DTOs de disciplinas
        // IEnumerable<DisciplinaDTO> é uma interface que representa uma coleção de objetos do tipo DisciplinaDTO
        // ActionResult é uma classe base para resultados de ação em controladores ASP.NET
        {
            var disciplinas = await _context.Disciplinas
            .Include(d => d.Curso)
            .Select(disciplinas => new DisciplinaDTO { Descricao = disciplinas.Descricao, Curso = disciplinas.Curso.Descricao })
            .ToListAsync();

            return Ok(disciplinas);
        }
        #endregion

        #region GetById
        [HttpGet("{id}")]
        public async Task<ActionResult<DisciplinaDTO>> GetById(int id)
        {
            var disciplinaDto = await _context.Disciplinas
            .Where(d => d.Id == id)
            .Select(d => new DisciplinaDTO {
                Id = d.Id,
                Curso = d.Curso.Descricao,
                Descricao = d.Descricao
            }).FirstOrDefaultAsync();

            if (disciplinaDto == null) return NotFound("Disciplina não encontrada");

            return Ok(disciplinaDto);
        }

        #endregion

        #region  Post
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaDTO disciplinaDTO)
        {
            var Curso = await _context.Cursos.FirstOrDefaultAsync(C => C.Descricao == disciplinaDTO.Curso);

            if (Curso == null) return BadRequest("Curso não encontrado");

            var disciplina = new Disciplina { Descricao = disciplinaDTO.Descricao, CursoId = Curso.Id };

            this._context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Disciplina Cadastrada"});
        }
        #endregion

        #region Put
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DisciplinaDTO disciplinaDTO)
        {
            var disciplina = await _context.Disciplinas.FirstOrDefaultAsync(a => a.Id == id);

            if (disciplina == null) return NotFound("Disciplina não encontrado"); // Caso caia neste if Vai dar erro 404

            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == disciplinaDTO.Curso);

            if (curso == null) return BadRequest("Curso não encontrado"); // Caso caia neste if vai dar erro 400 (Erro 400 é o erro que tem, mas falta alguma coisa e o 404 é o que não tem nada)

            disciplina.Descricao = disciplinaDTO.Descricao;
            disciplina.CursoId = curso.Id;

            _context.Disciplinas.Update(disciplina); // Update é um método que atualiza a entidade no banco

            await _context.SaveChangesAsync();

            return Ok(new {mensagem = "Disciplina atualizada com sucesso!"});
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var aluno = await _context.Disciplinas.FindAsync(id);

            if (aluno == null) return NotFound("Disciplina não encontrado");

            this._context.Disciplinas.Remove(aluno);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
