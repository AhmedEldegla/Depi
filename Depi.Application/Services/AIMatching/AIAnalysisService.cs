using DEPI.Application.Interfaces;
using DEPI.Application.Repositories.AIMatching;
using DEPI.Application.Repositories.Profiles;
using DEPI.Application.Repositories.Projects;
using DEPI.Domain.Entities.AIMatching;
using DEPI.Domain.Entities.Profiles;
using IProjectRepository = DEPI.Application.Repositories.Projects.IProjectRepository;
using ISkillRepository = DEPI.Application.Interfaces.ISkillRepository;

namespace DEPI.Application.Services.AIMatching;

public class AIAnalysisService : IAIAnalysisService
{
    private readonly IFreelancerScoringService _scoringService;
    private readonly IFreelancerProfileRepository _profileRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IAIModelConfigService _configService;
    private readonly IAILogRepository _logRepository;

    public AIAnalysisService(
        IFreelancerScoringService scoringService,
        IFreelancerProfileRepository profileRepository,
        ISkillRepository skillRepository,
        IProjectRepository projectRepository,
        IAIModelConfigService configService,
        IAILogRepository logRepository)
    {
        _scoringService = scoringService;
        _profileRepository = profileRepository;
        _skillRepository = skillRepository;
        _projectRepository = projectRepository;
        _configService = configService;
        _logRepository = logRepository;
    }

