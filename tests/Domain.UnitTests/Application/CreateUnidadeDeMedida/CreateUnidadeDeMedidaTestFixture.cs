using Application.Interfaces;
using Domain.Repository;
using Moq;
using UnitTests.Common;
using DomainEntity = Domain.Entity.UnidadeDeMedida;

namespace UnitTests.Application.CreateUnidadeDeMedida;

[CollectionDefinition(nameof(CreateUnidadeDeMedidaTestFixture))]
public class CreateUnidadeDeMedidaTestFixtureCollection
    : ICollectionFixture<CreateUnidadeDeMedidaTestFixture>
{
}

public class CreateUnidadeDeMedidaTestFixture
    : BaseFixture
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

    public Mock<IUnidadeDeMedidaRepository> GetUnidadeDeMedidaRepositoryMock() => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

}
