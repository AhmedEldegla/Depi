using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Shared;

namespace DEPI.Domain.Entities.Companies;

public class Company : AuditableEntity
{
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Size { get; set; } = "1-10";
    public string FoundedYear { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public Guid? IndustryId { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; } = true;
    public int TeamSize { get; set; }
    public decimal Rating { get; set; }
    public int TotalProjects { get; set; }
    public string CoverImageUrl { get; set; } = string.Empty;
    public string SocialLinks { get; set; } = string.Empty;

    public virtual User Owner { get; set; } = null!;
    public virtual IndustryCategory? Industry { get; set; }
    public virtual ICollection<CompanyMember> Members { get; set; } = new List<CompanyMember>();
    public virtual ICollection<CompanyProject> Projects { get; set; } = new List<CompanyProject>();
    public virtual ICollection<CompanyFollower> Followers { get; set; } = new List<CompanyFollower>();

    public void UpdateInfo(string name, string description, string website, string size, string location)
    {
        Name = name;
        Description = description;
        Website = website;
        Size = size;
        Location = location;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class CompanyMember : AuditableEntity
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = "Member";
    public string Title { get; set; } = string.Empty;
    public bool CanManage { get; set; }
    public bool CanInvite { get; set; }
    public bool CanPost { get; set; }
    public DateTime JoinedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual Company Company { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}

public class CompanyProject : AuditableEntity
{
    public Guid CompanyId { get; set; }
    public Guid ProjectId { get; set; }
    public string Role { get; set; } = "Client";
    public bool IsPinned { get; set; }

    public virtual Company Company { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;
}

public class CompanyFollower : AuditableEntity
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public bool IsNotificationsEnabled { get; set; } = true;

    public virtual Company Company { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}

public class CompanyReview : AuditableEntity
{
    public Guid CompanyId { get; set; }
    public Guid AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public Guid? ProjectId { get; set; }
    public bool IsVerified { get; set; }

    public virtual Company Company { get; set; } = null!;
    public virtual User Author { get; set; } = null!;
    public virtual Project? Project { get; set; } = null!;
}