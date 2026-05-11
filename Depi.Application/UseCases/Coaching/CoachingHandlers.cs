using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Coaching;
using DEPI.Domain.Entities.Coaching;
using MediatR;
using DEPI.Application.DTOs.Coaching;
namespace DEPI.Application.UseCases.Coaching;



public record GetMySessionsQuery(Guid UserId) : IRequest<List<CoachingSessionResponse>>;
public record GetUpcomingSessionsQuery(Guid UserId) : IRequest<List<CoachingSessionResponse>>;
public record ScheduleSessionCommand(Guid StudentId, ScheduleSessionRequest Request) : IRequest<CoachingSessionResponse>;
public record CompleteSessionCommand(Guid CoachId, CompleteSessionRequest Request) : IRequest<CoachingSessionResponse>;
public record GetCoachesQuery : IRequest<List<CoachProfileResponse>>;
public record RegisterCoachCommand(Guid UserId, RegisterCoachRequest Request) : IRequest<CoachProfileResponse>;

public class GetMySessionsQueryHandler : IRequestHandler<GetMySessionsQuery, List<CoachingSessionResponse>>
{
    private readonly ICoachingSessionRepository _repo; private readonly IMapper _mapper;
    public GetMySessionsQueryHandler(ICoachingSessionRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<CoachingSessionResponse>> Handle(GetMySessionsQuery r, CancellationToken ct) => _mapper.Map<List<CoachingSessionResponse>>((await _repo.GetByCoachIdAsync(r.UserId)).Union(await _repo.GetByStudentIdAsync(r.UserId)).DistinctBy(s => s.Id));
}

public class GetUpcomingSessionsQueryHandler : IRequestHandler<GetUpcomingSessionsQuery, List<CoachingSessionResponse>>
{
    private readonly ICoachingSessionRepository _repo; private readonly IMapper _mapper;
    public GetUpcomingSessionsQueryHandler(ICoachingSessionRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<CoachingSessionResponse>> Handle(GetUpcomingSessionsQuery r, CancellationToken ct) => _mapper.Map<List<CoachingSessionResponse>>(await _repo.GetUpcomingAsync(r.UserId));
}

public class ScheduleSessionCommandHandler : IRequestHandler<ScheduleSessionCommand, CoachingSessionResponse>
{
    private readonly ICoachingSessionRepository _repo; private readonly IMapper _mapper;
    public ScheduleSessionCommandHandler(ICoachingSessionRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<CoachingSessionResponse> Handle(ScheduleSessionCommand r, CancellationToken ct)
    {
        var session = new CoachingSession { CoachId = r.Request.CoachId, StudentId = r.StudentId, SessionType = r.Request.SessionType, ScheduledAt = r.Request.ScheduledAt, Agenda = r.Request.Agenda };
        await _repo.AddAsync(session, ct);
        return _mapper.Map<CoachingSessionResponse>(session);
    }
}

public class CompleteSessionCommandHandler : IRequestHandler<CompleteSessionCommand, CoachingSessionResponse>
{
    private readonly ICoachingSessionRepository _repo; private readonly ICoachProfileRepository _coachRepo; private readonly IMapper _mapper;
    public CompleteSessionCommandHandler(ICoachingSessionRepository repo, ICoachProfileRepository coachRepo, IMapper mapper) { _repo = repo; _coachRepo = coachRepo; _mapper = mapper; }
    public async Task<CoachingSessionResponse> Handle(CompleteSessionCommand r, CancellationToken ct)
    {
        var session = await _repo.GetByIdAsync(r.Request.SessionId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Session"));
        session.Complete(r.Request.Notes, r.Request.Feedback, r.Request.ActionItems, r.Request.Rating);
        await _repo.UpdateAsync(session, ct);
        var coach = await _coachRepo.GetByUserIdAsync(session.CoachId);
        if (coach != null) { coach.TotalSessions++; coach.UpdateRating(r.Request.Rating); await _coachRepo.UpdateAsync(coach, ct); }
        return _mapper.Map<CoachingSessionResponse>(session);
    }
}

public class GetCoachesQueryHandler : IRequestHandler<GetCoachesQuery, List<CoachProfileResponse>>
{
    private readonly ICoachProfileRepository _repo; private readonly IMapper _mapper;
    public GetCoachesQueryHandler(ICoachProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<CoachProfileResponse>> Handle(GetCoachesQuery r, CancellationToken ct) => _mapper.Map<List<CoachProfileResponse>>(await _repo.GetAvailableCoachesAsync());
}

public class RegisterCoachCommandHandler : IRequestHandler<RegisterCoachCommand, CoachProfileResponse>
{
    private readonly ICoachProfileRepository _repo; private readonly IMapper _mapper;
    public RegisterCoachCommandHandler(ICoachProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<CoachProfileResponse> Handle(RegisterCoachCommand r, CancellationToken ct)
    {
        var existing = await _repo.GetByUserIdAsync(r.UserId);
        if (existing != null) throw new InvalidOperationException(Errors.AlreadyExists("CoachProfile"));
        var profile = new CoachProfile { UserId = r.UserId, Specialization = r.Request.Specialization, Bio = r.Request.Bio, YearsOfExperience = r.Request.YearsOfExperience, HourlyRate = r.Request.HourlyRate, Certifications = r.Request.Certifications ?? "" };
        await _repo.AddAsync(profile, ct);
        return _mapper.Map<CoachProfileResponse>(profile);
    }
}
