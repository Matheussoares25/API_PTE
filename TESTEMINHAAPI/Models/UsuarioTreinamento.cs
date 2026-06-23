using TESTEMINHAAPI.Models;

    public class UsuarioTreinamento
{
    public int id { get; set; }

    public int usuario_id { get; set; }
    public Usuario usuario { get; set; }

    public int treinamento_id { get; set; }
    public Treinamentos treinamento { get; set; }
}
