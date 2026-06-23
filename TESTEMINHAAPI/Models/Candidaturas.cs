using System;
using System.ComponentModel.DataAnnotations;

namespace TESTEMINHAAPI.Models
{
    public class Candidaturas
    {
        public int Id { get; set; }

        // Relacionamento com Vagas
        public int VagaId { get; set; }
        public Vagas Vaga { get; set; }

        // Opcional: relacionamento com Usuario (se candidato for um usuário do sistema)
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [MaxLength(150)]
        public string Nome { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Telefone { get; set; }

        // URL do currículo ou anexo
        [MaxLength(500)]
        public string CurriculoUrl { get; set; }

        // Status da candidatura: "pendente", "aprovado", "rejeitado" etc.
        [MaxLength(50)]
        public string Status { get; set; } = "pendente";

        public DateTime Criado { get; set; }
    }
}
