using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Questoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(500)]
        public string texto { get; set; }

        // Relacionamento com Aulas
        public int aula_id { get; set; }
        [ForeignKey("aula_id")]
        public Aulas aula { get; set; }

        // Relacionamento com Prova
        public int prova_id { get; set; }
        [ForeignKey("prova_id")]
        public Prova prova { get; set; }

        // Alternativas relacionadas a esta questão
        public ICollection<Alternativas> alternativas { get; set; }

        public DateTime criado { get; set; }
    }
}
