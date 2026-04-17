using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Payments.Entities
{
    public class Wallet : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public decimal PendingBalance { get; set; }
        public string Currency { get; set; } = "EGP";
        public bool IsVerified { get; set; }
        public User User { get; set; } = null!;
    }
}
