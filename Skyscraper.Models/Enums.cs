namespace Avalara.Skyscraper.Models
{
    public enum Roles
    {
        Admin = 1,
        SuperVisor = 2,
        BasicUser = 3
    }

    public enum JobStatus
    {
        CANCELLED = -1,
        UNPROCESSED = 0,
        PROCESSING = 1,
        SCRAPING = 2,
        SUCCESS = 3,
        FAILED = 4,
        WEBFILING = 5,
        AWAITINGCONFIRMATION = 6
    }
    public enum JobType
    {
        CUSTOMERDORDATA = 1,
        WEBFILE = 2,
        LOGIN = 3,
        BulkAccountCustomerCheck = 4,
        VATREGISTRATION = 5,
        TAXIDVALIDATION = 6,
        WEBUPLOAD = 7,
        LOCATIONREGISTRATION = 8
    }

}
