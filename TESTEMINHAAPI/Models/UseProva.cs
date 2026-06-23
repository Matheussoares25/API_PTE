using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class UseProva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int usuario_id { get; set; }
        public Usuario usuario { get; set; }

        public int prova_id { get; set; }

        public decimal nota { get; set; }

        public DateTime realizado_em { get; set; }
    }
}
