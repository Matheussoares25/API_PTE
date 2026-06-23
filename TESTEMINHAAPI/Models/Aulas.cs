using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Aulas
    {
        public int Id { get; set; }

        [MaxLength(125)]
        public string Nome { get; set; }

        public string Conteudo { get; set; }

        // Relacionamento com Modulos
        public int ModuloId { get; set; }
        public Modulos Modulo { get; set; }

        public DateTime Criado { get; set; }
    }
}
