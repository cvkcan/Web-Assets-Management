using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace Web_Assets_Management.Models
{
    public static class Databases
    {
        static IConfiguration _configuration;

        static Databases()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }
        public static SqlConnection connect()
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultDB"));
            if (conn.State!=System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

        public static SqlCommand command(string query)
        {
            SqlCommand command = new SqlCommand(query,connect());
            return command;
        }
    }
}
