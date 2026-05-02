// Contracts/StartContract/StartContractCommandHandler.cs
using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Contracts;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Contracts;
namespace DEPI.Application.UseCases.Contracts.StartContract;
public class StartContractCommandHandler : IRequestHandler<StartContractCommand, ContractResponse>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    public StartContractCommandHandler(IContractRepository contractRepository, IMapper mapper) { _contractRepository = contractRepository; _mapper = mapper; }
    public async Task<ContractResponse> Handle(StartContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId) ?? throw new KeyNotFoundException(Errors.NotFound("Contract"));
        if (contract.ClientId != request.RequesterId && contract.FreelancerId != request.RequesterId) throw new UnauthorizedAccessException(Errors.Forbidden());
        contract.Start();
        await _contractRepository.UpdateAsync(contract);
        return _mapper.Map<ContractResponse>(contract);
    }
}