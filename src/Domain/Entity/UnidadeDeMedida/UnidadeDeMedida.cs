using Domain.Exceptions;
using Domain.SeedWork;

namespace Domain.Entity.UnidadeDeMedida;

public class UnidadeDeMedida : AggregateRoot
{
    private const int AbreviacaoMaxLength = 6;
    private const int DescricaoMaxLength = 50;

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
        ValidateAbreviacao();
        ValidateDescricao();
    }

    private void ValidateAbreviacao()
    {
        if (string.IsNullOrWhiteSpace(Abreviacao))
        {
            throw new EntityValidationException($"{nameof(Abreviacao)} não pode ser vazia ou espaços em branco");
        }

        if (Abreviacao.Length > AbreviacaoMaxLength)
        {
            throw new EntityValidationException(
                $"{nameof(Abreviacao)} deve ter no máximo {AbreviacaoMaxLength} caracteres");
        }
    }

    private void ValidateDescricao()
    {
        if (string.IsNullOrWhiteSpace(Descricao))
        {
            throw new EntityValidationException($"{nameof(Descricao)} não pode ser vazia ou espaços em branco");
        }

        if (Descricao.Length > DescricaoMaxLength)
        {
            throw new EntityValidationException(
                $"{nameof(Descricao)} deve ter no máximo {DescricaoMaxLength} caracteres");
        }
    }
}