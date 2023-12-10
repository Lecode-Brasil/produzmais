using Domain.Entity.UnidadeDeMedida;
using FluentAssertions;
using Moq;
using UseCases = Application.UseCases.UnidadeDeMedida.CreateUnidadeDeMedida;

namespace UnitTests.Application.CreateUnidadeDeMedida;

[Collection(nameof(CreateUnidadeDeMedidaTestFixture))]
public class CreateUnidadeDeMedidaTest
{
    private readonly CreateUnidadeDeMedidaTestFixture _fixture;

    public CreateUnidadeDeMedidaTest(CreateUnidadeDeMedidaTestFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task CreateUnidadeDeMedida()
    {
        var repositoryMock = _fixture.GetUnidadeDeMedidaRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var useCase = new UseCases.CreateUnidadeDeMedida(
            repositoryMock.Object,
            unitOfWorkMock.Object);
        UseCases.CreateUnidadeDeMedidaInput input = new(
            Abreviacao: _fixture.GetValidUnidadeDeMedidaAbreviacao(),
            Descricao: _fixture.GetValidUnidadeDeMedidaDescricao(),
            Ativo: true);

        UseCases.CreateUnidadeDeMedidaOutput output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<UnidadeDeMedida>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once);
        unitOfWorkMock.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);
        output.Should().NotBeNull();
        output.Abreviacao.Should().Be(input.Abreviacao);
        output.Descricao.Should().Be(input.Descricao);
        output.Ativo.Should().Be(input.Ativo);
    }
}
