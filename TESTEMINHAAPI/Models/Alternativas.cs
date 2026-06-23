using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Alternativas
    {
        public int Id { get; set; }

        // Relacionamento com Questoes
        public int QuestaoId { get; set; }
        public Questoes Questao { get; set; }

        [MaxLength(500)]
        public string Texto { get; set; }

        // opcional: mídia ou URL para a alternativa
        [MaxLength(500)]
        public string Url { get; set; }

        // indica se esta alternativa é a correta
        public bool Correta { get; set; }

        // ordem de exibição
        public int Ordem { get; set; }

        public DateTime Criado { get; set; }
    }
}
