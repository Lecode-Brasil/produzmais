using UnitTests.Common;
using DomainEntity = Domain.Entity.UnidadeDeMedida;

namespace UnitTests.Domain.Entity.UnidadeDeMedida;
public class UnidadeDeMedidaTestFixture : BaseFixture
{
    public UnidadeDeMedidaTestFixture()
    {
    }
    public string GetValidUnidadeDeMedidaAbreviacao()
    {
        var abreviacao = Faker.Commerce.Categories(1).First();
        return abreviacao[..Math.Min(DomainEntity.UnidadeDeMedida.AbreviacaoMaxLength, abreviacao.Length)];
    }
    public string GetValidUnidadeDeMedidaDescricao()
    {
        var descricao = Faker.Commerce.Product();
        return descricao[..Math.Min(DomainEntity.UnidadeDeMedida.DescricaoMaxLength, descricao.Length)];
    }

    public DomainEntity.UnidadeDeMedida GetValidUnidadeDeMedida() =>
        new(
            GetValidUnidadeDeMedidaAbreviacao(),
            GetValidUnidadeDeMedidaDescricao());
}

[CollectionDefinition(nameof(UnidadeDeMedidaTestFixture))]
public class UnidadeDeMedidaTestFixtureCollection
    : ICollectionFixture<UnidadeDeMedidaTestFixture>
{
}