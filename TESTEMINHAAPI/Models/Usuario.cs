using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Usuario
    {
        public int id { get; set; }

        [MaxLength(120)]
        public string email { get; set; }

        public string senha { get; set; }

        public int ativo { get; set; }

        [MaxLength(255)]
        public string token { get; set; }

        public int tipo { get; set; }

        public string nome { get; set; }

        public int acesso { get; set; }
    }
}
