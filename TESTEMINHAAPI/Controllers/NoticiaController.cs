using Microsoft.AspNetCore.Mvc;
using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.BancoDeDados;
using Microsoft.AspNetCore.Authorization;

namespace TESTEMINHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoticiaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NoticiaController(AppDbContext context) {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var noticias = _context.Noticias.ToList();
                return Ok(noticias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao listar as notícias.", erro = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            try
            {
                var noticia = _context.Noticias.FirstOrDefault(t => t.id == id);
                // No-op null handling: preserve 404 response for missing noticia
                if (noticia == null)
                {
                    return NotFound();
                }
                return Ok(noticia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao obter a notícia.", erro = ex.Message });
            }
        }


        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Noticia noticia)
        {
            try
            {
                if (noticia == null)
                {
                    return BadRequest(new { message = "Dados da notícia inválidos." });
                }

                if (string.IsNullOrWhiteSpace(noticia.titulo))
                {
                    return BadRequest(new { message = "Título é obrigatório." });
                }

                noticia.data_noticia ??= DateTime.Now;
                _context.Noticias.Add(noticia);
                _context.SaveChanges();

                return CreatedAtAction(nameof(Obter), new { id = noticia.id }, noticia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao criar a notícia.", erro = ex.Message });
            }
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var noticia = _context.Noticias.FirstOrDefault(t => t.id == id);
                if (noticia == null)
                {
                    return NotFound();
                }
                _context.Noticias.Remove(noticia);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao deletar a notícia.", erro = ex.Message });
            }
        }

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Noticia dto)
        {
            try
            {
                var noticia = _context.Noticias.FirstOrDefault(t => t.id == id);

                if (noticia == null)
                {
                    return NotFound();
                }

                noticia.titulo = dto.titulo;
                noticia.conteudo = dto.conteudo;
                noticia.vaga = dto.vaga;

                _context.SaveChanges();

                return Ok(new
                {
                    successo = true,
                    message = "Notícia atualizada com sucesso",
                    data = noticia
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao editar a notícia.", erro = ex.Message });
            }
        }
    }
}
