using DEPI.Application.DTOs.Contracts;
using MediatR;
namespace DEPI.Application.UseCases.Contracts.CompleteMilestone;
public record CompleteMilestoneCommand(Guid MilestoneId, Guid RequesterId, string? Deliverables = null) : IRequest<MilestoneResponse>;