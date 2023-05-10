using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Internal;
using Wow.Controllers;
using Wow.Models;


namespace Wow.Controllers
{
    public class AccountController
    {
        public static User user;


        static IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

        static readonly IConfiguration Configuration = builder.Build();

        private static readonly string? Link = Configuration.GetConnectionString("DefaultConnection");

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
        public static async Task<User> Authorization(string login, string password)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Link);

            conn.ConnectionString = Link;
            conn.Open();
            string commandText = $"SELECT * FROM users where login = '{login}'";
            await using (NpgsqlCommand cmd = new NpgsqlCommand(commandText, conn))
            {
                cmd.Parameters.AddWithValue("login", login);
                await using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        User user = ReadUser(reader);
                        return user;
                    }
                }
            }

            return null;
        }
        
        [HttpGet]
        private static User ReadUser(NpgsqlDataReader reader)
        {
            int? id = reader["id"] as int?;
            string? login = reader["login"] as string;
            string? email = reader["email"] as string;
            string? password = reader["password"] as string;
            DateTime registrationDate = reader["registrationdate"] as DateTime? ?? default;
            string? role = reader["role"] as string;
            
            User user = new User
            {
                Id = id.Value,
                Login = login,
                Password = password,
                Email = email,
                RegistrationDate = registrationDate,
                Role = role
            };
            return user;
        }
    }
}
