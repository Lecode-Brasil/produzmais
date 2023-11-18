using Application.Interfaces;
using Domain.Entity.UnidadeDeMedida;
using Domain.Repository;
using FluentAssertions;
using Moq;
using UseCases = Application.UseCases.UnidadeDeMedida.CreateUnidadeDeMedida;

namespace UnitTests.Application.CreateUnidadeDeMedida;

public class CreateUnidadeDeMedidaTest
{
    [Fact]
    public async Task CreateUnidadeDeMedida()
    {
        var repositoryMock = new Mock<IUnidadeDeMedidaRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var useCase = new UseCases.CreateUnidadeDeMedida(
            repositoryMock.Object,
            unitOfWorkMock.Object);
        UseCases.CreateUnidadeDeMedidaInput input = new(
            Abreviacao: "foo",
            Descricao: "bar",
            Ativo: true);

        UseCases.CreateUnidadeDeMedidaOutput output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<UnidadeDeMedida>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once);
        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once);
        output.Should().NotBeNull();
        output.Abreviacao.Should().Be(input.Abreviacao);
        output.Descricao.Should().Be(input.Descricao);
        output.Ativo.Should().Be(input.Ativo);
    }
}
