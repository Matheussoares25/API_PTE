using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Aulas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(125)]
        public string? nome { get; set; }

        public string? conteudo { get; set; }

        // Relacionamento com Modulos
        public int modulo_id { get; set; }
        [ForeignKey("modulo_id")]
        public Modulos? modulo { get; set; }
        // URL da mídia associada a esta aula
        [MaxLength(500)]
        public string? midia_url { get; set; }

        public DateTime criado { get; set; }
    }
}
