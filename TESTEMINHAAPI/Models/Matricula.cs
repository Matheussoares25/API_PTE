using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESTEMINHAAPI.Models
{
    public class Matricula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int usuario_id { get; set; }
        [ForeignKey("usuario_id")]
        public Usuario? Usuario { get; set; }

        public int treinamento_id { get; set; }
        [ForeignKey("treinamento_id")]
        public Treinamentos? treinamento { get; set; }

        public DateTime matriculado_em { get; set; }

        [MaxLength(50)]
        public string status { get; set; } = "matriculado";
    }
}
