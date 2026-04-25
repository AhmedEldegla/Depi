namespace DEPI.Domain.Entities.Projects;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Proposals;
using DEPI.Domain.Enums;


public class Project : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid OwnerId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ProjectType Type { get; private set; }
    public ProjectStatus Status { get; private set; }
    public decimal? BudgetMin { get; private set; }
    public decimal? BudgetMax { get; private set; }
    public decimal? FixedPrice { get; private set; }
    public ExperienceLevel RequiredLevel { get; private set; }
    public DateTime? Deadline { get; private set; }
    public string? Skills { get; private set; }
    public bool IsFeatured { get; private set; }
    public bool IsUrgent { get; private set; }
    public bool IsNda { get; private set; }
    public int ViewsCount { get; private set; }
    public int ProposalsCount { get; private set; }
    public Guid? AssignedFreelancerId { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public decimal? FinalPrice { get; private set; }

    public virtual User? Owner { get; private set; }
    public virtual Category? Category { get; private set; }
    public virtual User? AssignedFreelancer { get; private set; }
    public virtual ICollection<Proposal> Proposals { get; private set; } = new List<Proposal>();

    private Project() { }

    public void SetCreatedBy(Guid creatorId)
    {
        CreatedBy = creatorId;
    }

    public static Project Create(
        Guid ownerId,
        string title,
        string description,
        ProjectType type,
        decimal? budgetMin = null,
        decimal? budgetMax = null,
        decimal? fixedPrice = null,
        ExperienceLevel requiredLevel = ExperienceLevel.Intermediate,
        DateTime? deadline = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("عنوان المشروع مطلوب", nameof(title));

        var project = new Project
        {
            OwnerId = ownerId,
            Title = title.Trim(),
            Description = description.Trim(),
            Type = type,
            Status = ProjectStatus.Draft,
            BudgetMin = budgetMin,
            BudgetMax = budgetMax,
            FixedPrice = fixedPrice,
            RequiredLevel = requiredLevel,
            Deadline = deadline,
            IsFeatured = false,
            IsUrgent = false,
            IsNda = false,
            ViewsCount = 0,
            ProposalsCount = 0
        };

        project.RaiseDomainEvent(new ProjectCreatedEvent(project.Id, ownerId, title));
        return project;
    }

    public void Open()
    {
        if (Status != ProjectStatus.Draft)
            throw new InvalidOperationException("只能打开草稿项目");
            
        Status = ProjectStatus.Open;
        RaiseDomainEvent(new ProjectOpenedEvent(Id, OwnerId));
    }

    public void AssignFreelancer(Guid freelancerId, decimal agreedPrice)
    {
        if (Status != ProjectStatus.Open)
            throw new InvalidOperationException("项目必须开放才能分配");

        AssignedFreelancerId = freelancerId;
        FinalPrice = agreedPrice;
        Status = ProjectStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status != ProjectStatus.InProgress)
            throw new InvalidOperationException("只能完成进行中的项目");

        Status = ProjectStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void Cancel(string reason)
    {
        if (Status == ProjectStatus.Completed)
            throw new InvalidOperationException("无法取消已完成的項目");

        Status = ProjectStatus.Cancelled;
    }

    public void Update(
        string title,
        string description,
        decimal? budgetMin,
        decimal? budgetMax,
        DateTime? deadline,
        string? skills,
        ExperienceLevel requiredLevel,
        bool isNda)
    {
        Title = title;
        Description = description;
        BudgetMin = budgetMin;
        BudgetMax = budgetMax;
        Deadline = deadline;
        Skills = skills;
        RequiredLevel = requiredLevel;
        IsNda = isNda;
    }

    public void IncrementViews()
    {
        ViewsCount++;
    }

    public void IncrementProposals()
    {
        ProposalsCount++;
    }

    public void SetSkills(string skills)
    {
        Skills = skills;
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

public class ProjectCreatedEvent : DomainEventBase
{
    public Guid ProjectId { get; }
    public Guid OwnerId { get; }
    public string Title { get; }

    public override string EventType => nameof(ProjectCreatedEvent);

    public ProjectCreatedEvent(Guid projectId, Guid ownerId, string title)
    {
        ProjectId = projectId;
        OwnerId = ownerId;
        Title = title;
    }
}

public class ProjectOpenedEvent : DomainEventBase
{
    public Guid ProjectId { get; }
    public Guid OwnerId { get; }

    public override string EventType => nameof(ProjectOpenedEvent);

    public ProjectOpenedEvent(Guid projectId, Guid ownerId)
    {
        ProjectId = projectId;
        OwnerId = ownerId;
    }
}