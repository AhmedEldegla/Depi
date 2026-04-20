using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Companies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Domain.Modules.Companies.Entities
{
    public class CompanyArtifact :AuditableEntity
    {
        public string ArtifactType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        // Foreign Keys and Navigation Properties
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
        public Guid? MediaId { get; set; }
        //public MediaFiles? Media { get; set; }
    }



}
