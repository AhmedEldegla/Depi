using Depi.Domain.Modules.Communication.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Communication.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public Guid? RelatedEntityId { get; set; }  // nullable
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
    }
}
