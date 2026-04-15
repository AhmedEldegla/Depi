using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using DEPI.Domain.Modules.Country.Entities;


namespace DEPI.Domain.Modules.Profiles.Entities
{
    public class UserProfile : BaseEntity
    {
        public Guid UserId { get; set; }

        public string? Bio { get; set; }
        public string? Headline { get; set; }
        public string? Summary { get; set; }
        public decimal? HourlyRate { get; set; }

        public Guid? CountryId { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Timezone { get; set; }
        public int? Availability { get; set; }

        public User User { get; set; } = null!;
        public Country? Country { get; set; }
    }
}