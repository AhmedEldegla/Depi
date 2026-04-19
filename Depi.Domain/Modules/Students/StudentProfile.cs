namespace DEPI.Domain.Entities.Students;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class StudentProfile : AuditableEntity
{
    public Guid UserId { get;set; }
    public Guid? UniversityId { get;set; }
    public string? Major { get;set; }
    public int? GraduationYear { get;set; }
    public string? StudentId { get;set; }
    public StudentStatus Status { get;set; }
    public int CurrentStep { get;set; }
    public int TotalSteps { get;set; } = 10;
    public decimal ProgressPercentage { get;set; }
    public DateTime? EnrolledAt { get;set; }
    public DateTime? CompletedAt { get;set; }
    public int TrainingProjectsCount { get;set; }
    public int CompletedTrainingsCount { get;set; }
    public decimal OverallScore { get;set; }
    public bool IsEligibleForFreelancing { get;set; }
    public string? PortfolioUrl { get;set; }

    public User? User { get;set; }
    public ICollection<TrainingProgram> TrainingPrograms { get;set; } = new HashSet<TrainingProgram>();
    public ICollection<TrainingProject> TrainingProjects { get;set; } = new HashSet<TrainingProject>();
    public ICollection<MilestoneTask> MilestoneTasks { get;set; } = new HashSet<MilestoneTask>();

    private StudentProfile() { }

    public static StudentProfile Create(Guid userId)
    {
        return new StudentProfile
        {
            UserId = userId,
            Status = StudentStatus.Enrolled,
            CurrentStep = 1,
            EnrolledAt = DateTime.UtcNow
        };
    }

    public void SetUniversity(string? universityId, string? major, int? graduationYear)
    {
        UniversityId = Guid.TryParse(universityId, out var id) ? id : null;
        Major = major;
        GraduationYear = graduationYear;
        StudentId = universityId;
    }

    public void UpdateProgress(int step, decimal percentage)
    {
        CurrentStep = step;
        ProgressPercentage = percentage;
        
        if (percentage >= 100)
        {
            Status = StudentStatus.ReadyForFreelancing;
        }
    }

    public void CompleteTraining()
    {
        CompletedTrainingsCount++;
        
        if (CompletedTrainingsCount >= 3 && ProgressPercentage >= 80)
        {
            IsEligibleForFreelancing = true;
            Status = StudentStatus.ReadyForFreelancing;
            CompletedAt = DateTime.UtcNow;
        }
    }

    public void AddTrainingProject()
    {
        TrainingProjectsCount++;
        UpdateProgress(CurrentStep, ProgressPercentage);
    }

    public void UpdateScore(decimal score)
    {
        OverallScore = score;
    }

    public void Block()
    {
        Status = StudentStatus.Blocked;
    }

    public void Unenroll()
    {
        Status = StudentStatus.Unenrolled;
    }
}

public class TrainingProgram : AuditableEntity
{
    public Guid StudentId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public TrainingProgramType Type { get;set; }
    public TrainingProgramStatus Status { get;set; }
    public DateTime StartedAt { get;set; }
    public DateTime? CompletedAt { get;set; }
    public int CurrentMilestone { get;set; }
    public int TotalMilestones { get;set; }
    public decimal ProgressPercentage { get;set; }
    public decimal Score { get;set; }

    public StudentProfile? Student { get;set; }

    private TrainingProgram() { }

    public static TrainingProgram Create(
        Guid studentId,
        string title,
        string description,
        TrainingProgramType type,
        int totalMilestones)
    {
        return new TrainingProgram
        {
            StudentId = studentId,
            Title = title.Trim(),
            Description = description.Trim(),
            Type = type,
            Status = TrainingProgramStatus.NotStarted,
            TotalMilestones = totalMilestones,
            CurrentMilestone = 0,
            ProgressPercentage = 0
        };
    }

