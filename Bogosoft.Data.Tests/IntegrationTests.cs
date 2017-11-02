using NUnit.Framework;
using Should;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Bogosoft.Data.Tests
{
    [TestFixture, Category("Integration")]
    public class IntegrationTests
    {
        static class Table
        {
            internal const string Hero = "[dbo].[Bogosoft.Testing.Hero]";
        }

        static string Dsn => ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;

        [OneTimeSetUp]
        public async Task SetupAsync()
        {
            var builder = new SqlConnectionStringBuilder(Dsn);

            var database = builder.InitialCatalog;

            builder.InitialCatalog = "master";

            using (var connection = new SqlConnection(Dsn))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"
CREATE TABLE {Table.Hero}(
    hero_name NVARCHAR(128) NOT NULL,
    hero_height INT NOT NULL,
    most_notable_feat NVARCHAR(MAX) NULL
    )";
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        [OneTimeTearDown]
        public async Task TeardownAsync()
        {
            using (var connection = new SqlConnection(Dsn))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"DROP TABLE {Table.Hero}";

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        async Task<int> GetHeroCount(SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT COUNT(*) FROM {Table.Hero}";

                return (int)await command.ExecuteScalarAsync();
            }
        }

        [TestCase]
        public async Task CanLoadEntitiesFromCsvFileIntoSqlServerTable()
        {
            using (var connection = new SqlConnection(Dsn))
            {
                await connection.OpenAsync();

                (await GetHeroCount(connection)).ShouldEqual(0);

                var filepath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Heroes.csv");

                using (var source = new StreamReader(filepath))
                {
                    var csv = new StandardFlatFileReader(source);

                    using (var reader = RowSequenceDataReader.WithFieldNamesOnFirstLine(csv))
                    using (var loader = new SqlBulkCopy(connection))
                    {
                        loader.DestinationTableName = Table.Hero;

                        await loader.WriteToServerAsync(reader);
                    }
                }

                (await GetHeroCount(connection)).ShouldBeGreaterThan(0);
            }
        }
    }
}