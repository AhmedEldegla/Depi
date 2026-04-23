namespace DEPI.Domain.Entities.Proposals;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Enums;

public class Proposal : AuditableEntity
{
    public Guid ProjectId { get; private set; }
    public Guid FreelancerId { get; private set; }
    public decimal ProposedAmount { get; private set; }
    public int EstimatedDays { get; private set; }
    public string CoverLetter { get; private set; } = string.Empty;
    public ProposalStatus Status { get; private set; } = ProposalStatus.Pending;
    public string? RejectionReason { get; private set; }
    public DateTime? AcceptedAt { get; private set; }

    public virtual Project? Project { get; private set; }
    public virtual User? Freelancer { get; private set; }

    private Proposal() { }

    public static Proposal Create(
        Guid projectId,
        Guid freelancerId,
        decimal proposedAmount,
        int estimatedDays,
        string coverLetter)
    {
        if (string.IsNullOrWhiteSpace(coverLetter))
            throw new ArgumentException("خطاب التغطية مطلوب", nameof(coverLetter));

        return new Proposal
        {
            ProjectId = projectId,
            FreelancerId = freelancerId,
            ProposedAmount = proposedAmount,
            EstimatedDays = estimatedDays,
            CoverLetter = coverLetter.Trim(),
            Status = ProposalStatus.Pending
        };
    }

    public void Accept()
    {
        if (Status != ProposalStatus.Pending)
            throw new InvalidOperationException("لايمكن قبول عرض غير معلق");

        Status = ProposalStatus.Accepted;
        AcceptedAt = DateTime.UtcNow;
    }

    public void Reject(string? reason = null)
    {
        if (Status != ProposalStatus.Pending)
            throw new InvalidOperationException("لا يمكن رفض عرض غير معلق");

        Status = ProposalStatus.Rejected;
        RejectionReason = reason;
    }

    public void Withdraw()
    {
        if (Status != ProposalStatus.Pending)
            throw new InvalidOperationException("لا يمكن سحب عرض غير معلق");

        Status = ProposalStatus.Withdrawn;
    }
}