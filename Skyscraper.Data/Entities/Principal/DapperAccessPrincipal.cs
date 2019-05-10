using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using Avalara.Skyscraper.Data.Dapper.Base;
using Avalara.Skyscraper.Data.Dapper.Entities;
 
namespace Avalara.Skyscraper.Data.Dapper.Entities
{

	public partial class ApiStatusDap : BaseDap
	{
		public ApiStatusDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public ApiStatusDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public ApiStatusDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static ApiStatus[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<ApiStatus> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<ApiStatus>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<ApiStatus> GetTop(int count)
		{
			var queryResult = Query<ApiStatus>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<ApiStatus> ?? queryResult.ToList();
		}



		public ApiStatus GetById(Int32 Id)
		{
			return Query<ApiStatus>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(ApiStatus model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<ApiStatus> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(ApiStatus model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(ApiStatus model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<ApiStatus> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.ApiStatus";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ApiName , Method , Status , ModifiedBy , ModifiedDate) VALUES (@ApiName , @Method , @Status , @ModifiedBy , @ModifiedDate) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ApiName=@ApiName , Method=@Method , Status=@Status , ModifiedBy=@ModifiedBy , ModifiedDate=@ModifiedDate WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ApiName , Method , Status , ModifiedBy , ModifiedDate)  VALUES  (@ApiName , @Method , @Status , @ModifiedBy , @ModifiedDate) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class BulkAccountDap : BaseDap
	{
		public BulkAccountDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public BulkAccountDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public BulkAccountDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static BulkAccount[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<BulkAccount> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<BulkAccount>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<BulkAccount> GetTop(int count)
		{
			var queryResult = Query<BulkAccount>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<BulkAccount> ?? queryResult.ToList();
		}



		public BulkAccount GetByBulkAccountId(Int32 BulkAccountId)
		{
			return Query<BulkAccount>(SqlSelectCommand + " WHERE BulkAccountId=@BulkAccountId", new { BulkAccountId = BulkAccountId }).FirstOrDefault();
		}

		public void Insert(BulkAccount model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<BulkAccount> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the BulkAccountId from OUTPUT inserted.
/// </summary>
/// <returns>BulkAccountId from OUTPUT inserted.</returns>
public Int32 InsertWithId(BulkAccount model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 BulkAccountId)
		{
			Execute(SqlDeleteCommand, new { BulkAccountId = BulkAccountId });
		}

		public void Update(BulkAccount model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<BulkAccount> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.BulkAccount";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , Username , Password , CreatedUserId , CreatedDate , ModifiedUserId , ModifiedDate , IsDefault , IsActive , MaxJobs , CurrentJobs) VALUES (@ScraperRegion , @Country , @Username , @Password , @CreatedUserId , @CreatedDate , @ModifiedUserId , @ModifiedDate , @IsDefault , @IsActive , @MaxJobs , @CurrentJobs) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScraperRegion=@ScraperRegion , Country=@Country , Username=@Username , Password=@Password , CreatedUserId=@CreatedUserId , CreatedDate=@CreatedDate , ModifiedUserId=@ModifiedUserId , ModifiedDate=@ModifiedDate , IsDefault=@IsDefault , IsActive=@IsActive , MaxJobs=@MaxJobs , CurrentJobs=@CurrentJobs WHERE BulkAccountId=@BulkAccountId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE BulkAccountId=@BulkAccountId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , Username , Password , CreatedUserId , CreatedDate , ModifiedUserId , ModifiedDate , IsDefault , IsActive , MaxJobs , CurrentJobs)  VALUES  (@ScraperRegion , @Country , @Username , @Password , @CreatedUserId , @CreatedDate , @ModifiedUserId , @ModifiedDate , @IsDefault , @IsActive , @MaxJobs , @CurrentJobs) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class BulkAccountHistoryDap : BaseDap
	{
		public BulkAccountHistoryDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public BulkAccountHistoryDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public BulkAccountHistoryDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static BulkAccountHistory[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<BulkAccountHistory> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<BulkAccountHistory>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<BulkAccountHistory> GetTop(int count)
		{
			var queryResult = Query<BulkAccountHistory>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<BulkAccountHistory> ?? queryResult.ToList();
		}



		public BulkAccountHistory GetById(Int32 Id)
		{
			return Query<BulkAccountHistory>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(BulkAccountHistory model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<BulkAccountHistory> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(BulkAccountHistory model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(BulkAccountHistory model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<BulkAccountHistory> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.BulkAccountHistory";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (BulkAccountId , OldPassword , NewPassword , ModifiedDate , ModifiedUserId) VALUES (@BulkAccountId , @OldPassword , @NewPassword , @ModifiedDate , @ModifiedUserId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET BulkAccountId=@BulkAccountId , OldPassword=@OldPassword , NewPassword=@NewPassword , ModifiedDate=@ModifiedDate , ModifiedUserId=@ModifiedUserId WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (BulkAccountId , OldPassword , NewPassword , ModifiedDate , ModifiedUserId)  VALUES  (@BulkAccountId , @OldPassword , @NewPassword , @ModifiedDate , @ModifiedUserId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class BulkAccountUserMetaDataDap : BaseDap
	{
		public BulkAccountUserMetaDataDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public BulkAccountUserMetaDataDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public BulkAccountUserMetaDataDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static BulkAccountUserMetaData[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<BulkAccountUserMetaData> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<BulkAccountUserMetaData>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<BulkAccountUserMetaData> GetTop(int count)
		{
			var queryResult = Query<BulkAccountUserMetaData>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<BulkAccountUserMetaData> ?? queryResult.ToList();
		}



		public BulkAccountUserMetaData GetByBulkAccountId(Int32 BulkAccountId)
		{
			return Query<BulkAccountUserMetaData>(SqlSelectCommand + " WHERE BulkAccountId=@BulkAccountId", new { BulkAccountId = BulkAccountId }).FirstOrDefault();
		}

		public void Insert(BulkAccountUserMetaData model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<BulkAccountUserMetaData> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the BulkAccountId from OUTPUT inserted.
/// </summary>
/// <returns>BulkAccountId from OUTPUT inserted.</returns>
public Int32 InsertWithId(BulkAccountUserMetaData model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 BulkAccountId)
		{
			Execute(SqlDeleteCommand, new { BulkAccountId = BulkAccountId });
		}

		public void Update(BulkAccountUserMetaData model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<BulkAccountUserMetaData> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.BulkAccountUserMetaData";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (BulkAccountId , MetaDataJson) VALUES (@BulkAccountId , @MetaDataJson) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET BulkAccountId=@BulkAccountId , MetaDataJson=@MetaDataJson WHERE BulkAccountId=@BulkAccountId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE BulkAccountId=@BulkAccountId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (BulkAccountId , MetaDataJson)  VALUES  (@BulkAccountId , @MetaDataJson) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class BulkRequestDap : BaseDap
	{
		public BulkRequestDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public BulkRequestDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public BulkRequestDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static BulkRequest[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<BulkRequest> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<BulkRequest>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<BulkRequest> GetTop(int count)
		{
			var queryResult = Query<BulkRequest>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<BulkRequest> ?? queryResult.ToList();
		}



		public BulkRequest GetByBulkRequestId(Int64 BulkRequestId)
		{
			return Query<BulkRequest>(SqlSelectCommand + " WHERE BulkRequestId=@BulkRequestId", new { BulkRequestId = BulkRequestId }).FirstOrDefault();
		}

		public void Insert(BulkRequest model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<BulkRequest> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the BulkRequestId from OUTPUT inserted.
/// </summary>
/// <returns>BulkRequestId from OUTPUT inserted.</returns>
public Int64 InsertWithId(BulkRequest model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 BulkRequestId)
		{
			Execute(SqlDeleteCommand, new { BulkRequestId = BulkRequestId });
		}

		public void Update(BulkRequest model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<BulkRequest> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.BulkRequest";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Name , Description , CreateDateTime) VALUES (@Name , @Description , @CreateDateTime) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Name=@Name , Description=@Description , CreateDateTime=@CreateDateTime WHERE BulkRequestId=@BulkRequestId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE BulkRequestId=@BulkRequestId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Name , Description , CreateDateTime)  VALUES  (@Name , @Description , @CreateDateTime) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class ClientAPIKeysDap : BaseDap
	{
		public ClientAPIKeysDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public ClientAPIKeysDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public ClientAPIKeysDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static ClientAPIKeys[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<ClientAPIKeys> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<ClientAPIKeys>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<ClientAPIKeys> GetTop(int count)
		{
			var queryResult = Query<ClientAPIKeys>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<ClientAPIKeys> ?? queryResult.ToList();
		}



		public ClientAPIKeys GetById(Int32 Id)
		{
			return Query<ClientAPIKeys>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(ClientAPIKeys model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<ClientAPIKeys> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(ClientAPIKeys model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(ClientAPIKeys model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<ClientAPIKeys> models)
		{
			Execute(SqlUpdateCommand, models);
		}

		public List<ClientAPIKeys> GetByDepartmentId(Int32 DepartmentId)
		{
			return Query<ClientAPIKeys>(SqlSelectCommand + " WHERE DepartmentId=@DepartmentId", new { DepartmentId = DepartmentId }).ToList();
		}
		
		public Department GetDepartmentById(Int32 Id)
		{
			using (var dap = new DepartmentDap(this))
			{
				return dap.GetById(Id);
			}
		}



		public const string SqlTableName = "Skyscraper.ClientAPIKeys";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (APIKey , AppName , DepartmentId , ExpirationDate , ModifiedDate) VALUES (@APIKey , @AppName , @DepartmentId , @ExpirationDate , @ModifiedDate) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET APIKey=@APIKey , AppName=@AppName , DepartmentId=@DepartmentId , ExpirationDate=@ExpirationDate , ModifiedDate=@ModifiedDate WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (APIKey , AppName , DepartmentId , ExpirationDate , ModifiedDate)  VALUES  (@APIKey , @AppName , @DepartmentId , @ExpirationDate , @ModifiedDate) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class CustomerDORDataDap : BaseDap
	{
		public CustomerDORDataDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public CustomerDORDataDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public CustomerDORDataDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static CustomerDORData[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<CustomerDORData> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<CustomerDORData>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<CustomerDORData> GetTop(int count)
		{
			var queryResult = Query<CustomerDORData>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<CustomerDORData> ?? queryResult.ToList();
		}



		public CustomerDORData GetByJobId(Int64 JobId)
		{
			return Query<CustomerDORData>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(CustomerDORData model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<CustomerDORData> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(CustomerDORData model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(CustomerDORData model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<CustomerDORData> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.CustomerDORData";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobId , DORData , CreateDateTime , WebsiteLoginSuccess) VALUES (@JobId , @DORData , @CreateDateTime , @WebsiteLoginSuccess) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobId=@JobId , DORData=@DORData , CreateDateTime=@CreateDateTime , WebsiteLoginSuccess=@WebsiteLoginSuccess WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobId , DORData , CreateDateTime , WebsiteLoginSuccess)  VALUES  (@JobId , @DORData , @CreateDateTime , @WebsiteLoginSuccess) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class DedicatedInstanceStatesDap : BaseDap
	{
		public DedicatedInstanceStatesDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public DedicatedInstanceStatesDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public DedicatedInstanceStatesDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static DedicatedInstanceStates[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<DedicatedInstanceStates> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<DedicatedInstanceStates>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<DedicatedInstanceStates> GetTop(int count)
		{
			var queryResult = Query<DedicatedInstanceStates>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<DedicatedInstanceStates> ?? queryResult.ToList();
		}



		public DedicatedInstanceStates GetById(Int32 Id)
		{
			return Query<DedicatedInstanceStates>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(DedicatedInstanceStates model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<DedicatedInstanceStates> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(DedicatedInstanceStates model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(DedicatedInstanceStates model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<DedicatedInstanceStates> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.DedicatedInstanceStates";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Country , Region , UDF1 , UDF2 , MaxWorkers , CurrentWorkers) VALUES (@Country , @Region , @UDF1 , @UDF2 , @MaxWorkers , @CurrentWorkers) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Country=@Country , Region=@Region , UDF1=@UDF1 , UDF2=@UDF2 , MaxWorkers=@MaxWorkers , CurrentWorkers=@CurrentWorkers WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Country , Region , UDF1 , UDF2 , MaxWorkers , CurrentWorkers)  VALUES  (@Country , @Region , @UDF1 , @UDF2 , @MaxWorkers , @CurrentWorkers) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class DedicatedInstancesDap : BaseDap
	{
		public DedicatedInstancesDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public DedicatedInstancesDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public DedicatedInstancesDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static DedicatedInstances[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<DedicatedInstances> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<DedicatedInstances>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<DedicatedInstances> GetTop(int count)
		{
			var queryResult = Query<DedicatedInstances>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<DedicatedInstances> ?? queryResult.ToList();
		}



		public DedicatedInstances GetById(Int32 Id)
		{
			return Query<DedicatedInstances>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(DedicatedInstances model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<DedicatedInstances> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(DedicatedInstances model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(DedicatedInstances model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<DedicatedInstances> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.DedicatedInstances";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (InstanceId , HostName , Region , Country , UDF1 , UDF2 , UDF3 , UDF4 , UDF5) VALUES (@InstanceId , @HostName , @Region , @Country , @UDF1 , @UDF2 , @UDF3 , @UDF4 , @UDF5) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET InstanceId=@InstanceId , HostName=@HostName , Region=@Region , Country=@Country , UDF1=@UDF1 , UDF2=@UDF2 , UDF3=@UDF3 , UDF4=@UDF4 , UDF5=@UDF5 WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (InstanceId , HostName , Region , Country , UDF1 , UDF2 , UDF3 , UDF4 , UDF5)  VALUES  (@InstanceId , @HostName , @Region , @Country , @UDF1 , @UDF2 , @UDF3 , @UDF4 , @UDF5) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class DepartmentDap : BaseDap
	{
		public DepartmentDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public DepartmentDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public DepartmentDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static Department[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<Department> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<Department>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<Department> GetTop(int count)
		{
			var queryResult = Query<Department>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<Department> ?? queryResult.ToList();
		}



		public Department GetById(Int32 Id)
		{
			return Query<Department>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(Department model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<Department> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(Department model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(Department model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<Department> models)
		{
			Execute(SqlUpdateCommand, models);
		}

		public List<ClientAPIKeys> GetClientAPIKeysByDepartmentId(Int32 DepartmentId)
		{
			using (var dap = new ClientAPIKeysDap(this))
			{
				return dap.GetByDepartmentId(DepartmentId);
			}
		}



		public Department GetByDepartmentNameIndex(String DepartmentName)
		{
			return Query<Department>(SqlSelectCommand + " WHERE DepartmentName=@DepartmentName", new { DepartmentName = DepartmentName }).FirstOrDefault();
		}

		public void DeleteByDepartmentName(String DepartmentName)
		{
			Execute(SqlDeleteCommand, new { DepartmentName = DepartmentName });
		}

		public const string SqlTableName = "Skyscraper.Department";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (DepartmentName) VALUES (@DepartmentName) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET DepartmentName=@DepartmentName WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (DepartmentName)  VALUES  (@DepartmentName) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class DepartmentBulkAccountDap : BaseDap
	{
		public DepartmentBulkAccountDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public DepartmentBulkAccountDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public DepartmentBulkAccountDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static DepartmentBulkAccount[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<DepartmentBulkAccount> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<DepartmentBulkAccount>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<DepartmentBulkAccount> GetTop(int count)
		{
			var queryResult = Query<DepartmentBulkAccount>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<DepartmentBulkAccount> ?? queryResult.ToList();
		}



		public DepartmentBulkAccount GetByBulkAccountId(Int32 BulkAccountId)
		{
			return Query<DepartmentBulkAccount>(SqlSelectCommand + " WHERE BulkAccountId=@BulkAccountId", new { BulkAccountId = BulkAccountId }).FirstOrDefault();
		}

		public DepartmentBulkAccount GetByDepartmentId(Int32 DepartmentId)
		{
			return Query<DepartmentBulkAccount>(SqlSelectCommand + " WHERE DepartmentId=@DepartmentId", new { DepartmentId = DepartmentId }).FirstOrDefault();
		}

		public void Insert(DepartmentBulkAccount model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<DepartmentBulkAccount> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the BulkAccountId from OUTPUT inserted.
/// </summary>
/// <returns>BulkAccountId from OUTPUT inserted.</returns>
public Int32 InsertWithId(DepartmentBulkAccount model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 BulkAccountId, Int32 DepartmentId)
		{
			Execute(SqlDeleteCommand, new { BulkAccountId = BulkAccountId, DepartmentId = DepartmentId });
		}

		public void Update(DepartmentBulkAccount model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<DepartmentBulkAccount> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.DepartmentBulkAccount";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (BulkAccountId , DepartmentId) VALUES (@BulkAccountId , @DepartmentId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET BulkAccountId=@BulkAccountId , DepartmentId=@DepartmentId WHERE BulkAccountId=@BulkAccountId AND DepartmentId=@DepartmentId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE BulkAccountId=@BulkAccountId AND DepartmentId=@DepartmentId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (BulkAccountId , DepartmentId)  VALUES  (@BulkAccountId , @DepartmentId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class DepartmentPaymentInfoDap : BaseDap
	{
		public DepartmentPaymentInfoDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public DepartmentPaymentInfoDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public DepartmentPaymentInfoDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static DepartmentPaymentInfo[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<DepartmentPaymentInfo> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<DepartmentPaymentInfo>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<DepartmentPaymentInfo> GetTop(int count)
		{
			var queryResult = Query<DepartmentPaymentInfo>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<DepartmentPaymentInfo> ?? queryResult.ToList();
		}



		public DepartmentPaymentInfo GetByPaymentInfoId(Int32 PaymentInfoId)
		{
			return Query<DepartmentPaymentInfo>(SqlSelectCommand + " WHERE PaymentInfoId=@PaymentInfoId", new { PaymentInfoId = PaymentInfoId }).FirstOrDefault();
		}

		public DepartmentPaymentInfo GetByDepartmentId(Int32 DepartmentId)
		{
			return Query<DepartmentPaymentInfo>(SqlSelectCommand + " WHERE DepartmentId=@DepartmentId", new { DepartmentId = DepartmentId }).FirstOrDefault();
		}

		public void Insert(DepartmentPaymentInfo model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<DepartmentPaymentInfo> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the PaymentInfoId from OUTPUT inserted.
/// </summary>
/// <returns>PaymentInfoId from OUTPUT inserted.</returns>
public Int32 InsertWithId(DepartmentPaymentInfo model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 PaymentInfoId, Int32 DepartmentId)
		{
			Execute(SqlDeleteCommand, new { PaymentInfoId = PaymentInfoId, DepartmentId = DepartmentId });
		}

		public void Update(DepartmentPaymentInfo model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<DepartmentPaymentInfo> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.DepartmentPaymentInfo";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (PaymentInfoId , DepartmentId) VALUES (@PaymentInfoId , @DepartmentId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET PaymentInfoId=@PaymentInfoId , DepartmentId=@DepartmentId WHERE PaymentInfoId=@PaymentInfoId AND DepartmentId=@DepartmentId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE PaymentInfoId=@PaymentInfoId AND DepartmentId=@DepartmentId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (PaymentInfoId , DepartmentId)  VALUES  (@PaymentInfoId , @DepartmentId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class FilingModeDap : BaseDap
	{
		public FilingModeDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public FilingModeDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public FilingModeDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static FilingMode[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<FilingMode> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<FilingMode>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<FilingMode> GetTop(int count)
		{
			var queryResult = Query<FilingMode>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<FilingMode> ?? queryResult.ToList();
		}



		public FilingMode GetByFilingModeId(Int32 FilingModeId)
		{
			return Query<FilingMode>(SqlSelectCommand + " WHERE FilingModeId=@FilingModeId", new { FilingModeId = FilingModeId }).FirstOrDefault();
		}

		public void Insert(FilingMode model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<FilingMode> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the FilingModeId from OUTPUT inserted.
/// </summary>
/// <returns>FilingModeId from OUTPUT inserted.</returns>
public Int32 InsertWithId(FilingMode model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 FilingModeId)
		{
			Execute(SqlDeleteCommand, new { FilingModeId = FilingModeId });
		}

		public void Update(FilingMode model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<FilingMode> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.FilingMode";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Name , Description) VALUES (@Name , @Description) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Name=@Name , Description=@Description WHERE FilingModeId=@FilingModeId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE FilingModeId=@FilingModeId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Name , Description)  VALUES  (@Name , @Description) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class IndividualAccountUserMetadataDap : BaseDap
	{
		public IndividualAccountUserMetadataDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public IndividualAccountUserMetadataDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public IndividualAccountUserMetadataDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static IndividualAccountUserMetadata[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<IndividualAccountUserMetadata> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<IndividualAccountUserMetadata>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<IndividualAccountUserMetadata> GetTop(int count)
		{
			var queryResult = Query<IndividualAccountUserMetadata>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<IndividualAccountUserMetadata> ?? queryResult.ToList();
		}



		public IndividualAccountUserMetadata GetByScraperRegion(String ScraperRegion)
		{
			return Query<IndividualAccountUserMetadata>(SqlSelectCommand + " WHERE ScraperRegion=@ScraperRegion", new { ScraperRegion = ScraperRegion }).FirstOrDefault();
		}

		public IndividualAccountUserMetadata GetByCountry(String Country)
		{
			return Query<IndividualAccountUserMetadata>(SqlSelectCommand + " WHERE Country=@Country", new { Country = Country }).FirstOrDefault();
		}

		public IndividualAccountUserMetadata GetByUsername(String Username)
		{
			return Query<IndividualAccountUserMetadata>(SqlSelectCommand + " WHERE Username=@Username", new { Username = Username }).FirstOrDefault();
		}

		public void Insert(IndividualAccountUserMetadata model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<IndividualAccountUserMetadata> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ScraperRegion from OUTPUT inserted.
/// </summary>
/// <returns>ScraperRegion from OUTPUT inserted.</returns>
public String InsertWithId(IndividualAccountUserMetadata model)
{
    var result = Query<String>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(String ScraperRegion, String Country, String Username)
		{
			Execute(SqlDeleteCommand, new { ScraperRegion = ScraperRegion, Country = Country, Username = Username });
		}

		public void Update(IndividualAccountUserMetadata model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<IndividualAccountUserMetadata> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.IndividualAccountUserMetadata";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , Username , MetadataJson , UDF1 , UDF2) VALUES (@ScraperRegion , @Country , @Username , @MetadataJson , @UDF1 , @UDF2) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScraperRegion=@ScraperRegion , Country=@Country , Username=@Username , MetadataJson=@MetadataJson , UDF1=@UDF1 , UDF2=@UDF2 WHERE ScraperRegion=@ScraperRegion AND Country=@Country AND Username=@Username";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ScraperRegion=@ScraperRegion AND Country=@Country AND Username=@Username";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , Username , MetadataJson , UDF1 , UDF2)  VALUES  (@ScraperRegion , @Country , @Username , @MetadataJson , @UDF1 , @UDF2) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobDataDap : BaseDap
	{
		public JobDataDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobDataDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobDataDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobData[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobData> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobData>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobData> GetTop(int count)
		{
			var queryResult = Query<JobData>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobData> ?? queryResult.ToList();
		}



		public JobData GetByJobId(Int64 JobId)
		{
			return Query<JobData>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(JobData model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobData> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(JobData model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(JobData model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobData> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobData";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobId , CompanyId , AccountId , Country , Region , Username , Password , CustomerIdentifier , Mode , AdditionalOptions , ReturnsDataFileKey , FilingYear , FilingMonth , FilingDueDay , FilingFrequencyId , TaxFormCode , LegacyReturnName , PaymentDetails , BulkAccountUserId , ClientApiKeyId) VALUES (@JobId , @CompanyId , @AccountId , @Country , @Region , @Username , @Password , @CustomerIdentifier , @Mode , @AdditionalOptions , @ReturnsDataFileKey , @FilingYear , @FilingMonth , @FilingDueDay , @FilingFrequencyId , @TaxFormCode , @LegacyReturnName , @PaymentDetails , @BulkAccountUserId , @ClientApiKeyId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobId=@JobId , CompanyId=@CompanyId , AccountId=@AccountId , Country=@Country , Region=@Region , Username=@Username , Password=@Password , CustomerIdentifier=@CustomerIdentifier , Mode=@Mode , AdditionalOptions=@AdditionalOptions , ReturnsDataFileKey=@ReturnsDataFileKey , FilingYear=@FilingYear , FilingMonth=@FilingMonth , FilingDueDay=@FilingDueDay , FilingFrequencyId=@FilingFrequencyId , TaxFormCode=@TaxFormCode , LegacyReturnName=@LegacyReturnName , PaymentDetails=@PaymentDetails , BulkAccountUserId=@BulkAccountUserId , ClientApiKeyId=@ClientApiKeyId WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobId , CompanyId , AccountId , Country , Region , Username , Password , CustomerIdentifier , Mode , AdditionalOptions , ReturnsDataFileKey , FilingYear , FilingMonth , FilingDueDay , FilingFrequencyId , TaxFormCode , LegacyReturnName , PaymentDetails , BulkAccountUserId , ClientApiKeyId)  VALUES  (@JobId , @CompanyId , @AccountId , @Country , @Region , @Username , @Password , @CustomerIdentifier , @Mode , @AdditionalOptions , @ReturnsDataFileKey , @FilingYear , @FilingMonth , @FilingDueDay , @FilingFrequencyId , @TaxFormCode , @LegacyReturnName , @PaymentDetails , @BulkAccountUserId , @ClientApiKeyId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobDataArchiveDap : BaseDap
	{
		public JobDataArchiveDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobDataArchiveDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobDataArchiveDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobDataArchive[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobDataArchive> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobDataArchive>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobDataArchive> GetTop(int count)
		{
			var queryResult = Query<JobDataArchive>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobDataArchive> ?? queryResult.ToList();
		}



		public JobDataArchive GetByJobId(Int64 JobId)
		{
			return Query<JobDataArchive>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(JobDataArchive model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobDataArchive> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(JobDataArchive model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(JobDataArchive model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobDataArchive> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobDataArchive";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobId , CompanyId , AccountId , Country , Region , Username , Password , CustomerIdentifier , Mode , AdditionalOptions , ReturnsDataFileKey , FilingYear , FilingMonth , FilingDueDay , FilingFrequencyId , TaxFormCode , LegacyReturnName , PaymentDetails , BulkAccountUserId , ClientApiKeyId) VALUES (@JobId , @CompanyId , @AccountId , @Country , @Region , @Username , @Password , @CustomerIdentifier , @Mode , @AdditionalOptions , @ReturnsDataFileKey , @FilingYear , @FilingMonth , @FilingDueDay , @FilingFrequencyId , @TaxFormCode , @LegacyReturnName , @PaymentDetails , @BulkAccountUserId , @ClientApiKeyId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobId=@JobId , CompanyId=@CompanyId , AccountId=@AccountId , Country=@Country , Region=@Region , Username=@Username , Password=@Password , CustomerIdentifier=@CustomerIdentifier , Mode=@Mode , AdditionalOptions=@AdditionalOptions , ReturnsDataFileKey=@ReturnsDataFileKey , FilingYear=@FilingYear , FilingMonth=@FilingMonth , FilingDueDay=@FilingDueDay , FilingFrequencyId=@FilingFrequencyId , TaxFormCode=@TaxFormCode , LegacyReturnName=@LegacyReturnName , PaymentDetails=@PaymentDetails , BulkAccountUserId=@BulkAccountUserId , ClientApiKeyId=@ClientApiKeyId WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobId , CompanyId , AccountId , Country , Region , Username , Password , CustomerIdentifier , Mode , AdditionalOptions , ReturnsDataFileKey , FilingYear , FilingMonth , FilingDueDay , FilingFrequencyId , TaxFormCode , LegacyReturnName , PaymentDetails , BulkAccountUserId , ClientApiKeyId)  VALUES  (@JobId , @CompanyId , @AccountId , @Country , @Region , @Username , @Password , @CustomerIdentifier , @Mode , @AdditionalOptions , @ReturnsDataFileKey , @FilingYear , @FilingMonth , @FilingDueDay , @FilingFrequencyId , @TaxFormCode , @LegacyReturnName , @PaymentDetails , @BulkAccountUserId , @ClientApiKeyId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobDataFileDap : BaseDap
	{
		public JobDataFileDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobDataFileDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobDataFileDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobDataFile[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobDataFile> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobDataFile>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobDataFile> GetTop(int count)
		{
			var queryResult = Query<JobDataFile>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobDataFile> ?? queryResult.ToList();
		}



		public JobDataFile GetByJobDataFileId(Int64 JobDataFileId)
		{
			return Query<JobDataFile>(SqlSelectCommand + " WHERE JobDataFileId=@JobDataFileId", new { JobDataFileId = JobDataFileId }).FirstOrDefault();
		}

		public void Insert(JobDataFile model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobDataFile> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobDataFileId from OUTPUT inserted.
/// </summary>
/// <returns>JobDataFileId from OUTPUT inserted.</returns>
public Int64 InsertWithId(JobDataFile model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobDataFileId)
		{
			Execute(SqlDeleteCommand, new { JobDataFileId = JobDataFileId });
		}

		public void Update(JobDataFile model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobDataFile> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobDataFile";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobId , Name , FileKey) VALUES (@JobId , @Name , @FileKey) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobId=@JobId , Name=@Name , FileKey=@FileKey WHERE JobDataFileId=@JobDataFileId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobDataFileId=@JobDataFileId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobId , Name , FileKey)  VALUES  (@JobId , @Name , @FileKey) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobQueueDap : BaseDap
	{
		public JobQueueDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobQueueDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobQueueDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobQueue[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobQueue> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobQueue>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobQueue> GetTop(int count)
		{
			var queryResult = Query<JobQueue>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobQueue> ?? queryResult.ToList();
		}



		public JobQueue GetByJobId(Int64 JobId)
		{
			return Query<JobQueue>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(JobQueue model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobQueue> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(JobQueue model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(JobQueue model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobQueue> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobQueue";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (QueueDateTime , JobTypeId , JobStatusId , Country , Region , Message , InternalMessage , JobStartDateTime , UpdateDateTime , BulkRequestId , Priority , RefJobId , AvataxUserId , Error) VALUES (@QueueDateTime , @JobTypeId , @JobStatusId , @Country , @Region , @Message , @InternalMessage , @JobStartDateTime , @UpdateDateTime , @BulkRequestId , @Priority , @RefJobId , @AvataxUserId , @Error) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET QueueDateTime=@QueueDateTime , JobTypeId=@JobTypeId , JobStatusId=@JobStatusId , Country=@Country , Region=@Region , Message=@Message , InternalMessage=@InternalMessage , JobStartDateTime=@JobStartDateTime , UpdateDateTime=@UpdateDateTime , BulkRequestId=@BulkRequestId , Priority=@Priority , RefJobId=@RefJobId , AvataxUserId=@AvataxUserId , Error=@Error WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (QueueDateTime , JobTypeId , JobStatusId , Country , Region , Message , InternalMessage , JobStartDateTime , UpdateDateTime , BulkRequestId , Priority , RefJobId , AvataxUserId , Error)  VALUES  (@QueueDateTime , @JobTypeId , @JobStatusId , @Country , @Region , @Message , @InternalMessage , @JobStartDateTime , @UpdateDateTime , @BulkRequestId , @Priority , @RefJobId , @AvataxUserId , @Error) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobQueueArchiveDap : BaseDap
	{
		public JobQueueArchiveDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobQueueArchiveDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobQueueArchiveDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobQueueArchive[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobQueueArchive> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobQueueArchive>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobQueueArchive> GetTop(int count)
		{
			var queryResult = Query<JobQueueArchive>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobQueueArchive> ?? queryResult.ToList();
		}



		public JobQueueArchive GetByJobId(Int64 JobId)
		{
			return Query<JobQueueArchive>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(JobQueueArchive model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobQueueArchive> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(JobQueueArchive model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(JobQueueArchive model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobQueueArchive> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobQueueArchive";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (QueueDateTime , JobTypeId , JobStatusId , Country , Region , Message , InternalMessage , JobStartDateTime , UpdateDateTime , BulkRequestId , Priority , RefJobId , AvataxUserId , Error) VALUES (@QueueDateTime , @JobTypeId , @JobStatusId , @Country , @Region , @Message , @InternalMessage , @JobStartDateTime , @UpdateDateTime , @BulkRequestId , @Priority , @RefJobId , @AvataxUserId , @Error) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET QueueDateTime=@QueueDateTime , JobTypeId=@JobTypeId , JobStatusId=@JobStatusId , Country=@Country , Region=@Region , Message=@Message , InternalMessage=@InternalMessage , JobStartDateTime=@JobStartDateTime , UpdateDateTime=@UpdateDateTime , BulkRequestId=@BulkRequestId , Priority=@Priority , RefJobId=@RefJobId , AvataxUserId=@AvataxUserId , Error=@Error WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (QueueDateTime , JobTypeId , JobStatusId , Country , Region , Message , InternalMessage , JobStartDateTime , UpdateDateTime , BulkRequestId , Priority , RefJobId , AvataxUserId , Error)  VALUES  (@QueueDateTime , @JobTypeId , @JobStatusId , @Country , @Region , @Message , @InternalMessage , @JobStartDateTime , @UpdateDateTime , @BulkRequestId , @Priority , @RefJobId , @AvataxUserId , @Error) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobRequestDap : BaseDap
	{
		public JobRequestDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobRequestDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobRequestDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobRequest[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobRequest> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobRequest>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobRequest> GetTop(int count)
		{
			var queryResult = Query<JobRequest>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobRequest> ?? queryResult.ToList();
		}



		public JobRequest GetByJobId(Int64 JobId)
		{
			return Query<JobRequest>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(JobRequest model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobRequest> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(JobRequest model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(JobRequest model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobRequest> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobRequest";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobId , Request) VALUES (@JobId , @Request) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobId=@JobId , Request=@Request WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobId , Request)  VALUES  (@JobId , @Request) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobResponseDap : BaseDap
	{
		public JobResponseDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobResponseDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobResponseDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobResponse[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobResponse> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobResponse>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobResponse> GetTop(int count)
		{
			var queryResult = Query<JobResponse>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobResponse> ?? queryResult.ToList();
		}



		public JobResponse GetByJobId(Int64 JobId)
		{
			return Query<JobResponse>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(JobResponse model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobResponse> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(JobResponse model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(JobResponse model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobResponse> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobResponse";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobId , ResponseJson) VALUES (@JobId , @ResponseJson) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobId=@JobId , ResponseJson=@ResponseJson WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobId , ResponseJson)  VALUES  (@JobId , @ResponseJson) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobStatusDap : BaseDap
	{
		public JobStatusDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobStatusDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobStatusDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobStatus[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobStatus> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobStatus>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobStatus> GetTop(int count)
		{
			var queryResult = Query<JobStatus>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobStatus> ?? queryResult.ToList();
		}



		public JobStatus GetByJobStatusId(Int32 JobStatusId)
		{
			return Query<JobStatus>(SqlSelectCommand + " WHERE JobStatusId=@JobStatusId", new { JobStatusId = JobStatusId }).FirstOrDefault();
		}

		public void Insert(JobStatus model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobStatus> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobStatusId from OUTPUT inserted.
/// </summary>
/// <returns>JobStatusId from OUTPUT inserted.</returns>
public Int32 InsertWithId(JobStatus model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 JobStatusId)
		{
			Execute(SqlDeleteCommand, new { JobStatusId = JobStatusId });
		}

		public void Update(JobStatus model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobStatus> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobStatus";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobStatusId , Name , Description) VALUES (@JobStatusId , @Name , @Description) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobStatusId=@JobStatusId , Name=@Name , Description=@Description WHERE JobStatusId=@JobStatusId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobStatusId=@JobStatusId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobStatusId , Name , Description)  VALUES  (@JobStatusId , @Name , @Description) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobTypeDap : BaseDap
	{
		public JobTypeDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobTypeDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobTypeDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobType[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobType> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobType>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobType> GetTop(int count)
		{
			var queryResult = Query<JobType>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobType> ?? queryResult.ToList();
		}



		public JobType GetByJobTypeId(Int32 JobTypeId)
		{
			return Query<JobType>(SqlSelectCommand + " WHERE JobTypeId=@JobTypeId", new { JobTypeId = JobTypeId }).FirstOrDefault();
		}

		public void Insert(JobType model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobType> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobTypeId from OUTPUT inserted.
/// </summary>
/// <returns>JobTypeId from OUTPUT inserted.</returns>
public Int32 InsertWithId(JobType model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 JobTypeId)
		{
			Execute(SqlDeleteCommand, new { JobTypeId = JobTypeId });
		}

		public void Update(JobType model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<JobType> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobType";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobTypeId , Name , Description) VALUES (@JobTypeId , @Name , @Description) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobTypeId=@JobTypeId , Name=@Name , Description=@Description WHERE JobTypeId=@JobTypeId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobTypeId=@JobTypeId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobTypeId , Name , Description)  VALUES  (@JobTypeId , @Name , @Description) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class JobWorkerHostDap : BaseDap
	{
		public JobWorkerHostDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public JobWorkerHostDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public JobWorkerHostDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static JobWorkerHost[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<JobWorkerHost> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<JobWorkerHost>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<JobWorkerHost> GetTop(int count)
		{
			var queryResult = Query<JobWorkerHost>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<JobWorkerHost> ?? queryResult.ToList();
		}


		public void Insert(JobWorkerHost model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<JobWorkerHost> models)
		{
			Execute(SqlInsertCommand, models);
		}




		public const string SqlTableName = "Skyscraper.JobWorkerHost";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " @JobId , @MachineName , @HostIP , @CreateDateTime ";
		
	}

	public partial class LoginDataDap : BaseDap
	{
		public LoginDataDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public LoginDataDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public LoginDataDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static LoginData[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<LoginData> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<LoginData>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<LoginData> GetTop(int count)
		{
			var queryResult = Query<LoginData>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<LoginData> ?? queryResult.ToList();
		}



		public LoginData GetByJobId(Int64 JobId)
		{
			return Query<LoginData>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(LoginData model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<LoginData> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(LoginData model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(LoginData model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<LoginData> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.LoginData";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobId , LoginSuccess , CreateDateTime) VALUES (@JobId , @LoginSuccess , @CreateDateTime) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobId=@JobId , LoginSuccess=@LoginSuccess , CreateDateTime=@CreateDateTime WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobId , LoginSuccess , CreateDateTime)  VALUES  (@JobId , @LoginSuccess , @CreateDateTime) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class NotificationDetailsDap : BaseDap
	{
		public NotificationDetailsDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public NotificationDetailsDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public NotificationDetailsDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static NotificationDetails[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<NotificationDetails> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<NotificationDetails>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<NotificationDetails> GetTop(int count)
		{
			var queryResult = Query<NotificationDetails>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<NotificationDetails> ?? queryResult.ToList();
		}



		public NotificationDetails GetByID(Int32 ID)
		{
			return Query<NotificationDetails>(SqlSelectCommand + " WHERE ID=@ID", new { ID = ID }).FirstOrDefault();
		}

		public void Insert(NotificationDetails model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<NotificationDetails> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ID from OUTPUT inserted.
/// </summary>
/// <returns>ID from OUTPUT inserted.</returns>
public Int32 InsertWithId(NotificationDetails model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 ID)
		{
			Execute(SqlDeleteCommand, new { ID = ID });
		}

		public void Update(NotificationDetails model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<NotificationDetails> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.NotificationDetails";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (EventName , Subject , HtmlTemplate , PlainTemplate , EmailID) VALUES (@EventName , @Subject , @HtmlTemplate , @PlainTemplate , @EmailID) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET EventName=@EventName , Subject=@Subject , HtmlTemplate=@HtmlTemplate , PlainTemplate=@PlainTemplate , EmailID=@EmailID WHERE ID=@ID";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ID=@ID";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (EventName , Subject , HtmlTemplate , PlainTemplate , EmailID)  VALUES  (@EventName , @Subject , @HtmlTemplate , @PlainTemplate , @EmailID) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class NotificationHistoryDap : BaseDap
	{
		public NotificationHistoryDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public NotificationHistoryDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public NotificationHistoryDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static NotificationHistory[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<NotificationHistory> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<NotificationHistory>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<NotificationHistory> GetTop(int count)
		{
			var queryResult = Query<NotificationHistory>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<NotificationHistory> ?? queryResult.ToList();
		}



		public NotificationHistory GetByID(Int32 ID)
		{
			return Query<NotificationHistory>(SqlSelectCommand + " WHERE ID=@ID", new { ID = ID }).FirstOrDefault();
		}

		public void Insert(NotificationHistory model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<NotificationHistory> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ID from OUTPUT inserted.
/// </summary>
/// <returns>ID from OUTPUT inserted.</returns>
public Int32 InsertWithId(NotificationHistory model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 ID)
		{
			Execute(SqlDeleteCommand, new { ID = ID });
		}

		public void Update(NotificationHistory model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<NotificationHistory> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.NotificationHistory";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (EventName , MailBody , Sender , SendingDate) VALUES (@EventName , @MailBody , @Sender , @SendingDate) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET EventName=@EventName , MailBody=@MailBody , Sender=@Sender , SendingDate=@SendingDate WHERE ID=@ID";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ID=@ID";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (EventName , MailBody , Sender , SendingDate)  VALUES  (@EventName , @MailBody , @Sender , @SendingDate) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class PaymentInfoDap : BaseDap
	{
		public PaymentInfoDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public PaymentInfoDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public PaymentInfoDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static PaymentInfo[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<PaymentInfo> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<PaymentInfo>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<PaymentInfo> GetTop(int count)
		{
			var queryResult = Query<PaymentInfo>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<PaymentInfo> ?? queryResult.ToList();
		}



		public PaymentInfo GetByPaymentInfoId(Int32 PaymentInfoId)
		{
			return Query<PaymentInfo>(SqlSelectCommand + " WHERE PaymentInfoId=@PaymentInfoId", new { PaymentInfoId = PaymentInfoId }).FirstOrDefault();
		}

		public void Insert(PaymentInfo model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<PaymentInfo> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the PaymentInfoId from OUTPUT inserted.
/// </summary>
/// <returns>PaymentInfoId from OUTPUT inserted.</returns>
public Int32 InsertWithId(PaymentInfo model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 PaymentInfoId)
		{
			Execute(SqlDeleteCommand, new { PaymentInfoId = PaymentInfoId });
		}

		public void Update(PaymentInfo model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<PaymentInfo> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.PaymentInfo";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Country , Region , BankName , BankAccountNum , BankRoutingNum , AccountType) VALUES (@Country , @Region , @BankName , @BankAccountNum , @BankRoutingNum , @AccountType) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Country=@Country , Region=@Region , BankName=@BankName , BankAccountNum=@BankAccountNum , BankRoutingNum=@BankRoutingNum , AccountType=@AccountType WHERE PaymentInfoId=@PaymentInfoId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE PaymentInfoId=@PaymentInfoId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Country , Region , BankName , BankAccountNum , BankRoutingNum , AccountType)  VALUES  (@Country , @Region , @BankName , @BankAccountNum , @BankRoutingNum , @AccountType) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class PaymentInfoAdditionalFieldsDap : BaseDap
	{
		public PaymentInfoAdditionalFieldsDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public PaymentInfoAdditionalFieldsDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public PaymentInfoAdditionalFieldsDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static PaymentInfoAdditionalFields[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<PaymentInfoAdditionalFields> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<PaymentInfoAdditionalFields>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<PaymentInfoAdditionalFields> GetTop(int count)
		{
			var queryResult = Query<PaymentInfoAdditionalFields>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<PaymentInfoAdditionalFields> ?? queryResult.ToList();
		}



		public PaymentInfoAdditionalFields GetById(Int32 Id)
		{
			return Query<PaymentInfoAdditionalFields>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(PaymentInfoAdditionalFields model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<PaymentInfoAdditionalFields> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(PaymentInfoAdditionalFields model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(PaymentInfoAdditionalFields model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<PaymentInfoAdditionalFields> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.PaymentInfoAdditionalFields";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , FieldName , FieldValue , ShowValue , MaskValue , RegEx) VALUES (@ScraperRegion , @Country , @FieldName , @FieldValue , @ShowValue , @MaskValue , @RegEx) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScraperRegion=@ScraperRegion , Country=@Country , FieldName=@FieldName , FieldValue=@FieldValue , ShowValue=@ShowValue , MaskValue=@MaskValue , RegEx=@RegEx WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , FieldName , FieldValue , ShowValue , MaskValue , RegEx)  VALUES  (@ScraperRegion , @Country , @FieldName , @FieldValue , @ShowValue , @MaskValue , @RegEx) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class PaymentModeDap : BaseDap
	{
		public PaymentModeDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public PaymentModeDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public PaymentModeDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static PaymentMode[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<PaymentMode> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<PaymentMode>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<PaymentMode> GetTop(int count)
		{
			var queryResult = Query<PaymentMode>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<PaymentMode> ?? queryResult.ToList();
		}



		public PaymentMode GetByPaymentModeId(Int32 PaymentModeId)
		{
			return Query<PaymentMode>(SqlSelectCommand + " WHERE PaymentModeId=@PaymentModeId", new { PaymentModeId = PaymentModeId }).FirstOrDefault();
		}

		public void Insert(PaymentMode model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<PaymentMode> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the PaymentModeId from OUTPUT inserted.
/// </summary>
/// <returns>PaymentModeId from OUTPUT inserted.</returns>
public Int32 InsertWithId(PaymentMode model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 PaymentModeId)
		{
			Execute(SqlDeleteCommand, new { PaymentModeId = PaymentModeId });
		}

		public void Update(PaymentMode model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<PaymentMode> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.PaymentMode";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Name , Description) VALUES (@Name , @Description) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Name=@Name , Description=@Description WHERE PaymentModeId=@PaymentModeId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE PaymentModeId=@PaymentModeId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Name , Description)  VALUES  (@Name , @Description) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class RegionJobLimitDap : BaseDap
	{
		public RegionJobLimitDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public RegionJobLimitDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public RegionJobLimitDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static RegionJobLimit[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<RegionJobLimit> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<RegionJobLimit>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<RegionJobLimit> GetTop(int count)
		{
			var queryResult = Query<RegionJobLimit>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<RegionJobLimit> ?? queryResult.ToList();
		}



		public RegionJobLimit GetByCountry(String Country)
		{
			return Query<RegionJobLimit>(SqlSelectCommand + " WHERE Country=@Country", new { Country = Country }).FirstOrDefault();
		}

		public RegionJobLimit GetByRegion(String Region)
		{
			return Query<RegionJobLimit>(SqlSelectCommand + " WHERE Region=@Region", new { Region = Region }).FirstOrDefault();
		}

		public void Insert(RegionJobLimit model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<RegionJobLimit> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Country from OUTPUT inserted.
/// </summary>
/// <returns>Country from OUTPUT inserted.</returns>
public String InsertWithId(RegionJobLimit model)
{
    var result = Query<String>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(String Country, String Region)
		{
			Execute(SqlDeleteCommand, new { Country = Country, Region = Region });
		}

		public void Update(RegionJobLimit model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<RegionJobLimit> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.RegionJobLimit";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Country , Region , MaxJobs) VALUES (@Country , @Region , @MaxJobs) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Country=@Country , Region=@Region , MaxJobs=@MaxJobs WHERE Country=@Country AND Region=@Region";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Country=@Country AND Region=@Region";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Country , Region , MaxJobs)  VALUES  (@Country , @Region , @MaxJobs) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class RequiredFilingCalendarDataFieldDap : BaseDap
	{
		public RequiredFilingCalendarDataFieldDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public RequiredFilingCalendarDataFieldDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public RequiredFilingCalendarDataFieldDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static RequiredFilingCalendarDataField[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<RequiredFilingCalendarDataField> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<RequiredFilingCalendarDataField>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<RequiredFilingCalendarDataField> GetTop(int count)
		{
			var queryResult = Query<RequiredFilingCalendarDataField>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<RequiredFilingCalendarDataField> ?? queryResult.ToList();
		}



		public RequiredFilingCalendarDataField GetByRequiredFilingCalendarDataFieldId(Int32 RequiredFilingCalendarDataFieldId)
		{
			return Query<RequiredFilingCalendarDataField>(SqlSelectCommand + " WHERE RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId", new { RequiredFilingCalendarDataFieldId = RequiredFilingCalendarDataFieldId }).FirstOrDefault();
		}

		public void Insert(RequiredFilingCalendarDataField model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<RequiredFilingCalendarDataField> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the RequiredFilingCalendarDataFieldId from OUTPUT inserted.
/// </summary>
/// <returns>RequiredFilingCalendarDataFieldId from OUTPUT inserted.</returns>
public Int32 InsertWithId(RequiredFilingCalendarDataField model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 RequiredFilingCalendarDataFieldId)
		{
			Execute(SqlDeleteCommand, new { RequiredFilingCalendarDataFieldId = RequiredFilingCalendarDataFieldId });
		}

		public void Update(RequiredFilingCalendarDataField model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<RequiredFilingCalendarDataField> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.RequiredFilingCalendarDataField";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Name , Description) VALUES (@Name , @Description) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Name=@Name , Description=@Description WHERE RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Name , Description)  VALUES  (@Name , @Description) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class RoleDap : BaseDap
	{
		public RoleDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public RoleDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public RoleDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static Role[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<Role> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<Role>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<Role> GetTop(int count)
		{
			var queryResult = Query<Role>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<Role> ?? queryResult.ToList();
		}



		public Role GetById(Int32 Id)
		{
			return Query<Role>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(Role model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<Role> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(Role model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(Role model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<Role> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.Role";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (RoleName) VALUES (@RoleName) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET RoleName=@RoleName WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (RoleName)  VALUES  (@RoleName) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class SSResourceDap : BaseDap
	{
		public SSResourceDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public SSResourceDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public SSResourceDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static SSResource[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<SSResource> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<SSResource>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<SSResource> GetTop(int count)
		{
			var queryResult = Query<SSResource>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<SSResource> ?? queryResult.ToList();
		}



		public SSResource GetBySSResourceId(Int64 SSResourceId)
		{
			return Query<SSResource>(SqlSelectCommand + " WHERE SSResourceId=@SSResourceId", new { SSResourceId = SSResourceId }).FirstOrDefault();
		}

		public void Insert(SSResource model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<SSResource> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the SSResourceId from OUTPUT inserted.
/// </summary>
/// <returns>SSResourceId from OUTPUT inserted.</returns>
public Int64 InsertWithId(SSResource model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 SSResourceId)
		{
			Execute(SqlDeleteCommand, new { SSResourceId = SSResourceId });
		}

		public void Update(SSResource model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<SSResource> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.SSResource";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Name , Comments , JobId , FileKey , CreateDateTime , UpdateDateTime , FileType) VALUES (@Name , @Comments , @JobId , @FileKey , @CreateDateTime , @UpdateDateTime , @FileType) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Name=@Name , Comments=@Comments , JobId=@JobId , FileKey=@FileKey , CreateDateTime=@CreateDateTime , UpdateDateTime=@UpdateDateTime , FileType=@FileType WHERE SSResourceId=@SSResourceId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE SSResourceId=@SSResourceId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Name , Comments , JobId , FileKey , CreateDateTime , UpdateDateTime , FileType)  VALUES  (@Name , @Comments , @JobId , @FileKey , @CreateDateTime , @UpdateDateTime , @FileType) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class SSResourceArchiveDap : BaseDap
	{
		public SSResourceArchiveDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public SSResourceArchiveDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public SSResourceArchiveDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static SSResourceArchive[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<SSResourceArchive> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<SSResourceArchive>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<SSResourceArchive> GetTop(int count)
		{
			var queryResult = Query<SSResourceArchive>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<SSResourceArchive> ?? queryResult.ToList();
		}



		public SSResourceArchive GetBySSResourceId(Int64 SSResourceId)
		{
			return Query<SSResourceArchive>(SqlSelectCommand + " WHERE SSResourceId=@SSResourceId", new { SSResourceId = SSResourceId }).FirstOrDefault();
		}

		public void Insert(SSResourceArchive model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<SSResourceArchive> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the SSResourceId from OUTPUT inserted.
/// </summary>
/// <returns>SSResourceId from OUTPUT inserted.</returns>
public Int64 InsertWithId(SSResourceArchive model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 SSResourceId)
		{
			Execute(SqlDeleteCommand, new { SSResourceId = SSResourceId });
		}

		public void Update(SSResourceArchive model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<SSResourceArchive> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.SSResourceArchive";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (Name , Comments , JobId , FileKey , CreateDateTime , UpdateDateTime , FileType) VALUES (@Name , @Comments , @JobId , @FileKey , @CreateDateTime , @UpdateDateTime , @FileType) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET Name=@Name , Comments=@Comments , JobId=@JobId , FileKey=@FileKey , CreateDateTime=@CreateDateTime , UpdateDateTime=@UpdateDateTime , FileType=@FileType WHERE SSResourceId=@SSResourceId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE SSResourceId=@SSResourceId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (Name , Comments , JobId , FileKey , CreateDateTime , UpdateDateTime , FileType)  VALUES  (@Name , @Comments , @JobId , @FileKey , @CreateDateTime , @UpdateDateTime , @FileType) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class SSResourceTagsDap : BaseDap
	{
		public SSResourceTagsDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public SSResourceTagsDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public SSResourceTagsDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static SSResourceTags[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<SSResourceTags> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<SSResourceTags>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<SSResourceTags> GetTop(int count)
		{
			var queryResult = Query<SSResourceTags>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<SSResourceTags> ?? queryResult.ToList();
		}



		public SSResourceTags GetBySSResourceTagId(Int64 SSResourceTagId)
		{
			return Query<SSResourceTags>(SqlSelectCommand + " WHERE SSResourceTagId=@SSResourceTagId", new { SSResourceTagId = SSResourceTagId }).FirstOrDefault();
		}

		public void Insert(SSResourceTags model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<SSResourceTags> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the SSResourceTagId from OUTPUT inserted.
/// </summary>
/// <returns>SSResourceTagId from OUTPUT inserted.</returns>
public Int64 InsertWithId(SSResourceTags model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 SSResourceTagId)
		{
			Execute(SqlDeleteCommand, new { SSResourceTagId = SSResourceTagId });
		}

		public void Update(SSResourceTags model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<SSResourceTags> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.SSResourceTags";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (SSResourceId , TagId) VALUES (@SSResourceId , @TagId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET SSResourceId=@SSResourceId , TagId=@TagId WHERE SSResourceTagId=@SSResourceTagId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE SSResourceTagId=@SSResourceTagId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (SSResourceId , TagId)  VALUES  (@SSResourceId , @TagId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class SSResourceTagsArchiveDap : BaseDap
	{
		public SSResourceTagsArchiveDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public SSResourceTagsArchiveDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public SSResourceTagsArchiveDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static SSResourceTagsArchive[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<SSResourceTagsArchive> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<SSResourceTagsArchive>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<SSResourceTagsArchive> GetTop(int count)
		{
			var queryResult = Query<SSResourceTagsArchive>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<SSResourceTagsArchive> ?? queryResult.ToList();
		}



		public SSResourceTagsArchive GetBySSResourceTagId(Int64 SSResourceTagId)
		{
			return Query<SSResourceTagsArchive>(SqlSelectCommand + " WHERE SSResourceTagId=@SSResourceTagId", new { SSResourceTagId = SSResourceTagId }).FirstOrDefault();
		}

		public void Insert(SSResourceTagsArchive model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<SSResourceTagsArchive> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the SSResourceTagId from OUTPUT inserted.
/// </summary>
/// <returns>SSResourceTagId from OUTPUT inserted.</returns>
public Int64 InsertWithId(SSResourceTagsArchive model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 SSResourceTagId)
		{
			Execute(SqlDeleteCommand, new { SSResourceTagId = SSResourceTagId });
		}

		public void Update(SSResourceTagsArchive model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<SSResourceTagsArchive> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.SSResourceTagsArchive";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (SSResourceId , TagId) VALUES (@SSResourceId , @TagId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET SSResourceId=@SSResourceId , TagId=@TagId WHERE SSResourceTagId=@SSResourceTagId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE SSResourceTagId=@SSResourceTagId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (SSResourceId , TagId)  VALUES  (@SSResourceId , @TagId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class ScraperMetadataDap : BaseDap
	{
		public ScraperMetadataDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public ScraperMetadataDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public ScraperMetadataDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static ScraperMetadata[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<ScraperMetadata> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<ScraperMetadata>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<ScraperMetadata> GetTop(int count)
		{
			var queryResult = Query<ScraperMetadata>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<ScraperMetadata> ?? queryResult.ToList();
		}



		public ScraperMetadata GetByScraperRegion(String ScraperRegion)
		{
			return Query<ScraperMetadata>(SqlSelectCommand + " WHERE ScraperRegion=@ScraperRegion", new { ScraperRegion = ScraperRegion }).FirstOrDefault();
		}

		public void Insert(ScraperMetadata model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<ScraperMetadata> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ScraperRegion from OUTPUT inserted.
/// </summary>
/// <returns>ScraperRegion from OUTPUT inserted.</returns>
public String InsertWithId(ScraperMetadata model)
{
    var result = Query<String>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(String ScraperRegion)
		{
			Execute(SqlDeleteCommand, new { ScraperRegion = ScraperRegion });
		}

		public void Update(ScraperMetadata model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<ScraperMetadata> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.ScraperMetadata";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , MetadataJson) VALUES (@ScraperRegion , @Country , @MetadataJson) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScraperRegion=@ScraperRegion , Country=@Country , MetadataJson=@MetadataJson WHERE ScraperRegion=@ScraperRegion";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ScraperRegion=@ScraperRegion";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , MetadataJson)  VALUES  (@ScraperRegion , @Country , @MetadataJson) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class ScraperScriptsDap : BaseDap
	{
		public ScraperScriptsDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public ScraperScriptsDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public ScraperScriptsDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static ScraperScripts[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<ScraperScripts> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<ScraperScripts>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<ScraperScripts> GetTop(int count)
		{
			var queryResult = Query<ScraperScripts>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<ScraperScripts> ?? queryResult.ToList();
		}



		public ScraperScripts GetByScriptId(System.Guid ScriptId)
		{
			return Query<ScraperScripts>(SqlSelectCommand + " WHERE ScriptId=@ScriptId", new { ScriptId = ScriptId }).FirstOrDefault();
		}

		public void Insert(ScraperScripts model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<ScraperScripts> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ScriptId from OUTPUT inserted.
/// </summary>
/// <returns>ScriptId from OUTPUT inserted.</returns>
public System.Guid InsertWithId(ScraperScripts model)
{
    var result = Query<System.Guid>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(System.Guid ScriptId)
		{
			Execute(SqlDeleteCommand, new { ScriptId = ScriptId });
		}

		public void Update(ScraperScripts model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<ScraperScripts> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.ScraperScripts";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScriptId , ScriptVersion , ScraperRegion , ScriptChecksum , ScriptContent , IsActive , CreatedOn , CreatedBy , ModifiedBy , ModifiedOn) VALUES (@ScriptId , @ScriptVersion , @ScraperRegion , @ScriptChecksum , @ScriptContent , @IsActive , @CreatedOn , @CreatedBy , @ModifiedBy , @ModifiedOn) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScriptId=@ScriptId , ScriptVersion=@ScriptVersion , ScraperRegion=@ScraperRegion , ScriptChecksum=@ScriptChecksum , ScriptContent=@ScriptContent , IsActive=@IsActive , CreatedOn=@CreatedOn , CreatedBy=@CreatedBy , ModifiedBy=@ModifiedBy , ModifiedOn=@ModifiedOn WHERE ScriptId=@ScriptId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ScriptId=@ScriptId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScriptId , ScriptVersion , ScraperRegion , ScriptChecksum , ScriptContent , IsActive , CreatedOn , CreatedBy , ModifiedBy , ModifiedOn)  VALUES  (@ScriptId , @ScriptVersion , @ScraperRegion , @ScriptChecksum , @ScriptContent , @IsActive , @CreatedOn , @CreatedBy , @ModifiedBy , @ModifiedOn) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class ScraperStatusDap : BaseDap
	{
		public ScraperStatusDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public ScraperStatusDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public ScraperStatusDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static ScraperStatus[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<ScraperStatus> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<ScraperStatus>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<ScraperStatus> GetTop(int count)
		{
			var queryResult = Query<ScraperStatus>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<ScraperStatus> ?? queryResult.ToList();
		}



		public ScraperStatus GetByScraperRegion(String ScraperRegion)
		{
			return Query<ScraperStatus>(SqlSelectCommand + " WHERE ScraperRegion=@ScraperRegion", new { ScraperRegion = ScraperRegion }).FirstOrDefault();
		}

		public ScraperStatus GetByJobTypeId(Int32 JobTypeId)
		{
			return Query<ScraperStatus>(SqlSelectCommand + " WHERE JobTypeId=@JobTypeId", new { JobTypeId = JobTypeId }).FirstOrDefault();
		}

		public void Insert(ScraperStatus model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<ScraperStatus> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ScraperRegion from OUTPUT inserted.
/// </summary>
/// <returns>ScraperRegion from OUTPUT inserted.</returns>
public String InsertWithId(ScraperStatus model)
{
    var result = Query<String>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(String ScraperRegion, Int32 JobTypeId)
		{
			Execute(SqlDeleteCommand, new { ScraperRegion = ScraperRegion, JobTypeId = JobTypeId });
		}

		public void Update(ScraperStatus model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<ScraperStatus> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.ScraperStatus";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , JobTypeId , IsAvailable , IsTwoFactorAuth , Message) VALUES (@ScraperRegion , @Country , @JobTypeId , @IsAvailable , @IsTwoFactorAuth , @Message) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScraperRegion=@ScraperRegion , Country=@Country , JobTypeId=@JobTypeId , IsAvailable=@IsAvailable , IsTwoFactorAuth=@IsTwoFactorAuth , Message=@Message WHERE ScraperRegion=@ScraperRegion AND JobTypeId=@JobTypeId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ScraperRegion=@ScraperRegion AND JobTypeId=@JobTypeId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , JobTypeId , IsAvailable , IsTwoFactorAuth , Message)  VALUES  (@ScraperRegion , @Country , @JobTypeId , @IsAvailable , @IsTwoFactorAuth , @Message) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class ScraperStatusRequiredFieldsDap : BaseDap
	{
		public ScraperStatusRequiredFieldsDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public ScraperStatusRequiredFieldsDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public ScraperStatusRequiredFieldsDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static ScraperStatusRequiredFields[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<ScraperStatusRequiredFields> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<ScraperStatusRequiredFields>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<ScraperStatusRequiredFields> GetTop(int count)
		{
			var queryResult = Query<ScraperStatusRequiredFields>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<ScraperStatusRequiredFields> ?? queryResult.ToList();
		}



		public ScraperStatusRequiredFields GetByScraperRegion(String ScraperRegion)
		{
			return Query<ScraperStatusRequiredFields>(SqlSelectCommand + " WHERE ScraperRegion=@ScraperRegion", new { ScraperRegion = ScraperRegion }).FirstOrDefault();
		}

		public ScraperStatusRequiredFields GetByRequiredFilingCalendarDataFieldId(Int32 RequiredFilingCalendarDataFieldId)
		{
			return Query<ScraperStatusRequiredFields>(SqlSelectCommand + " WHERE RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId", new { RequiredFilingCalendarDataFieldId = RequiredFilingCalendarDataFieldId }).FirstOrDefault();
		}

		public ScraperStatusRequiredFields GetByJobTypeId(Int32 JobTypeId)
		{
			return Query<ScraperStatusRequiredFields>(SqlSelectCommand + " WHERE JobTypeId=@JobTypeId", new { JobTypeId = JobTypeId }).FirstOrDefault();
		}

		public void Insert(ScraperStatusRequiredFields model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<ScraperStatusRequiredFields> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ScraperRegion from OUTPUT inserted.
/// </summary>
/// <returns>ScraperRegion from OUTPUT inserted.</returns>
public String InsertWithId(ScraperStatusRequiredFields model)
{
    var result = Query<String>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(String ScraperRegion, Int32 RequiredFilingCalendarDataFieldId, Int32 JobTypeId)
		{
			Execute(SqlDeleteCommand, new { ScraperRegion = ScraperRegion, RequiredFilingCalendarDataFieldId = RequiredFilingCalendarDataFieldId, JobTypeId = JobTypeId });
		}

		public void Update(ScraperStatusRequiredFields model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<ScraperStatusRequiredFields> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.ScraperStatusRequiredFields";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , RequiredFilingCalendarDataFieldId , JobTypeId) VALUES (@ScraperRegion , @RequiredFilingCalendarDataFieldId , @JobTypeId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScraperRegion=@ScraperRegion , RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId , JobTypeId=@JobTypeId WHERE ScraperRegion=@ScraperRegion AND RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId AND JobTypeId=@JobTypeId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ScraperRegion=@ScraperRegion AND RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId AND JobTypeId=@JobTypeId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , RequiredFilingCalendarDataFieldId , JobTypeId)  VALUES  (@ScraperRegion , @RequiredFilingCalendarDataFieldId , @JobTypeId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class ScraperTaxFormDap : BaseDap
	{
		public ScraperTaxFormDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public ScraperTaxFormDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public ScraperTaxFormDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static ScraperTaxForm[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<ScraperTaxForm> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<ScraperTaxForm>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<ScraperTaxForm> GetTop(int count)
		{
			var queryResult = Query<ScraperTaxForm>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<ScraperTaxForm> ?? queryResult.ToList();
		}



		public ScraperTaxForm GetByScraperRegion(String ScraperRegion)
		{
			return Query<ScraperTaxForm>(SqlSelectCommand + " WHERE ScraperRegion=@ScraperRegion", new { ScraperRegion = ScraperRegion }).FirstOrDefault();
		}

		public ScraperTaxForm GetByJobTypeId(Int32 JobTypeId)
		{
			return Query<ScraperTaxForm>(SqlSelectCommand + " WHERE JobTypeId=@JobTypeId", new { JobTypeId = JobTypeId }).FirstOrDefault();
		}

		public ScraperTaxForm GetByTaxFormCode(String TaxFormCode)
		{
			return Query<ScraperTaxForm>(SqlSelectCommand + " WHERE TaxFormCode=@TaxFormCode", new { TaxFormCode = TaxFormCode }).FirstOrDefault();
		}

		public void Insert(ScraperTaxForm model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<ScraperTaxForm> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ScraperRegion from OUTPUT inserted.
/// </summary>
/// <returns>ScraperRegion from OUTPUT inserted.</returns>
public String InsertWithId(ScraperTaxForm model)
{
    var result = Query<String>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(String ScraperRegion, Int32 JobTypeId, String TaxFormCode)
		{
			Execute(SqlDeleteCommand, new { ScraperRegion = ScraperRegion, JobTypeId = JobTypeId, TaxFormCode = TaxFormCode });
		}

		public void Update(ScraperTaxForm model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<ScraperTaxForm> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.ScraperTaxForm";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , JobTypeId , TaxFormCode , LegacyReturnName) VALUES (@ScraperRegion , @Country , @JobTypeId , @TaxFormCode , @LegacyReturnName) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScraperRegion=@ScraperRegion , Country=@Country , JobTypeId=@JobTypeId , TaxFormCode=@TaxFormCode , LegacyReturnName=@LegacyReturnName WHERE ScraperRegion=@ScraperRegion AND JobTypeId=@JobTypeId AND TaxFormCode=@TaxFormCode";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ScraperRegion=@ScraperRegion AND JobTypeId=@JobTypeId AND TaxFormCode=@TaxFormCode";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , JobTypeId , TaxFormCode , LegacyReturnName)  VALUES  (@ScraperRegion , @Country , @JobTypeId , @TaxFormCode , @LegacyReturnName) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class SkyscraperAuditHistoryDap : BaseDap
	{
		public SkyscraperAuditHistoryDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public SkyscraperAuditHistoryDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public SkyscraperAuditHistoryDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static SkyscraperAuditHistory[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<SkyscraperAuditHistory> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<SkyscraperAuditHistory>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<SkyscraperAuditHistory> GetTop(int count)
		{
			var queryResult = Query<SkyscraperAuditHistory>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<SkyscraperAuditHistory> ?? queryResult.ToList();
		}



		public SkyscraperAuditHistory GetByid(Int64 id)
		{
			return Query<SkyscraperAuditHistory>(SqlSelectCommand + " WHERE id=@id", new { id = id }).FirstOrDefault();
		}

		public void Insert(SkyscraperAuditHistory model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<SkyscraperAuditHistory> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the id from OUTPUT inserted.
/// </summary>
/// <returns>id from OUTPUT inserted.</returns>
public Int64 InsertWithId(SkyscraperAuditHistory model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 id)
		{
			Execute(SqlDeleteCommand, new { id = id });
		}

		public void Update(SkyscraperAuditHistory model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<SkyscraperAuditHistory> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.SkyscraperAuditHistory";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (PrincipleTableName , PrincipleId , HistoryJson , CreatedDateTime , CreatedUserId , UserSystemName) VALUES (@PrincipleTableName , @PrincipleId , @HistoryJson , @CreatedDateTime , @CreatedUserId , @UserSystemName) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET PrincipleTableName=@PrincipleTableName , PrincipleId=@PrincipleId , HistoryJson=@HistoryJson , CreatedDateTime=@CreatedDateTime , CreatedUserId=@CreatedUserId , UserSystemName=@UserSystemName WHERE id=@id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE id=@id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (PrincipleTableName , PrincipleId , HistoryJson , CreatedDateTime , CreatedUserId , UserSystemName)  VALUES  (@PrincipleTableName , @PrincipleId , @HistoryJson , @CreatedDateTime , @CreatedUserId , @UserSystemName) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class SkyscraperUserDap : BaseDap
	{
		public SkyscraperUserDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public SkyscraperUserDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public SkyscraperUserDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static SkyscraperUser[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<SkyscraperUser> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<SkyscraperUser>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<SkyscraperUser> GetTop(int count)
		{
			var queryResult = Query<SkyscraperUser>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<SkyscraperUser> ?? queryResult.ToList();
		}



		public SkyscraperUser GetBySkyscraperUserId(Int64 SkyscraperUserId)
		{
			return Query<SkyscraperUser>(SqlSelectCommand + " WHERE SkyscraperUserId=@SkyscraperUserId", new { SkyscraperUserId = SkyscraperUserId }).FirstOrDefault();
		}

		public void Insert(SkyscraperUser model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<SkyscraperUser> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the SkyscraperUserId from OUTPUT inserted.
/// </summary>
/// <returns>SkyscraperUserId from OUTPUT inserted.</returns>
public Int64 InsertWithId(SkyscraperUser model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 SkyscraperUserId)
		{
			Execute(SqlDeleteCommand, new { SkyscraperUserId = SkyscraperUserId });
		}

		public void Update(SkyscraperUser model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<SkyscraperUser> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.SkyscraperUser";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (AvaTaxUserId , UserName , FirstName , LastName , AvaTaxAccountId , AvaTaxUserRoleId , CreateDateTime , UpdateDateTime , LastLoginDateTime , AISubjectId) VALUES (@AvaTaxUserId , @UserName , @FirstName , @LastName , @AvaTaxAccountId , @AvaTaxUserRoleId , @CreateDateTime , @UpdateDateTime , @LastLoginDateTime , @AISubjectId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET AvaTaxUserId=@AvaTaxUserId , UserName=@UserName , FirstName=@FirstName , LastName=@LastName , AvaTaxAccountId=@AvaTaxAccountId , AvaTaxUserRoleId=@AvaTaxUserRoleId , CreateDateTime=@CreateDateTime , UpdateDateTime=@UpdateDateTime , LastLoginDateTime=@LastLoginDateTime , AISubjectId=@AISubjectId WHERE SkyscraperUserId=@SkyscraperUserId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE SkyscraperUserId=@SkyscraperUserId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (AvaTaxUserId , UserName , FirstName , LastName , AvaTaxAccountId , AvaTaxUserRoleId , CreateDateTime , UpdateDateTime , LastLoginDateTime , AISubjectId)  VALUES  (@AvaTaxUserId , @UserName , @FirstName , @LastName , @AvaTaxAccountId , @AvaTaxUserRoleId , @CreateDateTime , @UpdateDateTime , @LastLoginDateTime , @AISubjectId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class TagsDap : BaseDap
	{
		public TagsDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public TagsDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public TagsDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static Tags[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<Tags> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<Tags>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<Tags> GetTop(int count)
		{
			var queryResult = Query<Tags>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<Tags> ?? queryResult.ToList();
		}



		public Tags GetByTagId(Int32 TagId)
		{
			return Query<Tags>(SqlSelectCommand + " WHERE TagId=@TagId", new { TagId = TagId }).FirstOrDefault();
		}

		public void Insert(Tags model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<Tags> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the TagId from OUTPUT inserted.
/// </summary>
/// <returns>TagId from OUTPUT inserted.</returns>
public Int32 InsertWithId(Tags model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 TagId)
		{
			Execute(SqlDeleteCommand, new { TagId = TagId });
		}

		public void Update(Tags model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<Tags> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public Tags GetByTagNameIndex(String TagName)
		{
			return Query<Tags>(SqlSelectCommand + " WHERE TagName=@TagName", new { TagName = TagName }).FirstOrDefault();
		}

		public void DeleteByTagName(String TagName)
		{
			Execute(SqlDeleteCommand, new { TagName = TagName });
		}

		public const string SqlTableName = "Skyscraper.Tags";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (TagName) VALUES (@TagName) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET TagName=@TagName WHERE TagId=@TagId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE TagId=@TagId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (TagName)  VALUES  (@TagName) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class TaxFormDap : BaseDap
	{
		public TaxFormDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public TaxFormDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public TaxFormDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static TaxForm[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<TaxForm> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<TaxForm>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<TaxForm> GetTop(int count)
		{
			var queryResult = Query<TaxForm>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<TaxForm> ?? queryResult.ToList();
		}



		public TaxForm GetByTaxFormId(Int32 TaxFormId)
		{
			return Query<TaxForm>(SqlSelectCommand + " WHERE TaxFormId=@TaxFormId", new { TaxFormId = TaxFormId }).FirstOrDefault();
		}

		public void Insert(TaxForm model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<TaxForm> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the TaxFormId from OUTPUT inserted.
/// </summary>
/// <returns>TaxFormId from OUTPUT inserted.</returns>
public Int32 InsertWithId(TaxForm model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 TaxFormId)
		{
			Execute(SqlDeleteCommand, new { TaxFormId = TaxFormId });
		}

		public void Update(TaxForm model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<TaxForm> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.TaxForm";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (TaxFormCode , LegacyReturnName , Country , Region , ScraperRegion , IsWebFilingAvailable , FileUpload , WebfilingAccount , IsDefaultTaxForm , FilingDisabledReason , IsWebfileForm) VALUES (@TaxFormCode , @LegacyReturnName , @Country , @Region , @ScraperRegion , @IsWebFilingAvailable , @FileUpload , @WebfilingAccount , @IsDefaultTaxForm , @FilingDisabledReason , @IsWebfileForm) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET TaxFormCode=@TaxFormCode , LegacyReturnName=@LegacyReturnName , Country=@Country , Region=@Region , ScraperRegion=@ScraperRegion , IsWebFilingAvailable=@IsWebFilingAvailable , FileUpload=@FileUpload , WebfilingAccount=@WebfilingAccount , IsDefaultTaxForm=@IsDefaultTaxForm , FilingDisabledReason=@FilingDisabledReason , IsWebfileForm=@IsWebfileForm WHERE TaxFormId=@TaxFormId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE TaxFormId=@TaxFormId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (TaxFormCode , LegacyReturnName , Country , Region , ScraperRegion , IsWebFilingAvailable , FileUpload , WebfilingAccount , IsDefaultTaxForm , FilingDisabledReason , IsWebfileForm)  VALUES  (@TaxFormCode , @LegacyReturnName , @Country , @Region , @ScraperRegion , @IsWebFilingAvailable , @FileUpload , @WebfilingAccount , @IsDefaultTaxForm , @FilingDisabledReason , @IsWebfileForm) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class TaxFormBulkAccountDap : BaseDap
	{
		public TaxFormBulkAccountDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public TaxFormBulkAccountDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public TaxFormBulkAccountDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static TaxFormBulkAccount[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<TaxFormBulkAccount> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<TaxFormBulkAccount>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<TaxFormBulkAccount> GetTop(int count)
		{
			var queryResult = Query<TaxFormBulkAccount>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<TaxFormBulkAccount> ?? queryResult.ToList();
		}


		public void Insert(TaxFormBulkAccount model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<TaxFormBulkAccount> models)
		{
			Execute(SqlInsertCommand, models);
		}




		public const string SqlTableName = "Skyscraper.TaxFormBulkAccount";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " @TaxFormId , @BulkAccountId ";
		
	}

	public partial class TaxFormFilingModeDap : BaseDap
	{
		public TaxFormFilingModeDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public TaxFormFilingModeDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public TaxFormFilingModeDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static TaxFormFilingMode[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<TaxFormFilingMode> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<TaxFormFilingMode>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<TaxFormFilingMode> GetTop(int count)
		{
			var queryResult = Query<TaxFormFilingMode>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<TaxFormFilingMode> ?? queryResult.ToList();
		}



		public TaxFormFilingMode GetByTaxFormId(Int32 TaxFormId)
		{
			return Query<TaxFormFilingMode>(SqlSelectCommand + " WHERE TaxFormId=@TaxFormId", new { TaxFormId = TaxFormId }).FirstOrDefault();
		}

		public TaxFormFilingMode GetByFilingModeId(Int32 FilingModeId)
		{
			return Query<TaxFormFilingMode>(SqlSelectCommand + " WHERE FilingModeId=@FilingModeId", new { FilingModeId = FilingModeId }).FirstOrDefault();
		}

		public void Insert(TaxFormFilingMode model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<TaxFormFilingMode> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the TaxFormId from OUTPUT inserted.
/// </summary>
/// <returns>TaxFormId from OUTPUT inserted.</returns>
public Int32 InsertWithId(TaxFormFilingMode model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 TaxFormId, Int32 FilingModeId)
		{
			Execute(SqlDeleteCommand, new { TaxFormId = TaxFormId, FilingModeId = FilingModeId });
		}

		public void Update(TaxFormFilingMode model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<TaxFormFilingMode> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.TaxFormFilingMode";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (TaxFormId , FilingModeId) VALUES (@TaxFormId , @FilingModeId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET TaxFormId=@TaxFormId , FilingModeId=@FilingModeId WHERE TaxFormId=@TaxFormId AND FilingModeId=@FilingModeId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE TaxFormId=@TaxFormId AND FilingModeId=@FilingModeId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (TaxFormId , FilingModeId)  VALUES  (@TaxFormId , @FilingModeId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class TaxFormPaymentModeDap : BaseDap
	{
		public TaxFormPaymentModeDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public TaxFormPaymentModeDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public TaxFormPaymentModeDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static TaxFormPaymentMode[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<TaxFormPaymentMode> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<TaxFormPaymentMode>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<TaxFormPaymentMode> GetTop(int count)
		{
			var queryResult = Query<TaxFormPaymentMode>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<TaxFormPaymentMode> ?? queryResult.ToList();
		}



		public TaxFormPaymentMode GetByTaxFormId(Int32 TaxFormId)
		{
			return Query<TaxFormPaymentMode>(SqlSelectCommand + " WHERE TaxFormId=@TaxFormId", new { TaxFormId = TaxFormId }).FirstOrDefault();
		}

		public TaxFormPaymentMode GetByPaymentModeId(Int32 PaymentModeId)
		{
			return Query<TaxFormPaymentMode>(SqlSelectCommand + " WHERE PaymentModeId=@PaymentModeId", new { PaymentModeId = PaymentModeId }).FirstOrDefault();
		}

		public void Insert(TaxFormPaymentMode model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<TaxFormPaymentMode> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the TaxFormId from OUTPUT inserted.
/// </summary>
/// <returns>TaxFormId from OUTPUT inserted.</returns>
public Int32 InsertWithId(TaxFormPaymentMode model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 TaxFormId, Int32 PaymentModeId)
		{
			Execute(SqlDeleteCommand, new { TaxFormId = TaxFormId, PaymentModeId = PaymentModeId });
		}

		public void Update(TaxFormPaymentMode model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<TaxFormPaymentMode> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.TaxFormPaymentMode";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (TaxFormId , PaymentModeId) VALUES (@TaxFormId , @PaymentModeId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET TaxFormId=@TaxFormId , PaymentModeId=@PaymentModeId WHERE TaxFormId=@TaxFormId AND PaymentModeId=@PaymentModeId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE TaxFormId=@TaxFormId AND PaymentModeId=@PaymentModeId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (TaxFormId , PaymentModeId)  VALUES  (@TaxFormId , @PaymentModeId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class TaxFormRequiredFilingCalendarDataFieldDap : BaseDap
	{
		public TaxFormRequiredFilingCalendarDataFieldDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public TaxFormRequiredFilingCalendarDataFieldDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public TaxFormRequiredFilingCalendarDataFieldDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static TaxFormRequiredFilingCalendarDataField[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<TaxFormRequiredFilingCalendarDataField> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<TaxFormRequiredFilingCalendarDataField>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<TaxFormRequiredFilingCalendarDataField> GetTop(int count)
		{
			var queryResult = Query<TaxFormRequiredFilingCalendarDataField>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<TaxFormRequiredFilingCalendarDataField> ?? queryResult.ToList();
		}



		public TaxFormRequiredFilingCalendarDataField GetByTaxFormId(Int32 TaxFormId)
		{
			return Query<TaxFormRequiredFilingCalendarDataField>(SqlSelectCommand + " WHERE TaxFormId=@TaxFormId", new { TaxFormId = TaxFormId }).FirstOrDefault();
		}

		public TaxFormRequiredFilingCalendarDataField GetByRequiredFilingCalendarDataFieldId(Int32 RequiredFilingCalendarDataFieldId)
		{
			return Query<TaxFormRequiredFilingCalendarDataField>(SqlSelectCommand + " WHERE RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId", new { RequiredFilingCalendarDataFieldId = RequiredFilingCalendarDataFieldId }).FirstOrDefault();
		}

		public void Insert(TaxFormRequiredFilingCalendarDataField model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<TaxFormRequiredFilingCalendarDataField> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the TaxFormId from OUTPUT inserted.
/// </summary>
/// <returns>TaxFormId from OUTPUT inserted.</returns>
public Int32 InsertWithId(TaxFormRequiredFilingCalendarDataField model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 TaxFormId, Int32 RequiredFilingCalendarDataFieldId)
		{
			Execute(SqlDeleteCommand, new { TaxFormId = TaxFormId, RequiredFilingCalendarDataFieldId = RequiredFilingCalendarDataFieldId });
		}

		public void Update(TaxFormRequiredFilingCalendarDataField model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<TaxFormRequiredFilingCalendarDataField> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.TaxFormRequiredFilingCalendarDataField";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (TaxFormId , RequiredFilingCalendarDataFieldId) VALUES (@TaxFormId , @RequiredFilingCalendarDataFieldId) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET TaxFormId=@TaxFormId , RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId WHERE TaxFormId=@TaxFormId AND RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE TaxFormId=@TaxFormId AND RequiredFilingCalendarDataFieldId=@RequiredFilingCalendarDataFieldId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (TaxFormId , RequiredFilingCalendarDataFieldId)  VALUES  (@TaxFormId , @RequiredFilingCalendarDataFieldId) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class TaxFormRequiredReturnsDataKeysDap : BaseDap
	{
		public TaxFormRequiredReturnsDataKeysDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public TaxFormRequiredReturnsDataKeysDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public TaxFormRequiredReturnsDataKeysDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static TaxFormRequiredReturnsDataKeys[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<TaxFormRequiredReturnsDataKeys> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<TaxFormRequiredReturnsDataKeys>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<TaxFormRequiredReturnsDataKeys> GetTop(int count)
		{
			var queryResult = Query<TaxFormRequiredReturnsDataKeys>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<TaxFormRequiredReturnsDataKeys> ?? queryResult.ToList();
		}



		public TaxFormRequiredReturnsDataKeys GetByTaxFormCode(String TaxFormCode)
		{
			return Query<TaxFormRequiredReturnsDataKeys>(SqlSelectCommand + " WHERE TaxFormCode=@TaxFormCode", new { TaxFormCode = TaxFormCode }).FirstOrDefault();
		}

		public void Insert(TaxFormRequiredReturnsDataKeys model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<TaxFormRequiredReturnsDataKeys> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the TaxFormCode from OUTPUT inserted.
/// </summary>
/// <returns>TaxFormCode from OUTPUT inserted.</returns>
public String InsertWithId(TaxFormRequiredReturnsDataKeys model)
{
    var result = Query<String>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(String TaxFormCode)
		{
			Execute(SqlDeleteCommand, new { TaxFormCode = TaxFormCode });
		}

		public void Update(TaxFormRequiredReturnsDataKeys model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<TaxFormRequiredReturnsDataKeys> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.TaxFormRequiredReturnsDataKeys";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (TaxFormCode , ReturnsDataKeys) VALUES (@TaxFormCode , @ReturnsDataKeys) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET TaxFormCode=@TaxFormCode , ReturnsDataKeys=@ReturnsDataKeys WHERE TaxFormCode=@TaxFormCode";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE TaxFormCode=@TaxFormCode";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (TaxFormCode , ReturnsDataKeys)  VALUES  (@TaxFormCode , @ReturnsDataKeys) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class UserRolesDap : BaseDap
	{
		public UserRolesDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public UserRolesDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public UserRolesDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static UserRoles[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<UserRoles> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<UserRoles>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<UserRoles> GetTop(int count)
		{
			var queryResult = Query<UserRoles>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<UserRoles> ?? queryResult.ToList();
		}



		public UserRoles GetById(Int32 Id)
		{
			return Query<UserRoles>(SqlSelectCommand + " WHERE Id=@Id", new { Id = Id }).FirstOrDefault();
		}

		public void Insert(UserRoles model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<UserRoles> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the Id from OUTPUT inserted.
/// </summary>
/// <returns>Id from OUTPUT inserted.</returns>
public Int32 InsertWithId(UserRoles model)
{
    var result = Query<Int32>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int32 Id)
		{
			Execute(SqlDeleteCommand, new { Id = Id });
		}

		public void Update(UserRoles model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<UserRoles> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.UserRoles";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (RoleId , SkyscraperUserId , DepartmentId , ExpirationDate , ModifiedDate) VALUES (@RoleId , @SkyscraperUserId , @DepartmentId , @ExpirationDate , @ModifiedDate) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET RoleId=@RoleId , SkyscraperUserId=@SkyscraperUserId , DepartmentId=@DepartmentId , ExpirationDate=@ExpirationDate , ModifiedDate=@ModifiedDate WHERE Id=@Id";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE Id=@Id";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (RoleId , SkyscraperUserId , DepartmentId , ExpirationDate , ModifiedDate)  VALUES  (@RoleId , @SkyscraperUserId , @DepartmentId , @ExpirationDate , @ModifiedDate) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class WebFileDataDap : BaseDap
	{
		public WebFileDataDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public WebFileDataDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public WebFileDataDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static WebFileData[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<WebFileData> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<WebFileData>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<WebFileData> GetTop(int count)
		{
			var queryResult = Query<WebFileData>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<WebFileData> ?? queryResult.ToList();
		}



		public WebFileData GetByJobId(Int64 JobId)
		{
			return Query<WebFileData>(SqlSelectCommand + " WHERE JobId=@JobId", new { JobId = JobId }).FirstOrDefault();
		}

		public void Insert(WebFileData model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<WebFileData> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the JobId from OUTPUT inserted.
/// </summary>
/// <returns>JobId from OUTPUT inserted.</returns>
public Int64 InsertWithId(WebFileData model)
{
    var result = Query<Int64>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(Int64 JobId)
		{
			Execute(SqlDeleteCommand, new { JobId = JobId });
		}

		public void Update(WebFileData model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<WebFileData> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.WebFileData";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (JobId , FilingData , CreateDateTime) VALUES (@JobId , @FilingData , @CreateDateTime) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET JobId=@JobId , FilingData=@FilingData , CreateDateTime=@CreateDateTime WHERE JobId=@JobId";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE JobId=@JobId";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (JobId , FilingData , CreateDateTime)  VALUES  (@JobId , @FilingData , @CreateDateTime) ; SELECT LAST_INSERT_ID();";

		
	}

	public partial class WebFileFormStatusDap : BaseDap
	{
		public WebFileFormStatusDap(IDbConnection connection) : base(connection)
		{
			Connection = connection;
		}

		public WebFileFormStatusDap(IDbTransaction transaction) : base(transaction.Connection)
		{
			Transaction = transaction;
			Connection = transaction.Connection;
		}

		public WebFileFormStatusDap(BaseDap dapProvider) : base(dapProvider.Connection)
		{
			Transaction = dapProvider.Transaction;
			Connection = dapProvider.Connection;
		}

        private static WebFileFormStatus[] _obj_cache;
        private static readonly object _cache_lock = new object();
        private static DateTime _next_cache_date = DateTime.MinValue;
        
        public List<WebFileFormStatus> GetAll(bool force_cache_reload, int CACHE_SECONDS = 300)
        {
            if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                lock (_cache_lock) {
                    if (_obj_cache == null || force_cache_reload || _next_cache_date < DateTime.UtcNow) {
                        _obj_cache = Query<WebFileFormStatus>(string.Format("SELECT * FROM {0}", SqlTableName)).ToArray();
                        _next_cache_date = DateTime.UtcNow.AddSeconds(CACHE_SECONDS);
                    }
                }
            }
            return _obj_cache.ToList();
        }

        public void ResetCache()
		    {
            lock (_cache_lock) {
                _next_cache_date = DateTime.MinValue;
                _obj_cache = null;
            }
		    }


    public List<WebFileFormStatus> GetTop(int count)
		{
			var queryResult = Query<WebFileFormStatus>(string.Format("SELECT * FROM {1}  LIMIT {0}", count, SqlTableName));
			return queryResult as List<WebFileFormStatus> ?? queryResult.ToList();
		}



		public WebFileFormStatus GetByScraperRegion(String ScraperRegion)
		{
			return Query<WebFileFormStatus>(SqlSelectCommand + " WHERE ScraperRegion=@ScraperRegion", new { ScraperRegion = ScraperRegion }).FirstOrDefault();
		}

		public WebFileFormStatus GetByForm(String Form)
		{
			return Query<WebFileFormStatus>(SqlSelectCommand + " WHERE Form=@Form", new { Form = Form }).FirstOrDefault();
		}

		public void Insert(WebFileFormStatus model)
		{
			Execute(SqlInsertCommand, model);
		}

		public void Insert(IEnumerable<WebFileFormStatus> models)
		{
			Execute(SqlInsertCommand, models);
		}

/// <summary>
/// Insert this record and return the ScraperRegion from OUTPUT inserted.
/// </summary>
/// <returns>ScraperRegion from OUTPUT inserted.</returns>
public String InsertWithId(WebFileFormStatus model)
{
    var result = Query<String>(SqlInsertWithIdCommand, model);
    ResetCache();
    return result.FirstOrDefault();
}
        
		public void Delete(String ScraperRegion, String Form)
		{
			Execute(SqlDeleteCommand, new { ScraperRegion = ScraperRegion, Form = Form });
		}

		public void Update(WebFileFormStatus model)
		{
			Execute(SqlUpdateCommand, model);
		}

		public void Update(IEnumerable<WebFileFormStatus> models)
		{
			Execute(SqlUpdateCommand, models);
		}




		public const string SqlTableName = "Skyscraper.WebFileFormStatus";
		public const string SqlSelectCommand = "SELECT * FROM " + SqlTableName;
		public const string SqlInsertCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , Form , IsAvailable , EFile , EPay , FileUpload) VALUES (@ScraperRegion , @Country , @Form , @IsAvailable , @EFile , @EPay , @FileUpload) ";
		public const string SqlUpdateCommand = "UPDATE " + SqlTableName + " SET ScraperRegion=@ScraperRegion , Country=@Country , Form=@Form , IsAvailable=@IsAvailable , EFile=@EFile , EPay=@EPay , FileUpload=@FileUpload WHERE ScraperRegion=@ScraperRegion AND Form=@Form";
		public const string SqlDeleteCommand = "DELETE FROM " + SqlTableName + " WHERE ScraperRegion=@ScraperRegion AND Form=@Form";
public const string SqlInsertWithIdCommand = "INSERT INTO " + SqlTableName + " (ScraperRegion , Country , Form , IsAvailable , EFile , EPay , FileUpload)  VALUES  (@ScraperRegion , @Country , @Form , @IsAvailable , @EFile , @EPay , @FileUpload) ; SELECT LAST_INSERT_ID();";

		
	}

}
