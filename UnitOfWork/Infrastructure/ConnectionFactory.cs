using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace UnitOfWorks.Infrastructure
{
    public class ConnectionFactory
    {
        private readonly Dialect _dialect;
        private readonly ConnectionSelect _connectionSelect;

        public ConnectionFactory(Dialect dialect, ConnectionSelect connectionSelect)
        {
            _dialect = dialect;
            _connectionSelect = connectionSelect;
        }

        public DbConnection CreateDbConn()
        {
            switch (_dialect)
            {
                case Dialect.SqlServer:
                    return new SqlConnection(GetConnectionString());

                case Dialect.SqlLite:
                    return new SQLiteConnection(GetConnectionString());

                default:
                    return new SqlConnection(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            switch (_connectionSelect)
            {
                case ConnectionSelect.MsSqlTest:
                    return Properties.Settings.Default.TestMsDb;

                case ConnectionSelect.MsSqlLive:
                    return Properties.Settings.Default.LiveMsDb;

                case ConnectionSelect.SqlLiteLocalDb:
                    return $"data source={Properties.Settings.Default.SqlLiteDbPath}";

                default:
                    return Properties.Settings.Default.LiveMsDb;
            }
        }
    }
}
