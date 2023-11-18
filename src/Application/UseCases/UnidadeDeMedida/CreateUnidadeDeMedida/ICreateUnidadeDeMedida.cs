namespace Application.UseCases.UnidadeDeMedida.CreateUnidadeDeMedida;
public interface ICreateUnidadeDeMedida
{
    public Task<CreateUnidadeDeMedidaOutput> Handle(
        CreateUnidadeDeMedidaInput input,
        CancellationToken cancellationToken);
}
