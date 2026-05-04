namespace DEPI.Domain.Enums;

public enum DocumentType
{
    NationalID = 1,
    Passport = 2,
    DriversLicense = 3,
    ResidencePermit = 4
}

public enum VerificationStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3
}
