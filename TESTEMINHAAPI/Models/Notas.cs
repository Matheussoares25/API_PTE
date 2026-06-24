using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Notas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int usuario_id { get; set; }
        [ForeignKey("usuario_id")]
        public Usuario usuario { get; set; }

        // Relacionamento com Prova
        public int? prova_id { get; set; }
        [ForeignKey("prova_id")]
        public Prova prova { get; set; }

        // Relacionamento com Treinamento (opcional)
        public int? treinamento_id { get; set; }

        public decimal valor { get; set; }

        public DateTime criado { get; set; }
    }
}
