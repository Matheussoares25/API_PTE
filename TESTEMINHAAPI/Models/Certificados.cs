using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Certificados
    {
        public int id { get; set; }

        public int usuario_id { get; set; }
        public Usuario usuario { get; set; }

        public int treinamento_id { get; set; }
        public Treinamentos treinamento { get; set; }

        [MaxLength(200)]
        public string codigo { get; set; }

        public DateTime emitido_em { get; set; }
    }
}
