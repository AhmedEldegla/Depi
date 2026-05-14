using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI.Domain.Entities.Messaging;

namespace Depi.Domain.Enums
{
    public enum NotificationType
    {
        System = 1,
        Message = 2,
        Proposal = 3,
        Contract = 4,
        Milestone = 5,
        Payment = 6,
        Review = 7,
        Project = 8
    }
}
