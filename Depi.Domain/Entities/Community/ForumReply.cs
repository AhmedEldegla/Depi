using System;
using System.Collections.Generic;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Companies;
using DEPI.Domain.Enums;
using DEPI.Domain.Entities.Community;

namespace Depi.Domain.Entities.Community
{
    public class ForumReply : AuditableEntity
    {
        public Guid ThreadId { get; set; }
        public Guid AuthorId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsAcceptedAnswer { get; set; }
        public bool IsEdited { get; set; }
        public int LikesCount { get; set; }
        public int Depth { get; set; }

        public virtual ForumThread Thread { get; set; } = null!;
        public virtual User Author { get; set; } = null!;
    }
}
