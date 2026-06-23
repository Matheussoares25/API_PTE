using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Reports
    {
        public int id { get; set; }

        public int usuario_id { get; set; }
        public Usuario usuario { get; set; }

        [MaxLength(500)]
        public string mensagem { get; set; }

        [MaxLength(100)]
        public string tipo { get; set; }

        public DateTime criado { get; set; }
    }
}
