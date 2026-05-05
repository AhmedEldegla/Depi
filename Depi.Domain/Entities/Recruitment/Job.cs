using System;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Companies;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Entities.Shared;

namespace DEPI.Domain.Entities.Recruitment;

public class Job : AuditableEntity
{
    public Guid OwnerId { get; set; }
    public Guid CompanyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Guid? SubCategoryId { get; set; }
    public Guid? IndustryId { get; set; }
    public JobType Type { get; set; } = JobType.Freelance;
    public JobStatus Status { get; set; } = JobStatus.Active;
    public decimal BudgetMin { get; set; }
    public decimal BudgetMax { get; set; }
    public string BudgetType { get; set; } = "Fixed";
    public string Currency { get; set; } = "USD";
    public int Duration { get; set; }
    public string ExperienceLevel { get; set; } = "Intermediate";
    public string Location { get; set; } = string.Empty;
    public bool IsRemote { get; set; }
    public bool IsFeatured { get; set; }
    public int ViewsCount { get; set; }
    public int ApplicationsCount { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string SkillsRequired { get; set; } = string.Empty;
    public string Benefits { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public int ConnectsRequired { get; set; } = 1;
    public decimal MatchScore { get; set; }

    public virtual User Owner { get; set; } = null!;
    public virtual Company Company { get; set; } = null!;
    public virtual JobCategory Category { get; set; } = null!;
    public virtual JobCategory? SubCategory { get; set; }
    public virtual IndustryCategory? Industry { get; set; }
    public virtual ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
    public virtual ICollection<JobSkill> Skills { get; set; } = new List<JobSkill>();
    public virtual ICollection<JobQuestion> Questions { get; set; } = new List<JobQuestion>();
}

public class JobCategory : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentCategoryId { get; set; }
    public string IconUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int JobsCount { get; set; }
    public int DisplayOrder { get; set; }

    public virtual JobCategory? ParentCategory { get; set; }
    public virtual ICollection<JobCategory> SubCategories { get; set; } = new List<JobCategory>();
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}

public class JobApplication : AuditableEntity
{
    public Guid JobId { get; set; }
    public Guid ApplicantId { get; set; }
    public string CoverLetter { get; set; } = string.Empty;
    public string ProposedRate { get; set; } = string.Empty;
    public string ProposedTimeline { get; set; } = string.Empty;
    public ApplicationStatus Status { get; } = ApplicationStatus.Pending;
    public int AIMatchScore { get; set; }
    public string AIAnalysis { get; set; } = string.Empty;
    public DateTime? InterviewScheduledAt { get; set; }
    public string InterviewNotes { get; set; } = string.Empty;
    public string RejectionReason { get; set; } = string.Empty;
    public bool IsShortlisted { get; set; }
    public int ViewCount { get; set; }

    public virtual Job Job { get; set; } = null!;
    public virtual User Applicant { get; set; } = null!;
    public virtual ICollection<JobApplicationAttachment> Attachments { get; set; } = new List<JobApplicationAttachment>();
}

public class JobSkill : AuditableEntity
{
    public Guid JobId { get; set; }
    public Guid SkillId { get; set; }
    public bool IsRequired { get; set; }
    public int Importance { get; set; }
    public string ProficiencyLevel { get; set; } = string.Empty;

    public virtual Job Job { get; set; } = null!;
    public virtual Profiles.Skill Skill { get; set; } = null!;
}

public class JobQuestion : AuditableEntity
{
    public Guid JobId { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public bool IsRequired { get; set; } = true;
    public int DisplayOrder { get; set; }

    public virtual Job Job { get; set; } = null!;
}

public class JobApplicationAttachment : AuditableEntity
{
    public Guid ApplicationId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }

    public virtual JobApplication Application { get; set; } = null!;
}

public enum JobType
{
    Freelance,
    PartTime,
    FullTime,
    Contract,
    Internship
}

public enum JobStatus
{
    Draft,
    Active,
    Paused,
    Closed,
    Expired,
    Pending,
    Rejected
}

public enum ApplicationStatus
{
    Pending,
    Screening,
    Shortlisted,
    Interview,
    Offer,
    Hired,
    Rejected,
    Withdrawn
}