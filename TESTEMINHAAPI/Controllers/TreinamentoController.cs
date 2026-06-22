using TESTEMINHAAPI.Models;
using TESTEMINHAAPI.BancoDeDados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace TESTEMINHAAPI.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class TreinamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TreinamentoController(AppDbContext context) { 
            
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public IActionResult BuscaTreinamentos()
        {
            var tipouser = User.FindFirst("Tipo")?.Value;

            if (tipouser == "2")
            {
                var Treinamentos = _context.Treinamentos.ToList();
                return Ok(Treinamentos);
            }

            return BadRequest();

        }
}
}
