using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TESTEMINHAAPI.Models;

    public class UsuarioTreinamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

    public int usuario_id { get; set; }
    public Usuario usuario { get; set; }

    public int treinamento_id { get; set; }
    public Treinamentos treinamento { get; set; }
}
