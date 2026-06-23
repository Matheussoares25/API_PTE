using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Progress
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Relacionamento com Aula
        public int AulaId { get; set; }
        public Aulas Aula { get; set; }

        // Progresso em percentual (0-100)
        public decimal Percentual { get; set; }

        // Tempo assistido em segundos
        public int TempoSegundos { get; set; }

        public DateTime AtualizadoEm { get; set; }
    }
}
