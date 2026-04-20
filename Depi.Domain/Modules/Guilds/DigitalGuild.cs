namespace DEPI.Domain.Entities.Guilds;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;

public class DigitalGuild : AuditableEntity
{
    public string Name { get;set; } = string.Empty;
    public string Slug { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public string Specialty { get;set; } = string.Empty;
    public string? IconUrl { get;set; }
    public string? BannerUrl { get;set; }
    public Guid LeaderId { get;set; }
    public int MemberCount { get;set; }
    public int ActiveProjectsCount { get;set; }
    public decimal TotalEarnings { get;set; }
    public decimal AverageRating { get;set; }
    public int TotalReviews { get;set; }
    public bool IsPublic { get;set; }
    public bool IsVerified { get;set; }
    public GuildStatus Status { get;set; }
    public string? Requirements { get;set; }
    public string? Benefits { get;set; }
    public User? Leader { get;set; }
    public ICollection<GuildMember> Members { get;set; } = new HashSet<GuildMember>();
    public ICollection<GuildProject> Projects { get;set; } = new HashSet<GuildProject>();
    public ICollection<GuildApplication> Applications { get;set; } = new HashSet<GuildApplication>();

    private DigitalGuild() { }

    public static DigitalGuild Create(
        string name,
        string description,
        string specialty,
        Guid leaderId,
        bool isPublic = true)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Guild name is required");
        if (string.IsNullOrWhiteSpace(specialty))
            throw new ArgumentException("Specialty is required");

        var slug = name.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("_", "-");

        return new DigitalGuild
        {
            Name = name.Trim(),
            Slug = slug,
            Description = description.Trim(),
            Specialty = specialty.Trim(),
            LeaderId = leaderId,
            IsPublic = isPublic,
            Status = GuildStatus.Active,
            MemberCount = 1,
            ActiveProjectsCount = 0,
            TotalEarnings = 0
        };
    }

    public void UpdateInfo(string name, string description, string? requirements, string? benefits)
    {
        Name = name.Trim();
        Description = description.Trim();
        Requirements = requirements?.Trim();
        Benefits = benefits?.Trim();
    }

    public void SetLeader(Guid userId)
    {
        LeaderId = userId;
    }

    public void IncrementMembers()
    {
        MemberCount++;
    }

    public void DecrementMembers()
    {
        if (MemberCount > 0)
            MemberCount--;
    }

    public void AddProject(decimal projectValue)
    {
        ActiveProjectsCount++;
        TotalEarnings += projectValue;
    }

    public void CompleteProject()
    {
        if (ActiveProjectsCount > 0)
            ActiveProjectsCount--;
    }

    public void Verify()
    {
        IsVerified = true;
    }

    public void Suspend()
    {
        Status = GuildStatus.Suspended;
    }

    public void Activate()
    {
        Status = GuildStatus.Active;
    }

    public void Archive()
    {
        Status = GuildStatus.Archived;
    }
}

public class GuildMember : BaseEntity
{
    public Guid GuildId { get;set; }
    public Guid UserId { get;set; }
    public GuildMemberRole Role { get;set; }
    public GuildMemberStatus Status { get;set; }
    public DateTime JoinedAt { get;set; }
    public DateTime? LastActiveAt { get;set; }
    public decimal ContributionScore { get;set; }
    public int CompletedProjectsCount { get;set; }
    public decimal TotalEarnings { get;set; }
    public string? Notes { get;set; }
    public DigitalGuild? Guild { get;set; }
    public User? User { get;set; }

    private GuildMember() { }

    public static GuildMember Create(Guid guildId, Guid userId, GuildMemberRole role = GuildMemberRole.Member)
    {
        return new GuildMember
        {
            GuildId = guildId,
            UserId = userId,
            Role = role,
            Status = GuildMemberStatus.Active,
            JoinedAt = DateTime.UtcNow,
            ContributionScore = 0
        };
    }

    public void Promote()
    {
        Role = Role switch
        {
            GuildMemberRole.Member => GuildMemberRole.Senior,
            GuildMemberRole.Senior => GuildMemberRole.Leader,
            _ => Role
        };
    }

    public void Demote()
    {
        Role = Role switch
        {
            GuildMemberRole.Leader => GuildMemberRole.Senior,
            GuildMemberRole.Senior => GuildMemberRole.Member,
            _ => Role
        };
    }

    public void UpdateActivity()
    {
        LastActiveAt = DateTime.UtcNow;
    }

