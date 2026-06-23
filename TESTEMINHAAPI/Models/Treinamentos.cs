using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Treinamentos
    {
        public int id { get; set; }
        [MaxLength(125)]
        public string nome { get; set; }

        public DateTime criado { get; set; }
    }
}