using System;
using System.ComponentModel.DataAnnotations;
using Avalara.Skyscraper.Data.Dapper.Base;
 
namespace Avalara.Skyscraper.Data.Dapper.Entities
{

	public partial class ApiStatus : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ApiName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Method { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 ModifiedBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime ModifiedDate { get; set; }

	}

	public partial class BulkAccount : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 BulkAccountId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Username { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Password { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 CreatedUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreatedDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 ModifiedUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? ModifiedDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean? IsDefault { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean? IsActive { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? MaxJobs { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? CurrentJobs { get; set; }

	}

	public partial class BulkAccountHistory : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 BulkAccountId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String OldPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String NewPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? ModifiedDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? ModifiedUserId { get; set; }

	}

	public partial class BulkAccountUserMetaData : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 BulkAccountId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String MetaDataJson { get; set; }

	}

	public partial class BulkRequest : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 BulkRequestId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreateDateTime { get; set; }

	}

	public partial class ClientAPIKeys : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String APIKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String AppName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? DepartmentId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? ExpirationDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? ModifiedDate { get; set; }

	}

	public partial class CustomerDORData : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String DORData { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime CreateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean? WebsiteLoginSuccess { get; set; }

	}

	public partial class DedicatedInstanceStates : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String UDF1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? UDF2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? MaxWorkers { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? CurrentWorkers { get; set; }

	}

