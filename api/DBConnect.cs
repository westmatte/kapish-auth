using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace api
{
    public static class DBConnect
    {
        public static async Task<SqlConnection> GetConnectionAsync(string connectionString)
        {
            ValidateConnectionString(connectionString);

            var resource = "https://database.windows.net/";
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var accessToken = await azureServiceTokenProvider.GetAccessTokenAsync(resource);

            var connection = new SqlConnection()
            {
                ConnectionString = connectionString,
                AccessToken = accessToken
            };

            connection.Open();
            return connection;
        }

        private static void ValidateConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException($"The provided SQL connection string is invalid. Value: {connectionString}");
            }
        }
    }
}
