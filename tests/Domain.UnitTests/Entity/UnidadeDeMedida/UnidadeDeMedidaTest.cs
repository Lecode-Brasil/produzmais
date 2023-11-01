namespace Domain.UnitTests.Entity.UnidadeDeMedida;

public class UnidadeDeMedidaTest
{
    [Fact(DisplayName = nameof(InstanciaComAbreviacaoDescricao))]
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

    [Theory(DisplayName = nameof(InstanciaComAbreviacaoDescricaoAtivo))]
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
}