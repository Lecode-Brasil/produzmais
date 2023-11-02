using Domain.Exceptions;

namespace Domain.UnitTests.Entity.UnidadeDeMedida;

public class UnidadeDeMedidaTest
{
    [Fact]
    public void InstanciaComAbreviacaoDescricao()
    {
        var validData = new
        {
            Abreviacao = "PC",
            Descricao = "Peça",
        };
        var dataAntes = DateTime.Now;

        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida = new(validData.Abreviacao, validData.Descricao);

        var dataDepois = DateTime.Now;

        Assert.NotNull(unidadeDeMedida);
        Assert.Equal(validData.Abreviacao, unidadeDeMedida.Abreviacao);
        Assert.Equal(validData.Descricao, unidadeDeMedida.Descricao);
        Assert.NotEqual(default(Guid), unidadeDeMedida.Id);
        Assert.NotEqual(default(DateTime), unidadeDeMedida.CriadoEm);
        Assert.InRange(unidadeDeMedida.CriadoEm, dataAntes, dataDepois);
        Assert.True(unidadeDeMedida.Ativo);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void InstanciaComAbreviacaoDescricaoAtivo(bool ativo)
    {
        var validData = new
        {
            Abreviacao = "PC",
            Descricao = "Peça",
        };
        var dataAntes = DateTime.Now;

        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida =
            new(validData.Abreviacao, validData.Descricao, ativo);

        var dataDepois = DateTime.Now;

        Assert.NotNull(unidadeDeMedida);
        Assert.Equal(validData.Abreviacao, unidadeDeMedida.Abreviacao);
        Assert.Equal(validData.Descricao, unidadeDeMedida.Descricao);
        Assert.NotEqual(default(Guid), unidadeDeMedida.Id);
        Assert.NotEqual(default(DateTime), unidadeDeMedida.CriadoEm);
        Assert.InRange(unidadeDeMedida.CriadoEm, dataAntes, dataDepois);
        Assert.Equal(unidadeDeMedida.Ativo, ativo);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ExceptionSeAbreviacaoVazia(string? abreviacao)
    {
        Action action = () =>
            new Domain.Entity.UnidadeDeMedida.UnidadeDeMedida(abreviacao!, descricao:"Descrição válida");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Abreviacao não pode ser vazia ou espaços em branco", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ExceptionSeDescricaoVazia(string? descricao)
    {
        Action action = () =>
            new Domain.Entity.UnidadeDeMedida.UnidadeDeMedida(abreviacao:"VALID", descricao!);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Descricao não pode ser vazia ou espaços em branco", exception.Message);
    }

    // Abrevição: mínimo 1, máximo 6
    // Descrição: mínimo 1, máximo 50
}