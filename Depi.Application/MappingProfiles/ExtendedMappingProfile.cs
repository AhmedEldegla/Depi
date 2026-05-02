using AutoMapper;
using DEPI.Application.UseCases.Companies;
using DEPI.Application.UseCases.Community;
using DEPI.Application.UseCases.Learning;
using DEPI.Application.UseCases.Recruitment;
using DEPI.Application.UseCases.Guilds;
using DEPI.Application.UseCases.HeadHunters;
using DEPI.Application.UseCases.Coaching;
using DEPI.Application.UseCases.Students;
using DEPI.Application.UseCases.Connects;
using DEPI.Application.UseCases.AIMatching;
using DEPI.Domain.Entities.Companies;
using DEPI.Domain.Entities.Community;
using DEPI.Domain.Entities.Learning;
using DEPI.Domain.Entities.Recruitment;
using DEPI.Domain.Entities.Guilds;
using DEPI.Domain.Entities.HeadHunters;
using DEPI.Domain.Entities.Coaching;
using DEPI.Domain.Entities.Students;
using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Entities.AIMatching;

namespace DEPI.Application.MappingProfiles;

public class ExtendedMappingProfile : Profile
{
    public ExtendedMappingProfile()
    {
        CreateMap<Company, CompanyResponse>();

        CreateMap<CommunityPost, CommunityPostResponse>();
        CreateMap<ForumThread, ForumThreadResponse>();

        CreateMap<Course, CourseResponse>();
        CreateMap<CourseLesson, CourseLessonResponse>();
        CreateMap<LessonProgress, LessonProgressResponse>();
        CreateMap<LearningPath, LearningPathResponse>();
        CreateMap<Certification, CertificationResponse>();
        CreateMap<CourseReview, CourseReviewResponse>();

        CreateMap<Job, JobResponse>();

        CreateMap<Guild, GuildResponse>();
        CreateMap<GuildMember, GuildMemberResponse>();

        CreateMap<HeadHunter, HeadHunterResponse>();
        CreateMap<HeadHunterAssignment, AssignmentResponse>();
        CreateMap<TalentRecommendation, TalentRecommendationResponse>();

        CreateMap<CoachingSession, CoachingSessionResponse>();
        CreateMap<CoachProfile, CoachProfileResponse>();

        CreateMap<StudentProfile, StudentProfileResponse>();

        CreateMap<Connect, ConnectPackResponse>();
        CreateMap<ConnectPurchase, ConnectPurchaseResponse>();
        CreateMap<ConnectUsage, ConnectUsageResponse>();
        CreateMap<ConnectEarning, ConnectEarningResponse>();
        CreateMap<ConnectEarningRule, EarningRuleResponse>();

        CreateMap<ProjectMatch, ProjectMatchResponse>();
        CreateMap<FreelancerScore, FreelancerScoreResponse>();
        CreateMap<Recommendation, RecommendationResponse>();
    }
}
