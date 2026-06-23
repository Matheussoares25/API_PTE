using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Midias
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(200)]
        public string nome { get; set; }

        // URL ou caminho do arquivo de mídia
        [MaxLength(500)]
        public string url { get; set; }

        // Tipo: "video", "audio", "imagem", etc.
        [MaxLength(50)]
        public string tipo { get; set; }

        // Relacionamento com Aulas
        public int aula_id { get; set; }
        public Aulas aula { get; set; }

        public DateTime criado { get; set; }
    }
}
