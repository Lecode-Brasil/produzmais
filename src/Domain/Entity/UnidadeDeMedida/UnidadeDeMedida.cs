using Domain.Exceptions;

namespace Domain.Entity.UnidadeDeMedida;

public class UnidadeDeMedida
{
    public Guid Id { get; private set; }
    public string Abreviacao { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }

    public UnidadeDeMedida(string abreviacao, string descricao, bool ativo = true)
    {
        Id = Guid.NewGuid();
        Abreviacao = abreviacao;
        Descricao = descricao;
        Ativo = ativo;
        CriadoEm = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Abreviacao))
        {
            throw new EntityValidationException($"{nameof(Abreviacao)} não pode ser vazio ou espaços em branco");
        }

        if (string.IsNullOrWhiteSpace(Descricao))
        {
            throw new EntityValidationException($"{nameof(Descricao)} não pode ser vazio ou espaços em branco");
        }
    }
}