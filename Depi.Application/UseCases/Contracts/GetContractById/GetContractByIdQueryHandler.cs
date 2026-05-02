using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Contracts;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Contracts.GetContractById;

public record GetContractByIdQuery(Guid ContractId) : IRequest<ContractResponse>;

public class GetContractByIdQueryHandler : IRequestHandler<GetContractByIdQuery, ContractResponse>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;

    public GetContractByIdQueryHandler(IContractRepository contractRepository, IMapper mapper)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
    }

    public async Task<ContractResponse> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.ContractId, includeMilestones: true)
            ?? throw new KeyNotFoundException(Errors.NotFound("Contract"));

        return _mapper.Map<ContractResponse>(contract);
    }
}
