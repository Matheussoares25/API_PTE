using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESTEMINHAAPI.Models
{
    public class Prova
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(200)]
        public string titulo { get; set; }

        public string descricao { get; set; }

        // Relacionamento com Treinamentos
        public int treinamento_id { get; set; }
        [ForeignKey("treinamento_id")]
        public Treinamentos treinamento { get; set; }

        // Pontuação máxima da prova
        public decimal pontuacao_maxima { get; set; }

        // Status: "ativa", "inativa", "finalizada"
        [MaxLength(50)]
        public string status { get; set; } = "ativa";

        // Questões relacionadas a esta prova
        public ICollection<Questoes> questoes { get; set; }

        // Notas/resultados dos usuários nesta prova
        public ICollection<Notas> notas { get; set; }

        public DateTime criado { get; set; }
    }
}
