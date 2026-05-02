// Contracts/CreateContract/CreateContractCommandHandler.cs
using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Contracts;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Contracts;
namespace DEPI.Application.UseCases.Contracts.CreateContract;
public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, ContractResponse>
{
    private readonly IContractRepository _contractRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    public CreateContractCommandHandler(IContractRepository contractRepository, IProjectRepository projectRepository, IMapper mapper) { _contractRepository = contractRepository; _projectRepository = projectRepository; _mapper = mapper; }
    public async Task<ContractResponse> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Request.ProjectId) ?? throw new KeyNotFoundException(Errors.NotFound("Project"));
        if (project.OwnerId != request.ClientId) throw new UnauthorizedAccessException(Errors.Forbidden());
        if (project.AssignedFreelancerId == null) throw new InvalidOperationException("Freelancer not assigned to this project");
        var existingContract = await _contractRepository.GetByProjectAsync(request.Request.ProjectId);
        if (existingContract != null) throw new InvalidOperationException(Errors.AlreadyExists("Contract"));
        var contract = Contract.Create(request.Request.ProjectId, request.ClientId, project.AssignedFreelancerId.Value, request.Request.TotalAmount);
        await _contractRepository.AddAsync(contract);
        return _mapper.Map<ContractResponse>(contract);
    }
}