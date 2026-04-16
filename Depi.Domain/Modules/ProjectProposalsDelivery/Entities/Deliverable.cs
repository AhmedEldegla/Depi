using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI.Domain.Common.Base;
using Depi.Domain.Modules.ProjectProposalsDelivery.Enums;


namespace Depi.Domain.Modules.ProjectProposalsDelivery.Entities
{
    public class Deliverable : AuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? SubmissionNote { get; set; }
        public DeliverableStatus Status { get; set; }

        // Forign keys & Navigation Properties
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
        public Guid? MilestoneId { get; set; }
        public Milestone? Milestone { get; set; }
    }
}
