using DEPI.Application.Repositories.AIMatching;
using DEPI.Domain.Entities.AIMatching;
using MediatR;

namespace DEPI.Application.UseCases.AIMatching;

public class FreelancerScoreResponse
{
    public Guid Id { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ProjectSuccessScore { get; set; }
    public decimal CommunicationScore { get; set; }
    public decimal ReliabilityScore { get; set; }
    public decimal QualityScore { get; set; }
    public decimal ClientSatisfactionScore { get; set; }
    public decimal CompletionRateScore { get; set; }
    public int TotalProjects { get; set; }
    public int CompletedProjects { get; set; }
    public decimal AverageRating { get; set; }
    public DateTime CalculatedAt { get; set; }
}

public class ProjectMatchResponse
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public string FreelancerName { get; set; } = string.Empty;
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public decimal BudgetScore { get; set; }
    public string AIReasoning { get; set; } = string.Empty;
    public MatchStatus Status { get; set; }
    public int Rank { get; set; }
    public DateTime CalculatedAt { get; set; }
}

public class RecommendationResponse
{
    public Guid Id { get; set; }
    public RecommendationType Type { get; set; }
    public string TargetType { get; set; } = string.Empty;
    public Guid TargetId { get; set; }
    public decimal ConfidenceScore { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public record GetProjectMatchesQuery(Guid ProjectId, int? Top) : IRequest<List<ProjectMatchResponse>>;
public record GetFreelancerScoreQuery(Guid FreelancerId) : IRequest<FreelancerScoreResponse?>;
public record GetTopFreelancersQuery(int Count) : IRequest<List<FreelancerScoreResponse>>;
public record GetRecommendationsQuery(Guid UserId) : IRequest<List<RecommendationResponse>>;

public class GetProjectMatchesQueryHandler : IRequestHandler<GetProjectMatchesQuery, List<ProjectMatchResponse>>
{
    private readonly IProjectMatchRepository _repo;
    public GetProjectMatchesQueryHandler(IProjectMatchRepository repo) => _repo = repo;

    public async Task<List<ProjectMatchResponse>> Handle(GetProjectMatchesQuery r, CancellationToken ct)
    {
        List<ProjectMatch> matches;
        if (r.Top.HasValue)
            matches = await _repo.GetTopMatchesAsync(r.ProjectId, r.Top.Value);
        else
            matches = await _repo.GetByProjectIdAsync(r.ProjectId);

        return matches.Select(m => new ProjectMatchResponse
        {
            Id = m.Id, ProjectId = m.ProjectId, OverallScore = m.OverallScore,
            SkillScore = m.SkillScore, ExperienceScore = m.ExperienceScore,
            BudgetScore = m.BudgetScore, AIReasoning = m.AIReasoning,
            Status = m.Status, Rank = m.Rank, CalculatedAt = m.CalculatedAt
        }).ToList();
    }
}

public class GetFreelancerScoreQueryHandler : IRequestHandler<GetFreelancerScoreQuery, FreelancerScoreResponse?>
{
    private readonly IFreelancerScoreRepository _repo;
    public GetFreelancerScoreQueryHandler(IFreelancerScoreRepository repo) => _repo = repo;

    public async Task<FreelancerScoreResponse?> Handle(GetFreelancerScoreQuery r, CancellationToken ct)
    {
        var score = await _repo.GetByFreelancerIdAsync(r.FreelancerId);
        if (score == null) return null;
        return new FreelancerScoreResponse
        {
            Id = score.Id, OverallScore = score.OverallScore, SkillScore = score.SkillScore,
            ProjectSuccessScore = score.ProjectSuccessScore, CommunicationScore = score.CommunicationScore,
            ReliabilityScore = score.ReliabilityScore, QualityScore = score.QualityScore,
            ClientSatisfactionScore = score.ClientSatisfactionScore,
            CompletionRateScore = score.CompletionRateScore, TotalProjects = score.TotalProjects,
            CompletedProjects = score.CompletedProjects, AverageRating = score.AverageRating,
            CalculatedAt = score.CalculatedAt
        };
    }
}

public class GetTopFreelancersQueryHandler : IRequestHandler<GetTopFreelancersQuery, List<FreelancerScoreResponse>>
{
    private readonly IFreelancerScoreRepository _repo;
    public GetTopFreelancersQueryHandler(IFreelancerScoreRepository repo) => _repo = repo;

    public async Task<List<FreelancerScoreResponse>> Handle(GetTopFreelancersQuery r, CancellationToken ct)
    {
        var scores = await _repo.GetTopScoredAsync(r.Count);
        return scores.Select(s => new FreelancerScoreResponse
        {
            Id = s.Id, OverallScore = s.OverallScore, SkillScore = s.SkillScore,
            ProjectSuccessScore = s.ProjectSuccessScore, CommunicationScore = s.CommunicationScore,
            ReliabilityScore = s.ReliabilityScore, QualityScore = s.QualityScore,
            ClientSatisfactionScore = s.ClientSatisfactionScore,
            CompletionRateScore = s.CompletionRateScore, TotalProjects = s.TotalProjects,
            CompletedProjects = s.CompletedProjects, AverageRating = s.AverageRating,
            CalculatedAt = s.CalculatedAt
        }).ToList();
    }
}

public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, List<RecommendationResponse>>
{
    private readonly IRecommendationRepository _repo;
    public GetRecommendationsQueryHandler(IRecommendationRepository repo) => _repo = repo;

    public async Task<List<RecommendationResponse>> Handle(GetRecommendationsQuery r, CancellationToken ct)
    {
        var recs = await _repo.GetActiveForUserAsync(r.UserId);
        return recs.Select(rec => new RecommendationResponse
        {
            Id = rec.Id, Type = rec.Type, TargetType = rec.TargetType,
            TargetId = rec.TargetId, ConfidenceScore = rec.ConfidenceScore,
            Reason = rec.Reason, CreatedAt = rec.CreatedAt
        }).ToList();
    }
}
