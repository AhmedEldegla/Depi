namespace DEPI.Domain.Entities.Projects;

using Depi.Domain.Modules.Projects.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;
using DEPI.Domain.Entities.Categories;
using DEPI.Domain.Entities.Identity;

public class Project : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid OwnerId { get;set; }
    public Guid? CategoryId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public string? Details { get;set; }
    public ProjectType Type { get;set; }
    public ProjectStatus Status { get;set; }
    public decimal? BudgetMin { get;set; }
    public decimal? BudgetMax { get;set; }
    public decimal? FixedPrice { get;set; }
    public decimal? HourlyRateMin { get;set; }
    public decimal? HourlyRateMax { get;set; }
    public int? EstimatedHours { get;set; }
    public int? DurationDays { get;set; }
    public ExperienceLevel RequiredLevel { get;set; }
    public DateTime? StartDate { get;set; }
    public DateTime? Deadline { get;set; }
    public string? Requirements { get;set; }
    public string? Deliverables { get;set; }
    public string? Skills { get;set; }
    public bool IsFeatured { get;set; }
    public bool IsUrgent { get;set; }
    public bool IsNda { get;set; }
    public int ViewsCount { get;set; }
    public int ProposalsCount { get;set; }
    public Guid? AssignedFreelancerId { get;set; }
    public DateTime? StartedAt { get;set; }
    public DateTime? CompletedAt { get;set; }
    public decimal? FinalPrice { get;set; }

    public User? Owner { get;set; }
    public Category? Category { get;set; }
    public User? AssignedFreelancer { get;set; }
    public ICollection<Proposal> Proposals { get;set; } = new HashSet<Proposal>();
    public ICollection<ProjectAttachment> Attachments { get;set; } = new HashSet<ProjectAttachment>();
    public ICollection<Contract> Contracts { get;set; } = new HashSet<Contract>();

    private Project() { }

    public static Project Create(
        Guid ownerId,
        string title,
        string description,
        ProjectType type,
        ProjectStatus status,
        decimal? budgetMin = null,
        decimal? budgetMax = null,
        decimal? fixedPrice = null,
        ExperienceLevel requiredLevel = ExperienceLevel.Intermediate,
        DateTime? deadline = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Project title is required", nameof(title));

        if (ownerId == Guid.Empty)
            throw new ArgumentException("Owner ID is required", nameof(ownerId));

        var project = new Project
        {
            OwnerId = ownerId,
            Title = title.Trim(),
            Description = description.Trim(),
            Type = type,
            Status = status,
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

    public void UpdateTitle(string title)
    {
        if (Status != ProjectStatus.Draft)
            throw new InvalidOperationException("Can only update title of draft projects");
            
        Title = title.Trim();
    }

    public void UpdateDescription(string description)
    {
        if (Status != ProjectStatus.Draft)
            throw new InvalidOperationException("Can only update description of draft projects");
            
        Description = description.Trim();
    }

    public void UpdateBudget(decimal? budgetMin, decimal? budgetMax)
    {
        if (Status != ProjectStatus.Draft)
            throw new InvalidOperationException("Can only update budget of draft projects");
            
        BudgetMin = budgetMin;
        BudgetMax = budgetMax;
    }

    public void Open()
    {
        if (Status != ProjectStatus.Draft)
            throw new InvalidOperationException("Only draft projects can be opened");
            
        Status = ProjectStatus.Open;
        RaiseDomainEvent(new ProjectOpenedEvent(Id, OwnerId));
    }

    public void AssignFreelancer(Guid freelancerId, decimal agreedPrice)
    {
        if (Status != ProjectStatus.Open)
            throw new InvalidOperationException("Project must be open to assign a freelancer");
            
        if (freelancerId == Guid.Empty)
            throw new ArgumentException("Freelancer ID is required", nameof(freelancerId));

        AssignedFreelancerId = freelancerId;
        FinalPrice = agreedPrice;
        Status = ProjectStatus.InProgress;
        StartedAt = DateTime.UtcNow;
        
        RaiseDomainEvent(new FreelancerAssignedEvent(Id, freelancerId, agreedPrice));
    }

    public void Complete()
    {
        if (Status != ProjectStatus.InProgress)
            throw new InvalidOperationException("Only in-progress projects can be completed");
            
        Status = ProjectStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        
        RaiseDomainEvent(new ProjectCompletedEvent(Id, OwnerId, AssignedFreelancerId, FinalPrice ?? 0));
    }

    public void Cancel(string reason)
    {
        if (Status == ProjectStatus.Completed)
            throw new InvalidOperationException("Completed projects cannot be cancelled");
            
        Status = ProjectStatus.Cancelled;
    }

    public void IncrementViews()
    {
        ViewsCount++;
    }

    public void IncrementProposals()
    {
        ProposalsCount++;
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

public class FreelancerAssignedEvent : DomainEventBase
{
    public Guid ProjectId { get; }
    public Guid FreelancerId { get; }
    public decimal AgreedPrice { get; }

    public override string EventType => nameof(FreelancerAssignedEvent);

    public FreelancerAssignedEvent(Guid projectId, Guid freelancerId, decimal agreedPrice)
    {
        ProjectId = projectId;
        FreelancerId = freelancerId;
        AgreedPrice = agreedPrice;
    }
}

public class ProjectCompletedEvent : DomainEventBase
{
    public Guid ProjectId { get; }
    public Guid OwnerId { get; }
    public Guid? FreelancerId { get; }
    public decimal FinalPrice { get; }

    public override string EventType => nameof(ProjectCompletedEvent);

    public ProjectCompletedEvent(Guid projectId, Guid ownerId, Guid? freelancerId, decimal finalPrice)
    {
        ProjectId = projectId;
        OwnerId = ownerId;
        FreelancerId = freelancerId;
        FinalPrice = finalPrice;
    }
}
