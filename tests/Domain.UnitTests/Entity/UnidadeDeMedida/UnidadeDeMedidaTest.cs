using Domain.Exceptions;
using FluentAssertions;

namespace Domain.UnitTests.Entity.UnidadeDeMedida;

public class UnidadeDeMedidaTest
{
    [Fact]
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

        unidadeDeMedida.Should().NotBeNull();
        unidadeDeMedida.Abreviacao.Should().Be(validData.Abreviacao);
        unidadeDeMedida.Descricao.Should().Be(validData.Descricao);
        unidadeDeMedida.Id.Should().NotBe(default(Guid));
        unidadeDeMedida.CriadoEm.Should().NotBe(default(DateTime));
        unidadeDeMedida.CriadoEm.Should().BeAfter(dataAntes);
        unidadeDeMedida.CriadoEm.Should().BeBefore(dataDepois);
        unidadeDeMedida.Ativo.Should().BeTrue();
    }

    [Theory]
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

        unidadeDeMedida.Should().NotBeNull();
        unidadeDeMedida.Abreviacao.Should().Be(validData.Abreviacao);
        unidadeDeMedida.Descricao.Should().Be(validData.Descricao);
        unidadeDeMedida.Id.Should().NotBe(default(Guid));
        unidadeDeMedida.CriadoEm.Should().NotBe(default(DateTime));
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
            new Domain.Entity.UnidadeDeMedida.UnidadeDeMedida(abreviacao!, descricao:"Descrição válida");

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
            new Domain.Entity.UnidadeDeMedida.UnidadeDeMedida(abreviacao:"VALID", descricao!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao não pode ser vazia ou espaços em branco");
    }

    [Fact]
    public void ExceptionSeAbreviacaoMaiorQue6Caracteres()
    {
        var abreviacaoInvalida = new string(c: 'A', count: 7);

        Action action = () =>
            new Domain.Entity.UnidadeDeMedida.UnidadeDeMedida(abreviacaoInvalida, descricao:"Descrição válida");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao deve ter no máximo 6 caracteres");
    }

    [Fact]
    public void ExceptionSeDescricaoMaiorQue50Caracteres()
    {
        var descricaoInvalida = new string(c: 'A', count: 51);

        Action action = () =>
            new Domain.Entity.UnidadeDeMedida.UnidadeDeMedida("PC", descricaoInvalida);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter no máximo 50 caracteres");
    }

    [Fact]
    public void Ativar()
    {
        var validData = new
        {
            Abreviacao = "PC",
            Descricao = "Peça",
        };
        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida =
            new(validData.Abreviacao, validData.Descricao, ativo: false);
        unidadeDeMedida.Ativar();

        unidadeDeMedida.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Inativar()
    {
        var validData = new
        {
            Abreviacao = "PC",
            Descricao = "Peça",
        };
        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida =
            new(validData.Abreviacao, validData.Descricao, ativo: true);
        unidadeDeMedida.Inativar();

        unidadeDeMedida.Ativo.Should().BeFalse();
    }

    [Fact]
    public void Update()
    {
        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida = new("PC", "Peça");
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
        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida = new("PC", "Peça");

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
        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida = new("PC", "Peça");
        Action action = () => unidadeDeMedida.Update(abreviacao:"VALID", descricao!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao não pode ser vazia ou espaços em branco");
    }

    [Fact]
    public void UpdateExceptionSeAbreviacaoMaiorQue6Caracteres()
    {
        var abreviacaoInvalida = new string(c: 'A', count: 7);
        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida = new("PC", "Peça");

        Action action = () => unidadeDeMedida.Update(abreviacaoInvalida, descricao:"Descrição válida");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Abreviacao deve ter no máximo 6 caracteres");
    }

    [Fact]
    public void UpdateExceptionSeDescricaoMaiorQue50Caracteres()
    {
        var descricaoInvalida = new string(c: 'A', count: 51);
        Domain.Entity.UnidadeDeMedida.UnidadeDeMedida unidadeDeMedida = new("PC", "Peça");

        Action action = () => unidadeDeMedida.Update("PC", descricaoInvalida);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Descricao deve ter no máximo 50 caracteres");
    }
}