using System;
using System.Collections.Generic;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Recruitment;

namespace DEPI.Domain.Entities.Shared;

public class Language : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string NativeName { get; set; } = string.Empty;
    public string Direction { get; set; } = "LTR";
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public virtual ICollection<UserLanguage> UserLanguages { get; set; } = new List<UserLanguage>();
}

public class UserLanguage : Entity
{
    public Guid UserId { get; set; }
    public Guid LanguageId { get; set; }
    public ProficiencyLevel Proficiency { get; set; } = ProficiencyLevel.Native;
    public bool IsPrimary { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Language Language { get; set; } = null!;
}

public enum ProficiencyLevel
{
    Native,
    Fluent,
    Professional,
    Conversational,
    Basic
}

public class IndustryCategory : Entity
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentIndustryId { get; set; }
    public string IconUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }

    public virtual IndustryCategory? ParentIndustry { get; set; }
    public virtual ICollection<IndustryCategory> SubIndustries { get; set; } = new List<IndustryCategory>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}