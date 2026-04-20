using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI.Domain.Common.Base;
using Depi.Domain.Modules.ProjectProposalsDelivery.Enums;
using DEPI.Domain.Modules.Identity.Entities;
using DEPI.Domain.Modules.Companies.Entities;
using System.ComponentModel;


namespace Depi.Domain.Modules.ProjectProposalsDelivery.Entities
{
    public class Project : AuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        public decimal? BudgetMin { get; set; }
        public decimal? BudgetMax { get; set; }
        public ProjectStatus Status { get; set; }

        // Forign keys & Navigation Properties
        public Guid ClientUserId { get; set; }
        public User ClientUser { get; set; }
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
        public Guid CategoryId { get; set; }
        //public Category Category { get; set; }

        public ICollection<ProjectAttachment> ProjectAttachments { get; set; } = new HashSet<ProjectAttachment>();
        public ICollection<Proposal> Proposals { get; set; } = new HashSet<Proposal>();
        public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();



    }
}