	public partial class DedicatedInstances : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String InstanceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String HostName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String UDF1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? UDF2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public SByte? UDF3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String UDF4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? UDF5 { get; set; }

	}

	public partial class Department : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String DepartmentName { get; set; }

	}

	public partial class DepartmentBulkAccount : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 BulkAccountId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 DepartmentId { get; set; }

	}

	public partial class DepartmentPaymentInfo : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 PaymentInfoId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 DepartmentId { get; set; }

	}

	public partial class FilingMode : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 FilingModeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Description { get; set; }

	}

	public partial class IndividualAccountUserMetadata : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Username { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String MetadataJson { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? UDF1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String UDF2 { get; set; }

	}

	public partial class JobData : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 CompanyId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 AccountId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Username { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Password { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String CustomerIdentifier { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Mode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String AdditionalOptions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ReturnsDataFileKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int16? FilingYear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Byte? FilingMonth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? FilingDueDay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? FilingFrequencyId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String TaxFormCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String LegacyReturnName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String PaymentDetails { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String BulkAccountUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? ClientApiKeyId { get; set; }

	}

	public partial class JobDataArchive : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 CompanyId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 AccountId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Username { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Password { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String CustomerIdentifier { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Mode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String AdditionalOptions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ReturnsDataFileKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int16? FilingYear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Byte? FilingMonth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? FilingDueDay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? FilingFrequencyId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String TaxFormCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String LegacyReturnName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String PaymentDetails { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String BulkAccountUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? ClientApiKeyId { get; set; }

	}

	public partial class JobDataFile : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobDataFileId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String FileKey { get; set; }

	}

	public partial class JobQueue : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime QueueDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobStatusId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Message { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String InternalMessage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? JobStartDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? UpdateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? BulkRequestId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Byte? Priority { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? RefJobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? AvataxUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Error { get; set; }

	}

	public partial class JobQueueArchive : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime QueueDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobStatusId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Message { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String InternalMessage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? JobStartDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? UpdateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? BulkRequestId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Byte? Priority { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? RefJobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? AvataxUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Error { get; set; }

	}

	public partial class JobRequest : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Request { get; set; }

	}

	public partial class JobResponse : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ResponseJson { get; set; }

	}

	public partial class JobStatus : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobStatusId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Description { get; set; }

	}

	public partial class JobType : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Description { get; set; }

	}

	public partial class JobWorkerHost : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String MachineName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String HostIP { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreateDateTime { get; set; }

	}

	public partial class LoginData : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean LoginSuccess { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime CreateDateTime { get; set; }

	}

	public partial class NotificationDetails : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String EventName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Subject { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String HtmlTemplate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String PlainTemplate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String EmailID { get; set; }

	}

	public partial class NotificationHistory : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String EventName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String MailBody { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Sender { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime SendingDate { get; set; }

	}

	public partial class PaymentInfo : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 PaymentInfoId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String BankName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String BankAccountNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String BankRoutingNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String AccountType { get; set; }

	}

	public partial class PaymentInfoAdditionalFields : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String FieldName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String FieldValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public SByte? ShowValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String MaskValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String RegEx { get; set; }

	}

	public partial class PaymentMode : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 PaymentModeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Description { get; set; }

	}

	public partial class RegionJobLimit : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? MaxJobs { get; set; }

	}

	public partial class RequiredFilingCalendarDataField : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 RequiredFilingCalendarDataFieldId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Description { get; set; }

	}

	public partial class Role : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String RoleName { get; set; }

	}

	public partial class SSResource : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 SSResourceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Comments { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String FileKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? UpdateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? FileType { get; set; }

	}

	public partial class SSResourceArchive : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 SSResourceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Comments { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String FileKey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? UpdateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? FileType { get; set; }

	}

	public partial class SSResourceTags : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 SSResourceTagId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? SSResourceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? TagId { get; set; }

	}

	public partial class SSResourceTagsArchive : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 SSResourceTagId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? SSResourceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? TagId { get; set; }

	}

	public partial class ScraperMetadata : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String MetadataJson { get; set; }

	}

	public partial class ScraperScripts : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public System.Guid ScriptId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Decimal ScriptVersion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScriptChecksum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Byte[] ScriptContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public UInt64? IsActive { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreatedOn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String CreatedBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ModifiedBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? ModifiedOn { get; set; }

	}

	public partial class ScraperStatus : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean IsAvailable { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean? IsTwoFactorAuth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Message { get; set; }

	}

	public partial class ScraperStatusRequiredFields : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 RequiredFilingCalendarDataFieldId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobTypeId { get; set; }

	}

	public partial class ScraperTaxForm : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 JobTypeId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String TaxFormCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String LegacyReturnName { get; set; }

	}

	public partial class SkyscraperAuditHistory : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String PrincipleTableName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String PrincipleId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String HistoryJson { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreatedDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? CreatedUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String UserSystemName { get; set; }

	}

	public partial class SkyscraperUser : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 SkyscraperUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String AvaTaxUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String UserName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String FirstName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String LastName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? AvaTaxAccountId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? AvaTaxUserRoleId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? UpdateDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? LastLoginDateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String AISubjectId { get; set; }

	}

	public partial class Tags : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 TagId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String TagName { get; set; }

	}

	public partial class TaxForm : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 TaxFormId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String TaxFormCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String LegacyReturnName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Region { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean? IsWebFilingAvailable { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean? FileUpload { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? WebfilingAccount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean? IsDefaultTaxForm { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String FilingDisabledReason { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean? IsWebfileForm { get; set; }

	}

	public partial class TaxFormBulkAccount : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? TaxFormId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? BulkAccountId { get; set; }

	}

	public partial class TaxFormFilingMode : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 TaxFormId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 FilingModeId { get; set; }

	}

	public partial class TaxFormPaymentMode : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 TaxFormId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 PaymentModeId { get; set; }

	}

	public partial class TaxFormRequiredFilingCalendarDataField : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 TaxFormId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 RequiredFilingCalendarDataFieldId { get; set; }

	}

	public partial class TaxFormRequiredReturnsDataKeys : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String TaxFormCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ReturnsDataKeys { get; set; }

	}

	public partial class UserRoles : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32 Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? RoleId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64? SkyscraperUserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int32? DepartmentId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? ExpirationDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? ModifiedDate { get; set; }

	}

	public partial class WebFileData : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Int64 JobId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String FilingData { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public DateTime? CreateDateTime { get; set; }

	}

	public partial class WebFileFormStatus : BaseModel
	{

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String ScraperRegion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public String Form { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean IsAvailable { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean EFile { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean EPay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[Display(Name = "")]
		public Boolean FileUpload { get; set; }

	}

}
