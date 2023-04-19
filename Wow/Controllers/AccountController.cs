using Microsoft.AspNetCore.Mvc;
using Npgsql;




namespace Wow.Controllers
{
    public class AccountController
    {
        static IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
        static readonly IConfiguration Configuration = builder.Build();
        
        private static readonly string? Link =Configuration.GetConnectionString("DefaultConnection");
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public static void Register(string login, string password, string email)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Link);
            conn.ConnectionString = Link;

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();

            cmd.CommandText = $"INSERT INTO users(LOGIN, PASSWORD, EMAIL, REGISTRATIONDATE) VALUES('{login}', '{password}', '{email}', '{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}');";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
