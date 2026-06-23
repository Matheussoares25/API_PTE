using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Questoes
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Texto { get; set; }

        // Relacionamento com Aulas
        public int AulaId { get; set; }
        public Aulas Aula { get; set; }

        // Alternativas relacionadas a esta questão
        public ICollection<Alternativas> Alternativas { get; set; }

        public DateTime Criado { get; set; }
    }
}