    public void UpdateContribution(decimal score)
    {
        ContributionScore = score;
    }

    public void IncrementCompletedProjects(decimal earnings)
    {
        CompletedProjectsCount++;
        TotalEarnings += earnings;
    }
}

public class GuildProject : BaseEntity
{
    public Guid GuildId { get;set; }
    public Guid ProjectId { get;set; }
    public Guid TeamLeaderId { get;set; }
    public GuildProjectStatus Status { get;set; }
    public decimal TotalValue { get;set; }
    public decimal GuildFee { get;set; }
    public DateTime StartedAt { get;set; }
    public DateTime? CompletedAt { get;set; }
    public decimal ClientRating { get;set; }

    public DigitalGuild? Guild { get;set; }
    public Project? Project { get;set; }
    public ICollection<GuildProjectMember> TeamMembers { get;set; } = new HashSet<GuildProjectMember>();

    private GuildProject() { }

    public static GuildProject Create(Guid guildId, Guid projectId, Guid teamLeaderId, decimal totalValue)
    {
        var guildFee = totalValue * 0.05m;

        return new GuildProject
        {
            GuildId = guildId,
            ProjectId = projectId,
            TeamLeaderId = teamLeaderId,
            Status = GuildProjectStatus.InProgress,
            TotalValue = totalValue,
            GuildFee = guildFee,
            StartedAt = DateTime.UtcNow
        };
    }

    public void Complete(decimal clientRating)
    {
        Status = GuildProjectStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        ClientRating = clientRating;
    }

    public void Cancel()
    {
        Status = GuildProjectStatus.Cancelled;
    }
}

public class GuildProjectMember : BaseEntity
{
    public Guid GuildProjectId { get;set; }
    public Guid UserId { get;set; }
    public string Role { get;set; } = string.Empty;
    public decimal AssignedValue { get;set; }
    public decimal EarnedValue { get;set; }
    public GuildProjectMemberStatus Status { get;set; }

    public GuildProject? Project { get;set; }
    public User? User { get;set; }

    private GuildProjectMember() { }

    public static GuildProjectMember Create(
        Guid projectId,
        Guid userId,
        string role,
        decimal assignedValue)
    {
        return new GuildProjectMember
        {
            GuildProjectId = projectId,
            UserId = userId,
            Role = role,
            AssignedValue = assignedValue,
            Status = GuildProjectMemberStatus.Assigned
        };
    }
}

public class GuildApplication : BaseEntity
{
    public Guid GuildId { get;set; }
    public Guid ApplicantId { get;set; }
    public string MotivationLetter { get;set; } = string.Empty;
    public GuildApplicationStatus Status { get;set; }
    public Guid? ReviewedById { get;set; }
    public DateTime AppliedAt { get;set; }
    public DateTime? ReviewedAt { get;set; }
    public string? ReviewNotes { get;set; }

    public virtual DigitalGuild? Guild { get;set; }
    public User? Applicant { get;set; }
    public User? Reviewer { get;set; }

    private GuildApplication() { }

    public static GuildApplication Create(Guid guildId, Guid applicantId, string motivationLetter)
    {
        return new GuildApplication
        {
            GuildId = guildId,
            ApplicantId = applicantId,
            MotivationLetter = motivationLetter.Trim(),
            Status = GuildApplicationStatus.Pending,
            AppliedAt = DateTime.UtcNow
        };
    }

    public void Approve(Guid reviewerId)
    {
        Status = GuildApplicationStatus.Approved;
        ReviewedById = reviewerId;
        ReviewedAt = DateTime.UtcNow;
    }

    public void Reject(Guid reviewerId, string reason)
    {
        Status = GuildApplicationStatus.Rejected;
        ReviewedById = reviewerId;
        ReviewedAt = DateTime.UtcNow;
        ReviewNotes = reason;
    }
}

public enum GuildStatus
{
    Active = 1,
    Suspended = 2,
    Archived = 3
}

public enum GuildMemberRole
{
    Member = 1,
    Senior = 2,
    Leader = 3
}

public enum GuildMemberStatus
{
    Active = 1,
    Inactive = 2,
    Suspended = 3
}

public enum GuildProjectStatus
{
    Active = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}

public enum GuildProjectMemberStatus
{
    Assigned = 1,
    Active = 2,
    Completed = 3,
    Blocked = 4
}

public enum GuildApplicationStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Withdrawn = 4
}
