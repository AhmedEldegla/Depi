using AutoMapper;
using DEPI.Application.DTOs.Contracts;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Contracts.GetMyContracts;

public record GetMyContractsQuery(Guid UserId) : IRequest<List<ContractResponse>>;

public class GetMyContractsQueryHandler : IRequestHandler<GetMyContractsQuery, List<ContractResponse>>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;

    public GetMyContractsQueryHandler(IContractRepository contractRepository, IMapper mapper)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
    }

    public async Task<List<ContractResponse>> Handle(GetMyContractsQuery request, CancellationToken cancellationToken)
    {
        var clientContracts = await _contractRepository.GetByClientAsync(request.UserId);
        var freelancerContracts = await _contractRepository.GetByFreelancerAsync(request.UserId);

        var allContracts = clientContracts
            .Union(freelancerContracts)
            .DistinctBy(c => c.Id)
            .OrderByDescending(c => c.CreatedAt);

        return allContracts.Select(c => _mapper.Map<ContractResponse>(c)).ToList();
    }
}
