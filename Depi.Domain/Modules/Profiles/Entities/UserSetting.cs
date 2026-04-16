using DEPI.Domain.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Profiles.Entities
{
    public class UserSetting : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }
}
