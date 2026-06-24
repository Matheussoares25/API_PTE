using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Progress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int usuario_id { get; set; }
        [ForeignKey("usuario_id")]
        public Usuario usuario { get; set; }

        // Relacionamento com Aula
        public int aula_id { get; set; }
        [ForeignKey("aula_id")]
        public Aulas aula { get; set; }

        // Progresso em percentual (0-100)
        public decimal percentual { get; set; }

        // Tempo assistido em segundos
        public int tempo_segundos { get; set; }

        public DateTime atualizado_em { get; set; }
    }
}
