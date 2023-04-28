using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Wow.Models;


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

            cmd.CommandText = $"INSERT INTO users(LOGIN, PASSWORD, EMAIL, REGISTRATIONDATE) " +
                              $"VALUES('{login}', '{password}', '{email}', '{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}');";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        [HttpGet]
        public static User Authorization(string login, string password)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Link);
            try
            {
                conn.ConnectionString = Link;
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"SELECT id FROM users where login = '{login}'";
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.CommandText = $"SELECT login FROM users where login = '{login}'";
                string log = Convert.ToString(cmd.ExecuteScalar());
                cmd.CommandText = $"SELECT password FROM users where login = '{login}'";
                string pass = Convert.ToString(cmd.ExecuteScalar());
                cmd.CommandText = $"SELECT email FROM users where login = '{login}'";
                string mail = Convert.ToString(cmd.ExecuteScalar());
                cmd.CommandText = $"SELECT registrationdate FROM users where login = '{login}'";
                DateTime regDate = Convert.ToDateTime(cmd.ExecuteScalar());

                if (password == pass)
                {
                    User user = new User(id, log,pass,mail,regDate);
                    conn.Close();
                    return user;
                }
                else
                {
                    conn.Close();
                    throw new Exception("Invalid password");
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }

            return new User();
        }

    }
}
