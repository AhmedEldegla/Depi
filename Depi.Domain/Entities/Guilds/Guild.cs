using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;

namespace DEPI.Domain.Entities.Guilds;

public class Guild : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public Guid LeaderId { get; set; }
    public GuildStatus Status { get; set; } = GuildStatus.Active;
    public int MemberCount { get; set; }
    public int CompletedProjects { get; set; }
    public decimal AverageRating { get; set; }
    public decimal TotalEarnings { get; set; }
    public string Requirements { get; set; } = string.Empty;
    public bool IsAcceptingMembers { get; set; } = true;
    public decimal MinProfileScore { get; set; }
    public int MaxMembers { get; set; } = 20;

    public virtual User Leader { get; set; } = null!;
    public virtual ICollection<GuildMember> Members { get; set; } = new List<GuildMember>();
    public virtual ICollection<GuildProject> Projects { get; set; } = new List<GuildProject>();

    public void UpdateInfo(string name, string description, string specialization, bool acceptingMembers, int maxMembers, decimal minScore)
    {
        Name = name; Description = description; Specialization = specialization;
        IsAcceptingMembers = acceptingMembers; MaxMembers = maxMembers; MinProfileScore = minScore;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close() { Status = GuildStatus.Closed; }
    public void Reopen() { Status = GuildStatus.Active; }
    public void IncrementProjects() { CompletedProjects++; }
}

public class GuildMember : AuditableEntity
{
    public Guid GuildId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = "Member";
    public string Skills { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public virtual Guild Guild { get; set; } = null!;
    public virtual User User { get; set; } = null!;

    public void Leave() { IsActive = false; }
}

public class GuildProject : AuditableEntity
{
    public Guid GuildId { get; set; }
    public Guid ProjectId { get; set; }
    public string Status { get; set; } = "Active";
    public DateTime? CompletedAt { get; set; }
    public decimal? Earnings { get; set; }

    public virtual Guild Guild { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;

    public void Complete(decimal earnings)
    {
        Status = "Completed";
        CompletedAt = DateTime.UtcNow;
        Earnings = earnings;
    }
}

public enum GuildStatus { Active, Closed, Archived }
