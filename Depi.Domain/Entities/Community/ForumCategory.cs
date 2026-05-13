using System;
using System.Collections.Generic;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Companies;
using DEPI.Domain.Enums;
using DEPI.Domain.Entities.Community;

namespace Depi.Domain.Entities.Community
{
    public class ForumCategory : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = "#007bff";
        public bool IsActive { get; set; } = true;
        public int ThreadsCount { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<ForumThread> Threads { get; set; } = new List<ForumThread>();
    }
}
