using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Payments.Entities
{
    public class ConnectPackage : BaseEntity
    {
        public string Name { get; set; }
        public int ConnectCount { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
