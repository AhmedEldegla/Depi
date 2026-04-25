using Microsoft.AspNetCore.Identity;

namespace DEPI.Domain.Entities.Identity;

public class UserClaim : IdentityUserClaim<Guid>
{
}

public class UserLogin : IdentityUserLogin<Guid>
{
}

public class RoleClaim : IdentityRoleClaim<Guid>
{
}

public class UserToken : IdentityUserToken<Guid>
{
}