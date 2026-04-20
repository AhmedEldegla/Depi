namespace DEPI.Domain.Entities.Projects;

using Depi.Domain.Modules.Projects.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;
using DEPI.Domain.Entities.Identity;

public class Proposal : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid ProjectId { get;set; }
    public Guid FreelancerId { get;set; }
    public string CoverLetter { get;set; } = string.Empty;
    public decimal ProposedAmount { get;set; }
    public int DeliveryDays { get;set; }
    public ProposalStatus Status { get;set; }
    public double? MatchScore { get;set; }
    public bool IsShortlisted { get;set; }
    public DateTime? ShortlistedAt { get;set; }
    public DateTime? InterviewScheduledAt { get;set; }
    public DateTime? WithdrawnAt { get;set; }
    public DateTime? AcceptedAt { get;set; }
    public DateTime? RejectedAt { get;set; }
    public string? RejectionReason { get;set; }
    public string? MilestonesJson { get;set; }

    public Project? Project { get;set; }
    public User? Freelancer { get;set; }
    public ICollection<ProposalAttachment> Attachments { get;set; } = new HashSet<ProposalAttachment>();

    private Proposal() { }

    public static Proposal Create(
        Guid projectId,
        Guid freelancerId,
        string coverLetter,
        decimal proposedAmount,
        int deliveryDays)
    {
        if (projectId == Guid.Empty)
            throw new ArgumentException("Project ID is required", nameof(projectId));

        if (freelancerId == Guid.Empty)
            throw new ArgumentException("Freelancer ID is required", nameof(freelancerId));

        if (string.IsNullOrWhiteSpace(coverLetter))
            throw new ArgumentException("Cover letter is required", nameof(coverLetter));

        if (proposedAmount <= 0)
            throw new ArgumentException("Proposed amount must be greater than zero", nameof(proposedAmount));

        if (deliveryDays <= 0)
            throw new ArgumentException("Delivery days must be greater than zero", nameof(deliveryDays));

        var proposal = new Proposal
        {
            ProjectId = projectId,
            FreelancerId = freelancerId,
            CoverLetter = coverLetter.Trim(),
            ProposedAmount = proposedAmount,
            DeliveryDays = deliveryDays,
            Status = ProposalStatus.Pending
        };

        proposal.RaiseDomainEvent(new ProposalSubmittedEvent(proposal.Id, projectId, freelancerId));
        return proposal;
    }

    public void Shortlist()
    {
        if (Status != ProposalStatus.Pending)
            throw new InvalidOperationException("Can only shortlist pending proposals");

        IsShortlisted = true;
        ShortlistedAt = DateTime.UtcNow;
    }

    public void ScheduleInterview(DateTime scheduledAt)
    {
        if (Status != ProposalStatus.Pending && Status != ProposalStatus.Shortlisted)
            throw new InvalidOperationException("Cannot schedule interview for this proposal");

        InterviewScheduledAt = scheduledAt;
        Status = ProposalStatus.Interview;
    }

    public void Accept()
    {
        if (Status == ProposalStatus.Accepted || Status == ProposalStatus.Rejected || Status == ProposalStatus.Withdrawn)
            throw new InvalidOperationException("Cannot accept this proposal");

        Status = ProposalStatus.Accepted;
        AcceptedAt = DateTime.UtcNow;
        RaiseDomainEvent(new ProposalAcceptedEvent(Id, ProjectId, FreelancerId));
    }

    public void Reject(string reason)
    {
        if (Status == ProposalStatus.Accepted || Status == ProposalStatus.Withdrawn)
            throw new InvalidOperationException("Cannot reject this proposal");

        Status = ProposalStatus.Rejected;
        RejectedAt = DateTime.UtcNow;
        RejectionReason = reason;
    }

    public void Withdraw()
    {
        if (Status == ProposalStatus.Accepted)
            throw new InvalidOperationException("Cannot withdraw accepted proposal");

        Status = ProposalStatus.Withdrawn;
        WithdrawnAt = DateTime.UtcNow;
    }

    public void SetMatchScore(double score)
    {
        if (score < 0 || score > 100)
            throw new ArgumentOutOfRangeException(nameof(score), "Match score must be between 0 and 100");

        MatchScore = score;
    }

    private void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public class ProposalSubmittedEvent : DomainEventBase
{
    public Guid ProposalId { get; }
    public Guid ProjectId { get; }
    public Guid FreelancerId { get; }

    public override string EventType => nameof(ProposalSubmittedEvent);

    public ProposalSubmittedEvent(Guid proposalId, Guid projectId, Guid freelancerId)
    {
        ProposalId = proposalId;
        ProjectId = projectId;
        FreelancerId = freelancerId;
    }
}

public class ProposalAcceptedEvent : DomainEventBase
{
    public Guid ProposalId { get; }
    public Guid ProjectId { get; }
    public Guid FreelancerId { get; }

    public override string EventType => nameof(ProposalAcceptedEvent);

    public ProposalAcceptedEvent(Guid proposalId, Guid projectId, Guid freelancerId)
    {
        ProposalId = proposalId;
        ProjectId = projectId;
        FreelancerId = freelancerId;
    }
}

public class ProposalAttachment : AuditableEntity
{
    public Guid ProposalId { get;set; }
    public Guid MediaId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string? Description { get;set; }

    public Proposal? Proposal { get;set; }
    public Media.Media? Media { get;set; }

    private ProposalAttachment() { }

    public static ProposalAttachment Create(Guid proposalId, Guid mediaId, string title, string? description = null)
    {
        return new ProposalAttachment
        {
            ProposalId = proposalId,
            MediaId = mediaId,
            Title = title,
            Description = description
        };
    }
}
