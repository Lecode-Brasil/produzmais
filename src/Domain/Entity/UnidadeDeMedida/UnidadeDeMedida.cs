using Domain.Exceptions;

namespace Domain.Entity.UnidadeDeMedida;

public class UnidadeDeMedida
{
    private const int AbreviacaoMaxLength = 6;

    public Guid Id { get; private set; }
    public string Abreviacao { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime CriadoEm { get; private set; }

    public UnidadeDeMedida(string abreviacao, string descricao) : this(abreviacao, descricao, ativo: true)
    {
    }

    public UnidadeDeMedida(string abreviacao, string descricao, bool ativo)
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
        ValidateAbreviacao();

        if (string.IsNullOrWhiteSpace(Descricao))
        {
            throw new EntityValidationException($"{nameof(Descricao)} não pode ser vazia ou espaços em branco");
        }
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
}