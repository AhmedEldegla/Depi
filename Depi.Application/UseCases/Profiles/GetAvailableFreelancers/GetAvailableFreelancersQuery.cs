using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.GetAvailableFreelancers;
public record GetAvailableFreelancersQuery(string? Search, int Page, int PageSize) : IRequest<(IEnumerable<UserProfileResponse> Items, int TotalCount)>;