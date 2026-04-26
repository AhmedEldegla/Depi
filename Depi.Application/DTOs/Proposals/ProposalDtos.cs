using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Proposals;

public record SubmitProposalRequest(
    Guid ProjectId,
    decimal ProposedAmount,
    int EstimatedDays,
    string CoverLetter
);

public record ProposalResponse(
    Guid Id,
    Guid ProjectId,
    string ProjectTitle,
    Guid FreelancerId,
    string FreelancerName,
    decimal ProposedAmount,
    int EstimatedDays,
    string CoverLetter,
    ProposalStatus Status,
    string? RejectionReason,
    DateTime? AcceptedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record ProposalFilterRequest(
    ProposalStatus? Status,
    Guid? ProjectId,
    Guid? FreelancerId,
    int Page = 1,
    int PageSize = 20
);