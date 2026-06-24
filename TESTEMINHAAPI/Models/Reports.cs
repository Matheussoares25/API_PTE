using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Reports
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int usuario_id { get; set; }
        [ForeignKey("usuario_id")]
        public Usuario usuario { get; set; }

        [MaxLength(500)]
        public string mensagem { get; set; }

        [MaxLength(100)]
        public string tipo { get; set; }

        public DateTime criado { get; set; }
    }
}
