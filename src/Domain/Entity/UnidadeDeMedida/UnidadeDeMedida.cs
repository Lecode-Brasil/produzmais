using Domain.Exceptions;
using Domain.SeedWork;
using Domain.Validation;

namespace Domain.Entity.UnidadeDeMedida;

public class UnidadeDeMedida : AggregateRoot
{
    public static readonly int AbreviacaoMaxLength = 6;
    public static readonly int DescricaoMaxLength = 50;

    public string Abreviacao { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }

    public UnidadeDeMedida(string abreviacao, string descricao) : this(abreviacao, descricao, ativo: true)
    {
    }

    public UnidadeDeMedida(string abreviacao, string descricao, bool ativo)
    {
        Abreviacao = abreviacao;
        Descricao = descricao;
        Ativo = ativo;
        CriadoEm = DateTime.Now;

        Validate();
    }

    public void Ativar()
    {
        Ativo = true;
        Validate();
    }

    public void Inativar()
    {
        Ativo = false;
        Validate();
    }

    public void Update(string abreviacao, string descricao)
    {
        Abreviacao = abreviacao;
        Descricao = descricao;
        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrWhiteSpace(Abreviacao, nameof(Abreviacao));
        DomainValidation.MaxLength(Abreviacao, AbreviacaoMaxLength, nameof(Abreviacao));
        DomainValidation.NotNullOrWhiteSpace(Descricao, nameof(Descricao));
        DomainValidation.MaxLength(Descricao, DescricaoMaxLength, nameof(Descricao));
    }
}
