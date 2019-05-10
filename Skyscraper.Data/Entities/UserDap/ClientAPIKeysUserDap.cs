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
	/// <summary>
	/// User custom methods for ClientAPIKeysDap
	/// </summary>
	partial class ClientAPIKeysDap
	{
        public ClientAPIKeys GetByApiKey(string apikey)
        {
            return Query<ClientAPIKeys>(SqlSelectCommand + " WHERE `APIKey`=@ApiKey", new { ApiKey = apikey }).FirstOrDefault();
        }
        public ClientAPIKeys GetByAppName(string AppName)
        {
            return Query<ClientAPIKeys>(SqlSelectCommand + " WHERE `AppName`=@AppName", new { AppName = AppName }).FirstOrDefault();
        }
    }
}
