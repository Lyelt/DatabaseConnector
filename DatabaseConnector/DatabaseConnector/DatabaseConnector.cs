using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DatabaseConnector
{
    public class DatabaseConnector : IDisposable
    {
        SqlConnectionStringBuilder _connBuilder;
        
        public SqlConnection Connection { get; }

        public DatabaseConnector(string connectionString)
        {
            try
            {
                _connBuilder = new SqlConnectionStringBuilder(connectionString);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"The following connection string is invalid: {connectionString}", ex);
            }

            Connection = new SqlConnection(_connBuilder.ConnectionString);
        }

        public SqlCommand BuildCommand(string commandText)
        {
            VerifyCommandText(commandText);

            var cmd = new SqlCommand(commandText, Connection);
            cmd.CommandType = CommandType.Text;

            return cmd;
        }

        public SqlDataReader GetReader(SqlCommand command)
        {
            return command.ExecuteReader();
        }

        private void VerifyCommandText(string commandText)
        {
            if (string.IsNullOrWhiteSpace(commandText))
                throw new ArgumentNullException(commandText, "Sql command text cannot be null or empty.");
        }

        #region IDisposable
        public void Dispose()
        {
            Connection?.Close();
            Connection?.Dispose();
        }
        #endregion
    }
}
