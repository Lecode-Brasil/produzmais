using Domain.Exceptions;
using FluentAssertions;
using DomainEntity = Domain.Entity.UnidadeDeMedida;

namespace Domain.UnitTests.Entity.UnidadeDeMedida;

[Collection(nameof(UnidadeDeMedidaTestFixture))]
public class UnidadeDeMedidaTest(UnidadeDeMedidaTestFixture unidadeDeMedidaTestFixture)
{
    private readonly UnidadeDeMedidaTestFixture _unidadeDeMedidaTestFixture = unidadeDeMedidaTestFixture;

    [Fact]
    public void InstanciaComAbreviacaoDescricao()
    {
        var unidadeDeMedidaValida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        var dataAntes = DateTime.Now;

        DomainEntity.UnidadeDeMedida unidadeDeMedida = new(unidadeDeMedidaValida.Abreviacao, unidadeDeMedidaValida.Descricao);

        var dataDepois = DateTime.Now;

        unidadeDeMedida.Should().NotBeNull();
        unidadeDeMedida.Abreviacao.Should().Be(unidadeDeMedidaValida.Abreviacao);
        unidadeDeMedida.Descricao.Should().Be(unidadeDeMedidaValida.Descricao);
        unidadeDeMedida.Id.Should().NotBe(default(Guid));
        unidadeDeMedida.CriadoEm.Should().NotBe(default);
        unidadeDeMedida.CriadoEm.Should().BeAfter(dataAntes);
        unidadeDeMedida.CriadoEm.Should().BeBefore(dataDepois);
        unidadeDeMedida.Ativo.Should().BeTrue();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void InstanciaComAbreviacaoDescricaoAtivo(bool ativo)
    {
        var unidadeDeMedidaValida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        var dataAntes = DateTime.Now;

        DomainEntity.UnidadeDeMedida unidadeDeMedida =
            new(unidadeDeMedidaValida.Abreviacao, unidadeDeMedidaValida.Descricao, ativo);

        var dataDepois = DateTime.Now;

        unidadeDeMedida.Should().NotBeNull();
        unidadeDeMedida.Abreviacao.Should().Be(unidadeDeMedidaValida.Abreviacao);
        unidadeDeMedida.Descricao.Should().Be(unidadeDeMedidaValida.Descricao);
        unidadeDeMedida.Id.Should().NotBe(default(Guid));
        unidadeDeMedida.CriadoEm.Should().NotBe(default);
        unidadeDeMedida.CriadoEm.Should().BeAfter(dataAntes);
        unidadeDeMedida.CriadoEm.Should().BeBefore(dataDepois);
        unidadeDeMedida.Ativo.Should().Be(ativo);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ExceptionSeAbreviacaoVazia(string? abreviacao)
    {
        Action action = () =>
            new DomainEntity.UnidadeDeMedida(abreviacao!, descricao: "Descrição válida");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao não pode ser vazia ou espaços em branco");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ExceptionSeDescricaoVazia(string? descricao)
    {
        Action action = () =>
            new DomainEntity.UnidadeDeMedida(abreviacao:"VALID", descricao!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao não pode ser vazia ou espaços em branco");
    }

    [Fact]
    public void ExceptionSeAbreviacaoMaiorQue6Caracteres()
    {
        var abreviacaoInvalida = new string(c: 'A', count: 7);

        Action action = () =>
            new DomainEntity.UnidadeDeMedida(abreviacaoInvalida, descricao:"Descrição válida");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao deve ter no máximo 6 caracteres");
    }

    [Fact]
    public void ExceptionSeDescricaoMaiorQue50Caracteres()
    {
        var descricaoInvalida = new string(c: 'A', count: 51);

        Action action = () =>
            new DomainEntity.UnidadeDeMedida("PC", descricaoInvalida);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter no máximo 50 caracteres");
    }

    [Fact]
    public void Ativar()
    {
        var unidadeDeMedidaValida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        DomainEntity.UnidadeDeMedida unidadeDeMedida =
            new(unidadeDeMedidaValida.Abreviacao, unidadeDeMedidaValida.Descricao, ativo: false);
        unidadeDeMedida.Ativar();

        unidadeDeMedida.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Inativar()
    {
        var unidadeDeMedidaValida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        DomainEntity.UnidadeDeMedida unidadeDeMedida =
            new(unidadeDeMedidaValida.Abreviacao, unidadeDeMedidaValida.Descricao, ativo: true);
        unidadeDeMedida.Inativar();

        unidadeDeMedida.Ativo.Should().BeFalse();
    }

    [Fact]
    public void Update()
    {
        var unidadeDeMedida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        var novosValores = new { Abreviacao = "UN", Descricao = "Unidade" };

        unidadeDeMedida.Update(novosValores.Abreviacao, novosValores.Descricao);

        unidadeDeMedida.Abreviacao.Should().Be(novosValores.Abreviacao);
        unidadeDeMedida.Descricao.Should().Be(novosValores.Descricao);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void UpdateExceptionSeAbreviacaoVazia(string? abreviacao)
    {
        var unidadeDeMedida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();

        Action action = () => unidadeDeMedida.Update(abreviacao!, descricao: "Descrição válida");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao não pode ser vazia ou espaços em branco");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void UpdateExceptionSeDescricaoVazia(string? descricao)
    {
        var unidadeDeMedida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        Action action = () => unidadeDeMedida.Update(abreviacao:"VALID", descricao!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao não pode ser vazia ou espaços em branco");
    }

    [Fact]
    public void UpdateExceptionSeAbreviacaoMaiorQue6Caracteres()
    {
        var abreviacaoInvalida = new string(c: 'A', count: 7);
        var unidadeDeMedida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();

        Action action = () => unidadeDeMedida.Update(abreviacaoInvalida, descricao:"Descrição válida");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao deve ter no máximo 6 caracteres");
    }

    [Fact]
    public void UpdateExceptionSeDescricaoMaiorQue50Caracteres()
    {
        var descricaoInvalida = new string(c: 'A', count: 51);
        var unidadeDeMedida = UnidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();

        Action action = () => unidadeDeMedida.Update("PC", descricaoInvalida);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter no máximo 50 caracteres");
    }
}