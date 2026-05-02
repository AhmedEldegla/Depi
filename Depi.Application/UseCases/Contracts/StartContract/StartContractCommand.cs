// Contracts/StartContract/StartContractCommand.cs
using DEPI.Application.DTOs.Contracts;
using MediatR;
namespace DEPI.Application.UseCases.Contracts.StartContract;
public record StartContractCommand(Guid ContractId, Guid RequesterId) : IRequest<ContractResponse>;