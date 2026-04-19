namespace Depi.Domain.Modules.Projects.Enums;

public enum ProjectStatus
{
    Draft = 1,
    Open = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5,
    Closed = 6
}

public enum ProjectType
{
    FixedPrice = 1,
    Hourly = 2,
    Contest = 3
}

public enum ExperienceLevel
{
    Entry = 1,
    Intermediate = 2,
    Expert = 3
}

public enum ContractStatus
{
    Draft = 1,
    Active = 2,
    Paused = 3,
    Completed = 4,
    Cancelled = 5,
    InDispute = 6
}

public enum ProposalStatus
{
    Pending = 1,
    Shortlisted = 2,
    Interview = 3,
    Accepted = 4,
    Rejected = 5,
    Withdrawn = 6
}

public enum MilestoneStatus
{
    Pending = 1,
    InProgress = 2,
    Submitted = 3,
    Approved = 4,
    NeedsRevision = 5,
    Cancelled = 6
}

public enum DeliverableStatus
{
    Pending = 1,
    Submitted = 2,
    Approved = 3,
    NeedsRevision = 4
}
