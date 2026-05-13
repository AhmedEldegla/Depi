using System;
using System.Collections.Generic;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Companies;
using Depi.Domain.Enums;
using DEPI.Domain.Entities.Community;

namespace Depi.Domain.Entities.Community
{
    public class ForumThread : AuditableEntity
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public ThreadStatus Status { get; set; } = ThreadStatus.Open;
        public int ViewsCount { get; set; }
        public int RepliesCount { get; set; }
        public int LikesCount { get; set; }
        public bool IsPinned { get; set; }
        public bool IsLocked { get; set; }
        public bool IsSolved { get; set; }
        public string Tags { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        public virtual User Author { get; set; } = null!;
        public virtual ForumCategory Category { get; set; } = null!;
        public virtual ICollection<ForumReply> Replies { get; set; } = new List<ForumReply>();
    }

}
