namespace Domain.Entity.UnidadeDeMedida;

public class UnidadeDeMedida
{
    public Guid Id { get; private set; }
    public string Abreviacao { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }

    public UnidadeDeMedida(string abreviacao, string descricao)
    {
        Id = Guid.NewGuid();
        Abreviacao = abreviacao;
        Descricao = descricao;
        Ativo = true;
        CriadoEm = DateTime.Now;
    }
}