using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI.Domain.Common.Base;
using Depi.Domain.Modules.ProjectProposalsDelivery.Enums;


namespace Depi.Domain.Modules.ProjectProposalsDelivery.Entities
{
    public class Milestone : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateOnly? DueDate { get; set; }
        public ContractMilestoneStaues Staues { get; set; }

        // Forign keys & Navigation Properties
        public Guid ContractId { get; set; }
        public Contract Contract {  get; set; }
        public ICollection<Deliverable> Deliverables { get; set; } = new HashSet<Deliverable>();




    }
}
