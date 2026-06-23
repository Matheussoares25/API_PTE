using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Vagas
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Titulo { get; set; }

        public string Descricao { get; set; }

        [MaxLength(150)]
        public string Localizacao { get; set; }

        // Quantidade de vagas disponíveis
        public int Quantidade { get; set; }

        // Indica se a vaga está ativa
        public bool Ativa { get; set; } = true;

        public DateTime Criado { get; set; }
    }
}
