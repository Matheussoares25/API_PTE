using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class UseProva
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ProvaId { get; set; }

        public decimal Nota { get; set; }

        public DateTime RealizadoEm { get; set; }
    }
}
