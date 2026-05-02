using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

namespace DEPI.Domain.Entities.Students;

public class StudentProfile : AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public DateTime EnrollmentDate { get; set; }
    public DateTime? GraduationDate { get; set; }
    public StudentStatus Status { get; set; } = StudentStatus.Active;
    public OnboardingStep CurrentStep { get; set; } = OnboardingStep.Profile;
    public int CompletedSteps { get; set; }
    public int TotalSteps { get; set; } = 5;
    public decimal ProgressPercentage { get; set; }
    public int PortfolioItemsCount { get; set; }
    public bool HasCompletedPortfolio { get; set; }
    public decimal SkillsAssessmentScore { get; set; }
    public bool HasCompletedSkillsAssessment { get; set; }
    public decimal ReadinessScore { get; set; }
    public bool IsReadyForMarket { get; set; }
    public string? AssignedCoachId { get; set; }
    public DateTime? CoachAssignedAt { get; set; }
    public Guid? FirstProjectId { get; set; }
    public DateTime? FirstProjectAt { get; set; }
    public string Skills { get; set; } = string.Empty;
    public string Goals { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public int TotalLearningHours { get; set; }
    public int CompletedCourses { get; set; }
    public int CompletedProjects { get; set; }
    public decimal AverageRating { get; set; }
    public StudentLevel Level { get; set; } = StudentLevel.Beginner;
    public DateTime? PromotedToFreelancerAt { get; set; }
    public string? PromotedBy { get; set; }

    public virtual User User { get; set; } = null!;

    public void CompleteStep()
    {
        CompletedSteps++;
        ProgressPercentage = TotalSteps > 0 ? (decimal)CompletedSteps / TotalSteps * 100 : 0;

        CurrentStep = CompletedSteps switch
        {
            1 => OnboardingStep.Portfolio,
            2 => OnboardingStep.Skills,
            3 => OnboardingStep.Coach,
            4 => OnboardingStep.FirstProject,
            5 => OnboardingStep.Graduation,
            _ => OnboardingStep.Completed
        };

        if (CompletedSteps >= TotalSteps) Status = StudentStatus.Graduated;
    }

    public void AssignCoach(string coachId)
    {
        AssignedCoachId = coachId;
        CoachAssignedAt = DateTime.UtcNow;
    }

    public void CompletePortfolio()
    {
        HasCompletedPortfolio = true;
        PortfolioItemsCount++;
    }

    public void CompleteSkillsAssessment(decimal score)
    {
        SkillsAssessmentScore = score;
        HasCompletedSkillsAssessment = true;
        ReadinessScore = score;
    }

    public void CompleteFirstProject(Guid projectId)
    {
        FirstProjectId = projectId;
        FirstProjectAt = DateTime.UtcNow;
        CompletedProjects++;
    }

    public void PromoteToFreelancer(string promotedBy)
    {
        PromotedToFreelancerAt = DateTime.UtcNow;
        PromotedBy = promotedBy;
        Status = StudentStatus.Promoted;
    }

    public void UpdateSkills(string skills)
    {
        Skills = skills;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Graduate()
    {
        Status = StudentStatus.Graduated;
        GraduationDate = DateTime.UtcNow;
        CurrentStep = OnboardingStep.Completed;
    }
}

public enum StudentStatus { Active, OnHold, Graduated, Promoted, Withdrawn }
public enum OnboardingStep { Profile = 0, Portfolio = 1, Skills = 2, Coach = 3, FirstProject = 4, Graduation = 5, Completed = 6 }
public enum StudentLevel { Beginner, Intermediate, Advanced, ReadyForMarket }
