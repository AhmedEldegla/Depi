// Contracts/AddMilestone/AddMilestoneCommand.cs
using DEPI.Application.DTOs.Contracts;
using MediatR;
namespace DEPI.Application.UseCases.Contracts.AddMilestone;
public record AddMilestoneCommand(Guid RequesterId, CreateMilestoneRequest Request) : IRequest<MilestoneResponse>;