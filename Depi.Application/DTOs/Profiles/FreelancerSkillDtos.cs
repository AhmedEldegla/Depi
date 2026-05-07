namespace DEPI.Application.DTOs.Profiles;

public class FreelancerSkillResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SkillId { get; set; }
    public string SkillName { get; set; } = string.Empty;
    public int ProficiencyLevel { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsVerified { get; set; }
    public string? VerificationProof { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}