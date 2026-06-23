using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Modulos
    {
        public int id { get; set; }

        [MaxLength(125)]
        public string nome { get; set; }

        // Relacionamento com Treinamentos (curso)
        public int treinamento_id { get; set; }
        public Treinamentos treinamento { get; set; }
    }
}
