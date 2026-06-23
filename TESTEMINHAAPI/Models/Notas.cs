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
        public Usuario usuario { get; set; }

        // opcional: relacionar a prova, aula ou treinamento
        public int? prova_id { get; set; }
        public int? treinamento_id { get; set; }

        public decimal valor { get; set; }

        public DateTime criado { get; set; }
    }
}
