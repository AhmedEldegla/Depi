// Contracts/CreateContract/CreateContractCommand.cs
using DEPI.Application.DTOs.Contracts;
using MediatR;
namespace DEPI.Application.UseCases.Contracts.CreateContract;
public record CreateContractCommand(Guid ClientId, CreateContractRequest Request) : IRequest<ContractResponse>;