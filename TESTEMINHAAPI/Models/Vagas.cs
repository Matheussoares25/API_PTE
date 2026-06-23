using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Vagas
    {
        public int id { get; set; }

        [MaxLength(200)]
        public string titulo { get; set; }

        public string descricao { get; set; }

        [MaxLength(150)]
        public string localizacao { get; set; }

        // Quantidade de vagas disponíveis
        public int quantidade { get; set; }

        // Indica se a vaga está ativa
        public bool ativa { get; set; } = true;

        public DateTime criado { get; set; }
    }
}
