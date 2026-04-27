using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI.Domain.Common.Base;
using Depi.Domain.Modules.ProjectProposalsDelivery.Enums;


namespace Depi.Domain.Modules.ProjectProposalsDelivery.Entities
{
    public class ProjectAttachment : AuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        
        // Forign keys & Navigation Properties
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid MediaId { get; set; }
        //public MediaFile? Media { get; set; }
    }
}
