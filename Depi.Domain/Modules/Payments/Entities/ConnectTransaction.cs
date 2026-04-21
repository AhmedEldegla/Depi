using Depi.Domain.Modules.Payments.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Payments.Entities
{
    public class ConnectTransaction : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid? ConnectPackageId { get; set; }
        public TransactionType Type { get; set; }
        public int ConnectAmount { get; set; }
        public decimal Amount { get; set; }
        public Guid? ProjectId { get; set; }
        public User User { get; set; } = null!;
    }
}
