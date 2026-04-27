using System;

namespace DEPI.Application.Common;

public interface ICurrentUserService
{
    Guid UserId { get; }
}