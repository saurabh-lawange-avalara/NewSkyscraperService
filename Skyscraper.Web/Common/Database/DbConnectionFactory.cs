using Avalara.Skyscraper.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace Avalara.Skyscraper.Web.Common
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection Connection { get; set; }

        public DbConnectionFactory(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
    }
}
