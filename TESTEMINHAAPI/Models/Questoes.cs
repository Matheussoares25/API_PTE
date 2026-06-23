using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Questoes
    {
        public int id { get; set; }

        [MaxLength(500)]
        public string texto { get; set; }

        // Relacionamento com Aulas
        public int aula_id { get; set; }
        public Aulas aula { get; set; }

        // Alternativas relacionadas a esta questão
        public ICollection<Alternativas> alternativas { get; set; }

        public DateTime criado { get; set; }
    }
}
