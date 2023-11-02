using DomainEntity = Domain.Entity.UnidadeDeMedida;

namespace Domain.UnitTests.Entity.UnidadeDeMedida;
public class UnidadeDeMedidaTestFixture
{
    public static DomainEntity.UnidadeDeMedida GetValidUnidadeDeMedida() =>  new ("PC", "Peça");
}

[CollectionDefinition(nameof(UnidadeDeMedidaTestFixture))]
public class UnidadeDeMedidaTestFixtureCollection
    : ICollectionFixture<UnidadeDeMedidaTestFixture>
{
}