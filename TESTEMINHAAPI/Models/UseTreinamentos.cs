using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class UseTreinamentos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int usuario_id { get; set; }
        public Usuario usuario { get; set; }

        public int treinamento_id { get; set; }
        public Treinamentos treinamento { get; set; }

        public DateTime matriculado_em { get; set; }

        [MaxLength(50)]
        public string status { get; set; } = "matriculado";
    }
}