    public void Start()
    {
        Status = TrainingProgramStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void CompleteMilestone(int milestoneNumber)
    {
        CurrentMilestone = milestoneNumber;
        ProgressPercentage = (decimal)CurrentMilestone / TotalMilestones * 100;
        
        if (CurrentMilestone >= TotalMilestones)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Status = TrainingProgramStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        ProgressPercentage = 100;
    }

    public void UpdateScore(decimal score)
    {
        Score = score;
    }

    public void Fail(string reason)
    {
        Status = TrainingProgramStatus.Failed;
    }
}

public class TrainingProject : AuditableEntity
{
    public Guid StudentId { get;set; }
    public Guid? MentorId { get;set; }
    public Guid? RelatedProjectId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public TrainingProjectStatus Status { get;set; }
    public decimal? Budget { get;set; }
    public DateTime StartedAt { get;set; }
    public DateTime? CompletedAt { get;set; }
    public DateTime? Deadline { get;set; }
    public decimal Score { get;set; }
    public string? Review { get;set; }
    public bool IsRealProject { get;set; }

    public StudentProfile? Student { get;set; }
    public User? Mentor { get;set; }

    private TrainingProject() { }

    public static TrainingProject Create(
        Guid studentId,
        string title,
        string description,
        bool isRealProject = false,
        decimal? budget = null)
    {
        return new TrainingProject
        {
            StudentId = studentId,
            Title = title.Trim(),
            Description = description.Trim(),
            Status = TrainingProjectStatus.Draft,
            Budget = budget,
            IsRealProject = isRealProject,
            StartedAt = DateTime.UtcNow,
            Deadline = DateTime.UtcNow.AddDays(30)
        };
    }

    public void Start()
    {
        Status = TrainingProjectStatus.InProgress;
    }

    public void Submit()
    {
        Status = TrainingProjectStatus.Submitted;
    }

    public void Approve(decimal score, string? review = null)
    {
        Status = TrainingProjectStatus.Approved;
        CompletedAt = DateTime.UtcNow;
        Score = score;
        Review = review;
    }

    public void Reject(string reason)
    {
        Status = TrainingProjectStatus.NeedsRevision;
    }

    public void AssignMentor(Guid mentorId)
    {
        MentorId = mentorId;
    }
}

public class MilestoneTask : BaseEntity
{
    public Guid StudentId { get;set; }
    public Guid TrainingProgramId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public MilestoneTaskStatus Status { get;set; }
    public int Order { get;set; }
    public DateTime? DueDate { get;set; }
    public DateTime? CompletedAt { get;set; }
    public decimal Score { get;set; }
    public string? Submission { get;set; }
    public string? Feedback { get;set; }

    public StudentProfile? Student { get;set; }

    private MilestoneTask() { }

    public static MilestoneTask Create(
        Guid studentId,
        Guid programId,
        string title,
        string description,
        int order,
        DateTime? dueDate = null)
    {
        return new MilestoneTask
        {
            StudentId = studentId,
            TrainingProgramId = programId,
            Title = title.Trim(),
            Description = description.Trim(),
            Order = order,
            DueDate = dueDate,
            Status = MilestoneTaskStatus.Pending
        };
    }

    public void Submit(string submission)
    {
        Submission = submission.Trim();
        Status = MilestoneTaskStatus.Submitted;
    }

    public void Grade(decimal score, string? feedback = null)
    {
        Score = score;
        Feedback = feedback;
        Status = score >= 60 ? MilestoneTaskStatus.Approved : MilestoneTaskStatus.NeedsRevision;
        
        if (Status == MilestoneTaskStatus.Approved)
            CompletedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        Status = MilestoneTaskStatus.Approved;
        CompletedAt = DateTime.UtcNow;
    }

    public void Reject(string feedback)
    {
        Status = MilestoneTaskStatus.NeedsRevision;
        Feedback = feedback;
    }
}

public enum StudentStatus
{
    Enrolled = 1,
    InProgress = 2,
    ReadyForFreelancing = 3,
    Blocked = 4,
    Unenrolled = 5
}

public enum TrainingProgramType
{
    WebDevelopment = 1,
    MobileDevelopment = 2,
    DataScience = 3,
    UIUXDesign = 4,
    DigitalMarketing = 5
}

public enum TrainingProgramStatus
{
    NotStarted = 1,
    InProgress = 2,
    Completed = 3,
    Failed = 4
}

public enum TrainingProjectStatus
{
    Draft = 1,
    InProgress = 2,
    Submitted = 3,
    Approved = 4,
    NeedsRevision = 5
}

public enum MilestoneTaskStatus
{
    Pending = 1,
    InProgress = 2,
    Submitted = 3,
    Approved = 4,
    NeedsRevision = 5
}
