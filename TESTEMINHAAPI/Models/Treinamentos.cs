using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Treinamentos
    {
        public int Id { get; set; }
        [MaxLength(125)]
        public string Nome { get; set; }

        public DateTime Criado { get; set; }
    }
}