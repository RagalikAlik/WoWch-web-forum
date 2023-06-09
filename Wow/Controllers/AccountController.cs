using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Internal;
using Wow.Controllers;
using Wow.Models;
using System.IO;
using System.Xml.Linq;


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
            string status = reader["status"] as string;


            User user = new User
            {
                Id = id.Value,
                Login = login,
                Password = password,
                Email = email,
                RegistrationDate = registrationDate,
                Role = role,
                Status = status
            };
            return user;
        }

        public static void UpdateUserAvatar(string login, byte[] byteImage)
        {
            var image = System.Text.Encoding.Default.GetString(byteImage);

            using (NpgsqlConnection conn = new NpgsqlConnection(Link))
            {
                conn.ConnectionString = Link;
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = $"UPDATE users SET avatar = {image} WHERE login = {login}";
                cmd.Connection = conn;
                cmd.ExecuteScalar();
            }
        }

        public static byte[] GetAvatarFromDb()
        {
            byte[] image = null;

            using(NpgsqlConnection connection= new NpgsqlConnection(Link))
            {
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"select imagedata from avatars where id = {user.Id}";
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    image = (byte[])dr[0];
                }

            }

            return image;
        }

        public static void BanAccount(string userLogin)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(Link))
            {
                conn.Open();
                var cmd = new NpgsqlCommand();
                cmd.CommandText = $"update users set status = 'banned' where login = @login";
                cmd.Parameters.AddWithValue("@login", userLogin);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
