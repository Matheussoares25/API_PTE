using Microsoft.AspNetCore.Mvc;
using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.BancoDeDados;

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

        [HttpGet]
        public IActionResult Listar()
        {
            var Treinamentos = _context.Noticias.ToList();
            return Ok(Treinamentos);
        }
        [HttpGet("{id}")]
        public IActionResult Obter(int id)
        {
            var Treinamento = _context.Noticias.FirstOrDefault(t => t.Id == id);
            if (Treinamento == null)
            {
                return NotFound();
            }
            return Ok(Treinamento);
        }

        [HttpPost]
        public IActionResult Criar(Noticia noticia)
        {
            _context.Noticias.Add(noticia);
            _context.SaveChanges();
            return Ok(new { successo = true, message = "Noticia Criada com Sucesso" });

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var noticia = _context.Noticias.FirstOrDefault(t => t.Id == id);
            if (noticia == null)
            {
                return NotFound();
            }
            _context.Noticias.Remove(noticia);
            _context.SaveChanges();
            return Ok(new {successo = true, message = "Noticia Apagada"});
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, Noticia dto)
        {
            var noticia = _context.Noticias.FirstOrDefault(t => t.Id == id);

            if (noticia == null)
            {
                return NotFound();
            }

            noticia.Titulo = dto.Titulo;
            noticia.Conteudo = dto.Conteudo;
            noticia.Vaga = dto.Vaga;

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
