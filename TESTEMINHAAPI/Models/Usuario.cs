using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [MaxLength(120)]
        public string Email { get; set; }

        public string Senha { get; set; }

        public int Ativo { get; set; }
        
        [MaxLength(255)]
        public string Token { get; set; }

        public int Tipo { get; set; }

        public string Nome { get; set; }

        public int Acesso { get; set; }

    }
}
