using Depi.Domain.Modules.Communication.Enums;
using DEPI.Domain.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Communication.Entities
{
    public class Conversation : BaseEntity
    {
        public ConversationType Type { get; set; }
        public string? Title { get; set; }
        public DateTime LastMessageAt { get; set; }

        // Navigation Properties
        public ICollection<ConversationParticipant> Participants
        { get; set; } = [];
        public ICollection<Message> Messages { get; set; } = [];
    }
}
