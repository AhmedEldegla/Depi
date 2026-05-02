using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Proposals;

public record SubmitProposalRequest(
    Guid ProjectId,
    decimal ProposedAmount,
    int EstimatedDays,
    string CoverLetter
);

public class ProposalResponse
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public Guid FreelancerId { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
    public decimal ProposedAmount { get; set; }
    public int EstimatedDays { get; set; }
    public string CoverLetter { get; set; } = string.Empty;
    public ProposalStatus Status { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public record ProposalFilterRequest(
    ProposalStatus? Status,
    Guid? ProjectId,
    Guid? FreelancerId,
    int Page = 1,
    int PageSize = 20
);