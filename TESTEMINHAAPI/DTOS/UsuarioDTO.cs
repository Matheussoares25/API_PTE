using System.ComponentModel.DataAnnotations;
using TESTEMINHAAPI.Models;

namespace TESTEMINHAAPI.DTOS
{
    public class UsuarioDTO
    {
        public int id { get; set; }

        [MaxLength(120)]
        public string email { get; set; }

        public int ativo { get; set; }
   
        public string nome { get; set; }

        public int acesso { get; set; }

    }
}
