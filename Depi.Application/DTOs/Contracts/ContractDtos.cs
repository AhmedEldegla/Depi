using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Contracts;

public record CreateContractRequest(
    Guid ProjectId,
    decimal TotalAmount
);

public record CreateMilestoneRequest(
    Guid ContractId,
    string Title,
    string Description,
    decimal Amount,
    DateTime? DueDate
);

public class ContractResponse
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public Guid FreelancerId { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public ContractStatus Status { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public List<MilestoneResponse>? Milestones { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class MilestoneResponse
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public MilestoneStatus Status { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Deliverables { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}