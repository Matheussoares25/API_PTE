using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace TESTEMINHAAPI.Models
{
    public class Modulos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(125)]
        public string nome { get; set; }

        // Relacionamento com Treinamentos (curso)
        public int treinamento_id { get; set; }
        [ForeignKey("treinamento_id")]
        public Treinamentos treinamento { get; set; }
    }
}
