
using Application.Interfaces;
using DomainEntity = Domain.Entity.UnidadeDeMedida;
using Domain.Repository;

namespace Application.UseCases.UnidadeDeMedida.CreateUnidadeDeMedida;
public class CreateUnidadeDeMedida(IUnidadeDeMedidaRepository unidadeDeMedidaRepository, IUnitOfWork unitOfWork)
    : ICreateUnidadeDeMedida
{
    private readonly IUnidadeDeMedidaRepository _unidadeDeMedidaRepository = unidadeDeMedidaRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateUnidadeDeMedidaOutput> Handle(
        CreateUnidadeDeMedidaInput input,
        CancellationToken cancellationToken)
    {
        DomainEntity.UnidadeDeMedida unidadeDeMedida = new(input.Abreviacao, input.Descricao, input.Ativo);
        await _unidadeDeMedidaRepository.Insert(unidadeDeMedida, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return new CreateUnidadeDeMedidaOutput(
            unidadeDeMedida.Id,
            unidadeDeMedida.Abreviacao,
            unidadeDeMedida.Descricao,
            unidadeDeMedida.Ativo,
            unidadeDeMedida.CriadoEm);
    }
}
