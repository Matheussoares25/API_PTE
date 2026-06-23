using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Alternativas
    {
        public int id { get; set; }

        // Relacionamento com Questoes
        public int questao_id { get; set; }
        public Questoes questao { get; set; }

        [MaxLength(500)]
        public string texto { get; set; }

        // opcional: mídia ou URL para a alternativa
        [MaxLength(500)]
        public string url { get; set; }

        // indica se esta alternativa é a correta
        public bool correta { get; set; }

        // ordem de exibição
        public int ordem { get; set; }

        public DateTime criado { get; set; }
    }
}
