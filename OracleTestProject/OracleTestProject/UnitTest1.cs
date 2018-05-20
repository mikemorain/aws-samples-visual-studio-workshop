using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;

namespace OracleTestProject
{
    [TestFixture]
    public class OracleTests
    {
        private OracleConnection _connection;
        [SetUp]
        public void SetupOracleConnection()
        {
            _connection = new OracleConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        [Test, Order(1)]
        public void AddPersonTest()
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Person (FirstName, LastName) " +
                              "VALUES ('Mike', 'Morain')";

            cmd.ExecuteNonQuery();
            Assert.Pass();
        }

        [Test, Order(2)]
        public void ValidatePersonTest()
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT FirstName, LastName FROM Person";

            var reader = cmd.ExecuteReader();
            var names = new List<string>();
            while (reader.Read())
            {
                names.Add(string.Format("{0} {1}", reader.GetString(0), reader.GetString(1)));
            }

            Assert.Contains("Mike Morain", names);
        }

        [Test,Order(3)]
        public void DeletePersonTest()
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Person WHERE LastName = 'Morain'";

            cmd.ExecuteNonQuery();

            // Validate it's gone
            cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT FirstName, LastName FROM Person";

            var reader = cmd.ExecuteReader();
            var names = new List<string>();
            while (reader.Read())
            {
                names.Add(string.Format("{0} {1}", reader.GetString(0), reader.GetString(1)));
            }

            Assert.That(names, Has.No.Member("Mike Morain"));
        }


    }
}
