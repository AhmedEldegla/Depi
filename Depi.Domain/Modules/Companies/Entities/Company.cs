using Depi.Domain.Modules.ProjectProposalsDelivery.Entities;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DEPI.Domain.Modules.Companies.Entities
{
    public class Company : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Industry {  get; set; } = string.Empty ;
        public string Overview {  get; set; } = string.Empty ;

        // Forign keys and Navigation Properties
        //Remove the comments once all entities are created

        public Guid OwnerUserId { get; set; }
        public User? User { get; set; }
        public Guid CountryId { get; set; }
        //public Countries? Country { get; set; }
        public Guid LogoMediaId { get; set; }
        // public MediaFiles? LogoMedia { get; set; }


        public ICollection<CompanyMember> CompanyMembers { get; set;} = new HashSet<CompanyMember>();

        // Add Project module Relations
        public ICollection<Project> projects { get; set; } = new HashSet<Project>();















    }
}


