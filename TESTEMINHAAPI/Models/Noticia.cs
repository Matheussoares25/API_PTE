namespace TESTEMINHAAPI.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        
        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public DateTime data_noticia { get; set; }

        public int Vaga { get; set; }
    }
}
