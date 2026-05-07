using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Contracts;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Contracts.PauseContract;

public record PauseContractCommand(Guid ContractId, Guid UserId) : IRequest<ContractResponse>;

public class PauseContractCommandHandler : IRequestHandler<PauseContractCommand, ContractResponse>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;

    public PauseContractCommandHandler(IContractRepository contractRepository, IMapper mapper)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
    }

    public async Task<ContractResponse> Handle(PauseContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Contract"));

        if (contract.ClientId != request.UserId)
            throw new UnauthorizedAccessException(Errors.Forbidden());

        contract.Pause();
        await _contractRepository.UpdateAsync(contract, cancellationToken);

        return _mapper.Map<ContractResponse>(contract);
    }
}
