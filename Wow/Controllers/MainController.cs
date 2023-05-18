using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Wow.Controllers
{
    public class MainController
    {
        static IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

        static readonly IConfiguration Configuration = builder.Build();

        private static readonly string? Link = Configuration.GetConnectionString("DefaultConnection");

        [HttpPost]
        public static string GetAmountOfUsers()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Link))
            {
                conn.ConnectionString = Link;

                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();

                cmd.CommandText = $"SELECT count(*) FROM users";
                cmd.Connection = conn;
                return cmd.ExecuteScalar().ToString();
            }
        }

        [HttpPost]
        public static string GetAmountOfArticles()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Link))
            {
                conn.ConnectionString = Link;

                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();

                cmd.CommandText = $"SELECT count(*) FROM themes";
                cmd.Connection = conn;
                return cmd.ExecuteScalar().ToString();
            }
        }

        //public static byte[] ConvertPicToBytea()
        //{
        //    byte? bytePic=null;



        //    return bytePic;
        //}
    }
}
