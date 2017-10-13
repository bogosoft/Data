using System.Configuration;

namespace Bogosoft.Data.Tests
{
    static class Config
    {
        internal static class AppSettings
        {
            internal static string TestDatabase => ConfigurationManager.AppSettings["TestDatabase"];
        }

        internal static class ConnectionStrings
        {
            internal static string TestConnectionString => ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;
        }
    }
}