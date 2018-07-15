using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DatabaseConnector;
using System.Data.SqlClient;
using System.Data;

namespace DatabaseConnectorTests
{
    [TestFixture]
    public class ConnectorTests
    {
        private const string CONN_STR = @"Data Source=NICK-HOME-PC;Initial Catalog=Lyelt;Integrated Security=True";
        private const string TEST_SQL = @"SELECT * FROM Budgets";

        [OneTimeSetUp]
        public void Setup()
        {
            DatabaseFactory.DefaultConnectionString = CONN_STR;
        }

        [Test]
        public void TestGetConnector()
        {
            Assert.DoesNotThrow(() =>
            {
                using (var dbc = DatabaseFactory.GetConnector())
                {

                }
            });
        }

        [Test]
        public void TestGetConnectorThrows()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                using (var dbc = DatabaseFactory.GetConnector("INVALID"))
                {

                }
            });
        }

        [Test]
        public void TestInvalidCommandThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                using (var dbc = DatabaseFactory.GetConnector())
                using (var cmd = dbc.BuildCommand(null))
                {

                }
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                using (var dbc = DatabaseFactory.GetConnector())
                using (var cmd = dbc.BuildCommand(string.Empty))
                {

                }
            });
        }

        [Test]
        public void TestBuildCommand()
        {
            using (var dbc = DatabaseFactory.GetConnector())
            using (var cmd = dbc.BuildCommand(TEST_SQL))
            {
                Assert.IsNotNull(cmd);
                Assert.That(cmd.Connection.ClientConnectionId == dbc.Connection.ClientConnectionId);
                Assert.That(cmd.CommandText == TEST_SQL);
                Assert.That(cmd.CommandType == CommandType.Text);
            }
        }
    }
}
