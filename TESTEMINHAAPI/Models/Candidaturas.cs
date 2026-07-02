using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Candidaturas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        // Relacionamento com Vagas
        public int vaga_id { get; set; }
        [ForeignKey("vaga_id")]
        public Vagas? vaga { get; set; }

        // Opcional: relacionamento com Usuario (se candidato for um usuário do sistema)
        public int? usuario_id { get; set; }
        [ForeignKey("usuario_id")]
        public Usuario? usuario { get; set; }

        [MaxLength(150)]
        public string nome { get; set; }

        [MaxLength(150)]
        public string? email { get; set; }

        [MaxLength(50)]
        public string telefone { get; set; }

        // URL do currículo ou anexo
        [MaxLength(500)]
        public string curriculo_url { get; set; }

        // Status da candidatura: "pendente", "aprovado", "rejeitado" etc.
        [MaxLength(50)]
        public string status { get; set; } = "pendente";

        public DateTime criado { get; set; }
    }
}
