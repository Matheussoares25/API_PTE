using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Modulos
    {
        public int Id { get; set; }

        [MaxLength(125)]
        public string Nome { get; set; }

        // Relacionamento com Treinamentos (curso)
        public int TreinamentoId { get; set; }
        public Treinamentos Treinamento { get; set; }
    }
}
