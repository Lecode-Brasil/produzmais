namespace Domain.UnitTests.Entity.UnidadeDeMedida;

public class UnidadeDeMedidaTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    public void Instantiate()
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
}