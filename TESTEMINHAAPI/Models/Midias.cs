using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Midias
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Nome { get; set; }

        // URL ou caminho do arquivo de mídia
        [MaxLength(500)]
        public string Url { get; set; }

        // Tipo: "video", "audio", "imagem", etc.
        [MaxLength(50)]
        public string Tipo { get; set; }

        // Relacionamento com Aulas
        public int AulaId { get; set; }
        public Aulas Aula { get; set; }

        public DateTime Criado { get; set; }
    }
}
