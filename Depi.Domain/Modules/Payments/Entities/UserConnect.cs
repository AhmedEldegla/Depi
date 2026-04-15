using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Payments.Entities
{
    public class UserConnect : BaseEntity
    {
        public Guid UserId { get; set; }
        public int TotalConnects { get; set; }
        public int UsedConnects { get; set; }
        public int RemainingConnects { get; set; }
        public User User { get; set; } = null!;
    }
}
