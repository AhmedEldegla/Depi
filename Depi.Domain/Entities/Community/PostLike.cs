using System;
using System;
using System.Collections.Generic;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Companies;
using Depi.Domain.Enums;
using DEPI.Domain.Entities.Community;

namespace Depi.Domain.Entities.Community
{
    public class PostLike : AuditableEntity
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

        public virtual CommunityPost Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
