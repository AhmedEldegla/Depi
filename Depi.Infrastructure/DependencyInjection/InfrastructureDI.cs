using DEPI.Application.Interfaces;
using DEPI.Application.Repositories.AIMatching;
using DEPI.Application.Repositories.Coaching;
using DEPI.Application.Repositories.Companies;
using DEPI.Application.Repositories.Community;
using DEPI.Application.Repositories.Guilds;
using DEPI.Application.Repositories.HeadHunters;
using DEPI.Application.Repositories.Learning;
using DEPI.Application.Repositories.Recruitment;
using DEPI.Application.Repositories.Students;
using DEPI.Application.Repositories.Wallets;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Media;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Entities.Shared;
using DEPI.Domain.Interfaces;
using DEPI.Infrastructure.Persistence;
using DEPI.Infrastructure.Persistence.Repositories;
using DEPI.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DEPI.Infrastructure.DependencyInjection;

public static class InfrastructureDI
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;

            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped<UserManager<User>>();
        services.AddScoped<RoleManager<Role>>();
        services.AddScoped<SignInManager<User>>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IMilestoneRepository, MilestoneRepository>();
        services.AddScoped<IProposalRepository, ProposalRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IMediaFileRepository, MediaFileRepository>();
        services.AddScoped<IPortfolioItemRepository, PortfolioItemRepository>();
        services.AddScoped<IServicePackageRepository, ServicePackageRepository>();
        services.AddScoped<ITokenService, TokenService>();

        // Company repositories
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyMemberRepository, CompanyMemberRepository>();
        services.AddScoped<ICompanyProjectRepository, CompanyProjectRepository>();
        services.AddScoped<ICompanyFollowerRepository, CompanyFollowerRepository>();
        services.AddScoped<ICompanyReviewRepository, CompanyReviewRepository>();

        // Community repositories
        services.AddScoped<ICommunityPostRepository, CommunityPostRepository>();
        services.AddScoped<IPostCommentRepository, PostCommentRepository>();
        services.AddScoped<IPostLikeRepository, PostLikeRepository>();
        services.AddScoped<IPostBookmarkRepository, PostBookmarkRepository>();
        services.AddScoped<IForumThreadRepository, ForumThreadRepository>();
        services.AddScoped<IForumCategoryRepository, ForumCategoryRepository>();
        services.AddScoped<IForumReplyRepository, ForumReplyRepository>();

        // Learning repositories
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseLessonRepository, CourseLessonRepository>();
        services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();
        services.AddScoped<ILearningPathRepository, LearningPathRepository>();
        services.AddScoped<ILearningPathCourseRepository, LearningPathCourseRepository>();
        services.AddScoped<ICertificationRepository, CertificationRepository>();
        services.AddScoped<ICourseReviewRepository, CourseReviewRepository>();
        services.AddScoped<ILessonProgressRepository, LessonProgressRepository>();

        // Recruitment repositories
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
        services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
        services.AddScoped<IJobSkillRepository, JobSkillRepository>();
        services.AddScoped<IJobQuestionRepository, JobQuestionRepository>();

        // AI Matching repositories
        services.AddScoped<ISkillMatchRepository, SkillMatchRepository>();
        services.AddScoped<IProjectMatchRepository, ProjectMatchRepository>();
        services.AddScoped<IJobMatchRepository, JobMatchRepository>();
        services.AddScoped<IFreelancerScoreRepository, FreelancerScoreRepository>();
        services.AddScoped<IScoringRuleRepository, ScoringRuleRepository>();
        services.AddScoped<IRecommendationRepository, RecommendationRepository>();
        services.AddScoped<IAIModelConfigRepository, AIModelConfigRepository>();
        services.AddScoped<IAILogRepository, AILogRepository>();

        // Connect repositories
        services.AddScoped<IConnectRepository, ConnectRepository>();
        services.AddScoped<IConnectPurchaseRepository, ConnectPurchaseRepository>();
        services.AddScoped<IConnectUsageRepository, ConnectUsageRepository>();
        services.AddScoped<IFreelancerSubscriptionRepository, FreelancerSubscriptionRepository>();
        services.AddScoped<IConnectEarningRuleRepository, ConnectEarningRuleRepository>();
        services.AddScoped<IConnectEarningRepository, ConnectEarningRepository>();

        // Head Hunter repositories
        services.AddScoped<IHeadHunterRepository, HeadHunterRepository>();
        services.AddScoped<IHeadHunterAssignmentRepository, HeadHunterAssignmentRepository>();
        services.AddScoped<ITalentRecommendationRepository, TalentRecommendationRepository>();

        // Guild repositories
        services.AddScoped<IGuildRepository, GuildRepository>();
        services.AddScoped<IGuildMemberRepository, GuildMemberRepository>();
        services.AddScoped<IGuildProjectRepository, GuildProjectRepository>();

        // Coaching repositories
        services.AddScoped<ICoachingSessionRepository, CoachingSessionRepository>();
        services.AddScoped<ICoachProfileRepository, CoachProfileRepository>();

        // Student repositories
        services.AddScoped<IStudentProfileRepository, StudentProfileRepository>();

        return services;
    }
}