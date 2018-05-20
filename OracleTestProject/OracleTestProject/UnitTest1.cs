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
        [SetUp, Order(1)]
        public void SetupOracleConnection()
        {
            _connection = new OracleConnection();
            // var connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            var connectionString = "User ID=oraadmin;password=PASSWORD;Data Source=DNSNAME:1521/ORADB01;Pooling=false;";
            _connection.ConnectionString = connectionString;
            _connection.Open();
        }

        [SetUp, Order(2)]
        public void CreateSchema()
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = @"CREATE TABLE Person
                                (
                                    FirstName VARCHAR(30),
                                    LastName VARCHAR(30)
                                );";

            cmd.ExecuteNonQuery();
            Assert.Pass();
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
                names.Add($"{reader.GetString(0)} {reader.GetString(1)}");
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
                names.Add($"{reader.GetString(0)} {reader.GetString(1)}");
            }

            Assert.That(names, Has.No.Member("Mike Morain"));
        }

        [TearDown]
        public void RemoveTable()
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = @"DROP TABLE Person;";

            cmd.ExecuteNonQuery();
            Assert.Pass();
        }
    }
}
