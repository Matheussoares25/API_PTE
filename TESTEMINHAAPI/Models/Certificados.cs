using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Certificados
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int TreinamentoId { get; set; }
        public Treinamentos Treinamento { get; set; }

        [MaxLength(200)]
        public string Codigo { get; set; }

        public DateTime EmitidoEm { get; set; }
    }
}
