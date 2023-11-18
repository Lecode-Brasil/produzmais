namespace Application.UseCases.UnidadeDeMedida.CreateUnidadeDeMedida;

public record CreateUnidadeDeMedidaInput(
    string Abreviacao,
    string Descricao,
    bool Ativo)
{
}
