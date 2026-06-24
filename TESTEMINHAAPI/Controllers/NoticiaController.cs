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
            var noticias = _context.Noticias.ToList();
            return Ok(noticias);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var noticia = _context.Noticias.FirstOrDefault(t => t.id == id);
            // No-op null handling: preserve 404 response for missing noticia
            if (noticia == null)
            {
                return NotFound();
            }
            return Ok(noticia);
        }


        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Criar(Noticia noticia)
        {
            noticia.data_noticia ??= DateTime.Now;
            _context.Noticias.Add(noticia);
            _context.SaveChanges();  

            return CreatedAtAction(nameof(Obter), new { id = noticia.id }, noticia);
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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

        [Authorize(Roles = "2,3")]
        [HttpPut("{id}")]
        public IActionResult Editar(int id, Noticia dto)
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
    }
}