    public async Task<string> AnalyzeProposalAsync(string coverLetter, Guid projectId)
    {
        var startTime = DateTime.UtcNow;
        var project = await _projectRepository.GetByIdAsync(projectId);
        
        var analysis = new System.Text.StringBuilder();
        
        analysis.AppendLine("=== AI Proposal Analysis ===");
        
        if (coverLetter.Length < 100)
        {
            analysis.AppendLine("- Warning: Cover letter is too short");
        }
        
        if (project != null)
        {
            var requiredSkills = project.Skills?.Split(',') ?? Array.Empty<string>();
            foreach (var skill in requiredSkills)
            {
                if (coverLetter.Contains(skill.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    analysis.AppendLine($"+ Mentions required skill: {skill.Trim()}");
                }
            }
            
            if (coverLetter.Contains("$") || coverLetter.Contains("USD"))
            {
                analysis.AppendLine("+ Includes budget information");
            }
            
            if (coverLetter.Contains("experience", StringComparison.OrdinalIgnoreCase) ||
                coverLetter.Contains("years", StringComparison.OrdinalIgnoreCase))
            {
                analysis.AppendLine("+ Mentions relevant experience");
            }
        }

        var wordCount = coverLetter.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        analysis.AppendLine($"- Word count: {wordCount}");
        
        var response = analysis.ToString();
        
        await LogAIAnalysisAsync("AnalyzeProposal", coverLetter, response, startTime);
        
        return response;
    }

    public async Task<string> AnalyzeFreelancerProfileAsync(Guid freelancerId)
    {
        var startTime = DateTime.UtcNow;
        var profile = await _profileRepository.GetByUserIdAsync(freelancerId);
        var skills = await _skillRepository.GetAllAsync();
        
        var analysis = new System.Text.StringBuilder();
        
        analysis.AppendLine("=== AI Profile Analysis ===");
        
        if (profile == null)
        {
            return "Profile not found";
        }
        
        var completionScore = CalculateCompletionScore(profile);
        analysis.AppendLine($"- Profile Completion: {completionScore:P0}");
        
        var skillsList = skills.ToList();
        if (skillsList.Any())
        {
            analysis.AppendLine($"- Skills Count: {skillsList.Count}");
            analysis.AppendLine($"- Top Skills: {string.Join(", ", skillsList.Take(5).Select(s => s.Name))}");
        }
        
        analysis.AppendLine($"- Experience Level: {profile.ExperienceLevel}");
        analysis.AppendLine($"- Availability: {(profile.IsAvailable ? "Available" : "Not Available")}");
        
        var strengths = new List<string>();
        var weaknesses = new List<string>();
        
        if (profile.CompletedProjects > 10)
            strengths.Add("Strong project history");
        else if (profile.CompletedProjects < 3)
            weaknesses.Add("Limited project experience");
            
        if (skillsList.Count > 10)
            strengths.Add("Diverse skill set");
        else if (skillsList.Count < 3)
            weaknesses.Add("Limited skills");
            
        if (profile.HourlyRate > 100)
            strengths.Add("Premium rate suggests expertise");
            
        if (strengths.Any())
            analysis.AppendLine($"+ Strengths: {string.Join(", ", strengths)}");
            
        if (weaknesses.Any())
            analysis.AppendLine($"- Areas for Improvement: {string.Join(", ", weaknesses)}");

        var response = analysis.ToString();
        
        await LogAIAnalysisAsync("AnalyzeFreelancerProfile", freelancerId.ToString(), response, startTime);
        
        return response;
    }

    public async Task<string> GenerateProjectDescriptionAsync(string title, string category)
    {
        var startTime = DateTime.UtcNow;
        
        var description = $"Project in {category}: {title}\n\n" +
            "This project requires the following:\n" +
            "- Clear project objectives and deliverables\n" +
            "- Defined timeline and milestones\n" +
            "- Communication plan and progress updates\n" +
            "- Quality assurance and testing requirements";
        
        await LogAIAnalysisAsync("GenerateProjectDescription", $"{title}|{category}", description, startTime);
        
        return description;
    }

    public async Task<string> EvaluatePortfolioItemAsync(string description, List<string> skills)
    {
        var startTime = DateTime.UtcNow;
        
        var evaluation = new System.Text.StringBuilder();
        
        evaluation.AppendLine("=== AI Portfolio Evaluation ===");
        
        if (description.Length > 100)
        {
            evaluation.AppendLine("+ Detailed description provided");
        }
        else
        {
            evaluation.AppendLine("- Description could be more detailed");
        }
        
        var matchingSkills = new List<string>();
        foreach (var skill in skills)
        {
            if (description.Contains(skill, StringComparison.OrdinalIgnoreCase))
            {
                matchingSkills.Add(skill);
            }
        }
        
        if (matchingSkills.Any())
        {
            evaluation.AppendLine($"+ Skills demonstrated: {string.Join(", ", matchingSkills)}");
        }
        
        var score = (description.Length / 500.0) + (matchingSkills.Count * 0.1);
        score = Math.Min(score, 1.0);
        
        evaluation.AppendLine($"- Overall Quality Score: {score:P0}");
        
        var response = evaluation.ToString();
        
        await LogAIAnalysisAsync("EvaluatePortfolioItem", description, response, startTime);
        
        return response;
    }

    public async Task<decimal> CalculateSkillProficiencyAsync(Guid freelancerId, Guid skillId)
    {
        var skills = await _skillRepository.GetAllAsync();
        var skill = skills.FirstOrDefault(s => s.Id == skillId);
        
        if (skill == null) return 0;
        
        return 0.7m;
    }

    public async Task<string> GenerateMatchReasoningAsync(MatchContext context)
    {
        var reasoning = new System.Text.StringBuilder();
        
        reasoning.AppendLine("=== AI Match Analysis ===");
        reasoning.AppendLine($"Overall Compatibility: {context.OverallScore:P0}");
        
        reasoning.AppendLine("\n** Score Breakdown:");
        reasoning.AppendLine($"- Skills Match: {context.SkillScore:P0}");
        reasoning.AppendLine($"- Experience Match: {context.ExperienceScore:P0}");
        
        if (context.MatchingSkills.Any())
        {
            reasoning.AppendLine($"\n** Matching Skills:");
            foreach (var skill in context.MatchingSkills)
            {
                reasoning.AppendLine($"  + {skill}");
            }
        }
        
        if (context.MissingSkills.Any())
        {
            reasoning.AppendLine($"\n** Skills Gap:");
            foreach (var skill in context.MissingSkills)
            {
                reasoning.AppendLine($"  - {skill}");
            }
        }
        
        if (!string.IsNullOrEmpty(context.FreelancerStrengths))
        {
            reasoning.AppendLine($"\n** Strengths: {context.FreelancerStrengths}");
        }
        
        if (!string.IsNullOrEmpty(context.FreelancerWeaknesses))
        {
            reasoning.AppendLine($"\n** Considerations: {context.FreelancerWeaknesses}");
        }
        
        return reasoning.ToString();
    }

    private decimal CalculateCompletionScore(UserProfile profile)
    {
        var score = 0m;
        
        if (!string.IsNullOrEmpty(profile.ExperienceLevel)) score += 0.15m;
        if (profile.CompletedProjects > 0) score += 0.2m;
        if (!string.IsNullOrEmpty(profile.CountryName)) score += 0.1m;
        if (profile.ProfileCompletion > 50) score += 0.25m;
        if (profile.HourlyRate > 0) score += 0.15m;
        if (profile.IsAvailable) score += 0.15m;
        
        return Math.Min(score, 1.0m);
    }

    private async Task LogAIAnalysisAsync(string action, string input, string output, DateTime startTime)
    {
        var processingTime = (long)(DateTime.UtcNow - startTime).TotalMilliseconds;
        
        var config = await _configService.GetActiveConfigurationAsync();
        
        var log = new AILog
        {
            Action = action,
            Input = input.Length > 1000 ? input.Substring(0, 1000) : input,
            Output = output.Length > 5000 ? output.Substring(0, 5000) : output,
            Model = $"{config.Provider}:{config.ModelId}",
            ProcessingTimeMs = processingTime,
            IsSuccess = true
        };
        
        await _logRepository.AddAsync(log);
    }
}