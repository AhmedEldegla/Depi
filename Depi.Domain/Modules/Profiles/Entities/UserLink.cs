using DEPI.Domain.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Profiles.Entities
{
    public class UserLink : BaseEntity
    {
        public Guid UserId { get; set; }
        public LinkType Type { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
    }
}
