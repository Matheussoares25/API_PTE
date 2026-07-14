using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TESTEMINHAAPI.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(120)]
        public string email { get; set; }

        public string? senha { get; set; }

        public int ativo { get; set; }

        // Relação: usuário pode possuir licenças (tokens) adquiridas
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Licencas> licencas { get; set; } = new List<Licencas>();

        public int tipo { get; set; }

        public string nome { get; set; }

        public int acesso { get; set; }
        public string? telefone { get; set; }

        public DateTime data_criacao { get; set; }

    }
}

