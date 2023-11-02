using Domain.UnitTests.Common;
using DomainEntity = Domain.Entity.UnidadeDeMedida;

namespace Domain.UnitTests.Entity.UnidadeDeMedida;
public class UnidadeDeMedidaTestFixture : BaseFixture
{
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
        new (
            GetValidUnidadeDeMedidaAbreviacao(),
            GetValidUnidadeDeMedidaDescricao());
}

[CollectionDefinition(nameof(UnidadeDeMedidaTestFixture))]
public class UnidadeDeMedidaTestFixtureCollection
    : ICollectionFixture<UnidadeDeMedidaTestFixture>
{
}