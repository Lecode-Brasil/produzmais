namespace Domain.Entity.UnidadeDeMedida;

public class UnidadeDeMedida
{
    public string Abreviacao { get; }
    public string Descricao { get; }

    public UnidadeDeMedida(string abreviacao, string descricao)
    {
        Abreviacao = abreviacao;
        Descricao = descricao;
    }
}