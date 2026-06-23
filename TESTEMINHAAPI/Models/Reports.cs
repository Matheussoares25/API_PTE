using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Reports
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [MaxLength(500)]
        public string Mensagem { get; set; }

        [MaxLength(100)]
        public string Tipo { get; set; }

        public DateTime Criado { get; set; }
    }
}
