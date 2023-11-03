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
        var unidadeDeMedidaValida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        var dataAntes = DateTime.Now;

        DomainEntity.UnidadeDeMedida unidadeDeMedida =
            new(unidadeDeMedidaValida.Abreviacao, unidadeDeMedidaValida.Descricao);

        var dataDepois = DateTime.Now.AddSeconds(1);

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
        var unidadeDeMedidaValida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        var dataAntes = DateTime.Now;

        DomainEntity.UnidadeDeMedida unidadeDeMedida =
            new(unidadeDeMedidaValida.Abreviacao, unidadeDeMedidaValida.Descricao, ativo);

        var dataDepois = DateTime.Now.AddSeconds(1);

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
            new DomainEntity.UnidadeDeMedida(
                abreviacao!,
                descricao: _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedidaDescricao());

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao não pode ser nulo, vazio ou apenas espaços");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void ExceptionSeDescricaoVazia(string? descricao)
    {
        Action action = () =>
            new DomainEntity.UnidadeDeMedida(
                abreviacao: _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedidaAbreviacao(),
                descricao!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao não pode ser nulo, vazio ou apenas espaços");
    }

    [Fact]
    public void ExceptionSeAbreviacaoMaiorQue6Caracteres()
    {
        var abreviacaoInvalida = _unidadeDeMedidaTestFixture.Faker.Lorem.Letter(7);

        Action action = () =>
            new DomainEntity.UnidadeDeMedida(
                abreviacaoInvalida,
                descricao: _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedidaDescricao());

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao deve ter no máximo 6 caracteres");
    }

    [Fact]
    public void ExceptionSeDescricaoMaiorQue50Caracteres()
    {
        var descricaoInvalida = _unidadeDeMedidaTestFixture.Faker.Lorem.Letter(51);

        Action action = () =>
            new DomainEntity.UnidadeDeMedida(
                _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedidaAbreviacao(),
                descricaoInvalida);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter no máximo 50 caracteres");
    }

    [Fact]
    public void Ativar()
    {
        var unidadeDeMedidaValida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        DomainEntity.UnidadeDeMedida unidadeDeMedida =
            new(unidadeDeMedidaValida.Abreviacao, unidadeDeMedidaValida.Descricao, ativo: false);
        unidadeDeMedida.Ativar();

        unidadeDeMedida.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Inativar()
    {
        var unidadeDeMedidaValida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        DomainEntity.UnidadeDeMedida unidadeDeMedida =
            new(unidadeDeMedidaValida.Abreviacao, unidadeDeMedidaValida.Descricao, ativo: true);
        unidadeDeMedida.Inativar();

        unidadeDeMedida.Ativo.Should().BeFalse();
    }

    [Fact]
    public void Update()
    {
        var unidadeDeMedida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        var unidadeDeMedidaComNovosValores = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();

        unidadeDeMedida.Update(unidadeDeMedidaComNovosValores.Abreviacao, unidadeDeMedidaComNovosValores.Descricao);

        unidadeDeMedida.Abreviacao.Should().Be(unidadeDeMedidaComNovosValores.Abreviacao);
        unidadeDeMedida.Descricao.Should().Be(unidadeDeMedidaComNovosValores.Descricao);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void UpdateExceptionSeAbreviacaoVazia(string? abreviacao)
    {
        var unidadeDeMedida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();

        Action action = () => unidadeDeMedida.Update(
            abreviacao!,
            descricao: _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedidaDescricao());

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao não pode ser nulo, vazio ou apenas espaços");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public void UpdateExceptionSeDescricaoVazia(string? descricao)
    {
        var unidadeDeMedida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();
        Action action = () => unidadeDeMedida.Update(
            abreviacao: _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedidaAbreviacao(),
            descricao!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao não pode ser nulo, vazio ou apenas espaços");
    }

    [Fact]
    public void UpdateExceptionSeAbreviacaoMaiorQue6Caracteres()
    {
        var abreviacaoInvalida = _unidadeDeMedidaTestFixture.Faker.Lorem.Letter(7);
        var unidadeDeMedida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();

        Action action = () => unidadeDeMedida.Update(
            abreviacaoInvalida,
            descricao: _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedidaDescricao());

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao deve ter no máximo 6 caracteres");
    }

    [Fact]
    public void UpdateExceptionSeDescricaoMaiorQue50Caracteres()
    {
        var descricaoInvalida = _unidadeDeMedidaTestFixture.Faker.Lorem.Letter(51);
        var unidadeDeMedida = _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedida();

        Action action = () => unidadeDeMedida.Update(
            _unidadeDeMedidaTestFixture.GetValidUnidadeDeMedidaAbreviacao(),
            descricaoInvalida);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter no máximo 50 caracteres");
    }
}
