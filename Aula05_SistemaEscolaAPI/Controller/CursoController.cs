using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aula05_SistemaEscolaAPI.DTO;
using Aula05_SistemaEscolaAPI.DB;
using Aula05_SistemaEscolaAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace Aula05_SistemaEscolaAPI.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class CursoController : ControllerBase
        {
            private readonly AppDbContext _context;

            public CursoController(AppDbContext context)
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
                var cursos = await _context.Cursos
                .Select(cursos => new CursoDTO {
                     Id = cursos.Id,
                     Descricao = cursos.Descricao})
                .ToListAsync();

                return Ok(cursos);
            }
            #endregion

            #region GetById
            [HttpGet("{id}")]
            public async Task<ActionResult<CursoDTO>> GetById(int id)
            {
                var curso = await _context.Cursos.FindAsync(id);

                if (curso == null) return NotFound("Curso não encontrado");

                var cursoDto = new CursoDTO
                {
                    Id = curso.Id,
                    Descricao = curso.Descricao
                };

                return Ok(cursoDto);
            }
            #endregion

            #region Post
            [HttpPost]
            public async Task<ActionResult> Post([FromBody] CursoDTO cursoDTO)
            {
                var curso = new Curso { Descricao = cursoDTO.Descricao};

                this._context.Cursos.Add(curso);
                await _context.SaveChangesAsync();

                return Ok("Curso criado com sucesso!");
            }
            #endregion

            #region Put
            [HttpPut("{id}")]
            public async Task<ActionResult> Put(int id, [FromBody] CursoDTO cursoDTO)
            {
                var curso = await _context.Cursos.FirstOrDefaultAsync(a => a.Id == id);
                if (curso == null) return BadRequest("Curso não encontrado"); // Caso caia neste if vai dar erro 400 (Erro 400 é o erro que tem, mas falta alguma coisa e o 404 é o que não tem nada)

                curso.Descricao = cursoDTO.Descricao;

                _context.Cursos.Update(curso); // Update é um método que atualiza a entidade no banco
                await _context.SaveChangesAsync();

                return Ok(new {mensagem = "Curso Atualizado com Sucesso!"});
            }
            #endregion

            #region Delete
            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(int id)
            {
                var curso = await _context.Cursos.FindAsync(id);

                if (curso == null) return NotFound("Curso não encontrado");

                this._context.Cursos.Remove(curso);

                await _context.SaveChangesAsync();

                return Ok(new {mensagem = "Curso deletado com Sucesso!"});
            }
            #endregion
        }
    }