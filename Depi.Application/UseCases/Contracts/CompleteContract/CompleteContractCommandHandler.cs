using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Contracts;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Contracts.CompleteContract;

public record CompleteContractCommand(Guid ContractId, Guid UserId) : IRequest<ContractResponse>;

public class CompleteContractCommandHandler : IRequestHandler<CompleteContractCommand, ContractResponse>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;

    public CompleteContractCommandHandler(IContractRepository contractRepository, IMapper mapper)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
    }

    public async Task<ContractResponse> Handle(CompleteContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Contract"));

        if (contract.ClientId != request.UserId)
            throw new UnauthorizedAccessException(Errors.Forbidden());

        contract.Complete();
        await _contractRepository.UpdateAsync(contract, cancellationToken);

        return _mapper.Map<ContractResponse>(contract);
    }
}
