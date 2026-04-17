using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI.Domain.Common.Base;
using Depi.Domain.Modules.ProjectProposalsDelivery.Enums;



namespace Depi.Domain.Modules.ProjectProposalsDelivery.Entities
{
    public class Contract : AuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public ContractStatus? Status { get; set; }

        // Forign keys & Navigation Properties
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid? ProposalId { get; set; }
        public Proposal? Proposal { get; set; }
        public Guid FreelancerId { get; set; }
        //public Freelancer Freelancer { get; set; }
        public ICollection<Milestone> Milestones { get; set; } = new HashSet<Milestone>();
        public ICollection<Deliverable> Deliverables { get; set; } = new HashSet<Deliverable>();

    }
}
