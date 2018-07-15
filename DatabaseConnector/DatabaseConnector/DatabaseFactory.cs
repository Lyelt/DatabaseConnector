using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseConnector
{
    public static class DatabaseFactory
    {
        public static string DefaultConnectionString { get; set; }

        public static DatabaseConnector GetConnector()
        {
            if (DefaultConnectionString == null)
            {
                throw new InvalidOperationException($"If you do not specify a connection string, you must set the {nameof(DefaultConnectionString)} property before requesting a database connector.");
            }

            return new DatabaseConnector(DefaultConnectionString);
        }

        public static DatabaseConnector GetConnector(string connectionString)
        {
            return new DatabaseConnector(connectionString);
        }

        
    }
}
