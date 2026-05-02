using DEPI.Domain.Entities.AIMatching;
using DEPI.Domain.Entities.Coaching;
using DEPI.Domain.Entities.Companies;
using DEPI.Domain.Entities.Community;
using DEPI.Domain.Entities.Contracts;
using DEPI.Domain.Entities.Guilds;
using DEPI.Domain.Entities.HeadHunters;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Learning;
using DEPI.Domain.Entities.Media;
using DEPI.Domain.Entities.Messaging;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Entities.Proposals;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Recruitment;
using DEPI.Domain.Entities.Reviews;
using DEPI.Domain.Entities.Shared;
using DEPI.Domain.Entities.Students;
using DEPI.Domain.Entities.Wallets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid,
    UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Proposal> Proposals => Set<Proposal>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Milestone> Milestones => Set<Milestone>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Escrow> Escrows => Set<Escrow>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<MediaFile> MediaFiles => Set<MediaFile>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<FreelancerSkill> FreelancerSkills => Set<FreelancerSkill>();
    public DbSet<PortfolioItem> PortfolioItems => Set<PortfolioItem>();
    public DbSet<ServicePackage> ServicePackages => Set<ServicePackage>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<UserLanguage> UserLanguages => Set<UserLanguage>();
    public DbSet<IndustryCategory> Industries => Set<IndustryCategory>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyMember> CompanyMembers => Set<CompanyMember>();
    public DbSet<CompanyProject> CompanyProjects => Set<CompanyProject>();
    public DbSet<CompanyFollower> CompanyFollowers => Set<CompanyFollower>();
    public DbSet<CompanyReview> CompanyReviews => Set<CompanyReview>();
    public DbSet<Connect> Connects => Set<Connect>();
    public DbSet<ConnectPurchase> ConnectPurchases => Set<ConnectPurchase>();
    public DbSet<ConnectUsage> ConnectUsages => Set<ConnectUsage>();
    public DbSet<FreelancerSubscription> FreelancerSubscriptions => Set<FreelancerSubscription>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<JobCategory> JobCategories => Set<JobCategory>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<JobSkill> JobSkills => Set<JobSkill>();
    public DbSet<JobQuestion> JobQuestions => Set<JobQuestion>();
    public DbSet<JobApplicationAttachment> JobApplicationAttachments => Set<JobApplicationAttachment>();
    public DbSet<CommunityPost> CommunityPosts => Set<CommunityPost>();
    public DbSet<PostComment> PostComments => Set<PostComment>();
    public DbSet<PostLike> PostLikes => Set<PostLike>();
    public DbSet<PostBookmark> PostBookmarks => Set<PostBookmark>();
    public DbSet<PostShare> PostShares => Set<PostShare>();
    public DbSet<ForumThread> ForumThreads => Set<ForumThread>();
    public DbSet<ForumCategory> ForumCategories => Set<ForumCategory>();
    public DbSet<ForumReply> ForumReplies => Set<ForumReply>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseLesson> CourseLessons => Set<CourseLesson>();
    public DbSet<CourseEnrollment> CourseEnrollments => Set<CourseEnrollment>();
    public DbSet<CourseReview> CourseReviews => Set<CourseReview>();
    public DbSet<LessonProgress> LessonProgresses => Set<LessonProgress>();
    public DbSet<LearningPath> LearningPaths => Set<LearningPath>();
    public DbSet<LearningPathCourse> LearningPathCourses => Set<LearningPathCourse>();
    public DbSet<Certification> Certifications => Set<Certification>();
    public DbSet<SkillMatch> SkillMatches => Set<SkillMatch>();
    public DbSet<ProjectMatch> ProjectMatches => Set<ProjectMatch>();
    public DbSet<JobMatch> JobMatches => Set<JobMatch>();
    public DbSet<FreelancerScore> FreelancerScores => Set<FreelancerScore>();
    public DbSet<ScoringRule> ScoringRules => Set<ScoringRule>();
    public DbSet<ScoringCriteria> ScoringCriterias => Set<ScoringCriteria>();
    public DbSet<Recommendation> Recommendations => Set<Recommendation>();
    public DbSet<AIModelConfig> AIModelConfigs => Set<AIModelConfig>();
    public DbSet<AILog> AILogs => Set<AILog>();
    public DbSet<HeadHunter> HeadHunters => Set<HeadHunter>();
    public DbSet<HeadHunterAssignment> HeadHunterAssignments => Set<HeadHunterAssignment>();
    public DbSet<TalentRecommendation> TalentRecommendations => Set<TalentRecommendation>();
    public DbSet<Guild> Guilds => Set<Guild>();
    public DbSet<GuildMember> GuildMembers => Set<GuildMember>();
    public DbSet<GuildProject> GuildProjects => Set<GuildProject>();
    public DbSet<CoachingSession> CoachingSessions => Set<CoachingSession>();
    public DbSet<CoachProfile> CoachProfiles => Set<CoachProfile>();
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();
    public DbSet<ConnectEarningRule> ConnectEarningRules => Set<ConnectEarningRule>();
    public DbSet<ConnectEarning> ConnectEarnings => Set<ConnectEarning>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
        });
        
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");
        });
        
        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        
        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        
        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        
        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.ToTable("UserTokens");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasOne(j => j.Category)
                .WithMany(jc => jc.Jobs)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(j => j.Company)
                .WithMany()
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<JobCategory>(entity =>
        {
            entity.HasOne(jc => jc.ParentCategory)
                .WithMany(jc => jc.SubCategories)
                .HasForeignKey(jc => jc.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}