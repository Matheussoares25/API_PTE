using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Notas
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // opcional: relacionar a prova, aula ou treinamento
        public int? ProvaId { get; set; }
        public int? TreinamentoId { get; set; }

        public decimal Valor { get; set; }

        public DateTime Criado { get; set; }
    }
}
