namespace DEPI.Application.DTOs.Connects;

public record SpendConnectsRequest(string UsageType, Guid? ProjectId, Guid? JobId, int ConnectsUsed);