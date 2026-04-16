using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Communication.Entities
{
    public class ConversationParticipant : BaseEntity
    {
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime? LastReadAt { get; set; }

        // Navigation Properties
        public Conversation Conversation { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
