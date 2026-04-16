using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI.Domain.Common.Base;
using Depi.Domain.Modules.ProjectProposalsDelivery.Enums;



namespace Depi.Domain.Modules.ProjectProposalsDelivery.Entities
{
    public class Proposal : AuditableEntity
    {
        public string CoverLetter { get; set; } = string.Empty;
        public decimal ProposedAmount { get; set; }
        public int? DeliveryDays { get; set; }
        public ProposalStatus Status { get; set; }

        // Forign keys & Navigation Properties
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid FreelancerId { get; set; }
        //public Freelancer Freelancer { get; set; }
        public Contract? Contract { get; set; }


    }
}
