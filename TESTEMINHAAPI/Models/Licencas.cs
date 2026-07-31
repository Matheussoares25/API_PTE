using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TESTEMINHAAPI.Models
{

    public class Licencas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(200)]
        // valor do token/código que será fornecido ao cliente
        public string? token { get; set; }

        // comprador/opcional: quando a licença for vendida pode apontar para o usuário
        public int? usuario_id { get; set; }
        [ForeignKey("usuario_id")]
        public Usuario? usuario { get; set; }

        // data de emissão/criação do token
        public DateTime criado_em { get; set; }

        // data de expiração da licença
        public DateTime validade_em { get; set; }

        // indica se a licença ainda está ativa (não revogada)
        public bool ativo { get; set; } = true;
        // preço cobrado pela licença (opcional, para registro)
        public decimal preco { get; set; }
    }
}
