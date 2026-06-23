using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class UseTreinamentos
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int TreinamentoId { get; set; }
        public Treinamentos Treinamento { get; set; }

        public DateTime MatriculadoEm { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "matriculado";
    }
}
