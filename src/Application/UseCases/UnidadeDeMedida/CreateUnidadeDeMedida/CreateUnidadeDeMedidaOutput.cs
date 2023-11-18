namespace Application.UseCases.UnidadeDeMedida.CreateUnidadeDeMedida;

public record CreateUnidadeDeMedidaOutput(
    Guid Id,
    string Abreviacao,
    string Descricao,
    bool Ativo,
    DateTime CriadoEm)
{
}
