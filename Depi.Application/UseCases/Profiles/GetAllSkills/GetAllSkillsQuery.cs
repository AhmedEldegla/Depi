using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.GetAllSkills;
public record GetAllSkillsQuery(bool? IsActive = null) : IRequest<IEnumerable<SkillResponse>>;