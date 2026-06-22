using TESTEMINHAAPI.Models;

public class UsuarioTreinamento
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public int TreinamentoId { get; set; }
    public Treinamentos Treinamento { get; set; }
}