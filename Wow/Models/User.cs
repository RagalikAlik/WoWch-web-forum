﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Npgsql;

namespace Wow.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public byte[] ProfilePicture { get; set; }

        public User(int id, string login, string password, string email, DateTime registrationDate, string role, string status)
        {
            Id = id;
            Login = login;
            Password = password;
            Email = email;
            RegistrationDate = registrationDate;
            Role = role;
            Status = status;
        }

        public User()
        {
            
        }

        public void WriteUserToDb(string login, string password, string email, DateTime date)
        {
            const string link1 = "Server=localhost;Port=5432;Username=user;Password=password;Database=postgres";

            NpgsqlConnection conn = new NpgsqlConnection(link1);
            conn.ConnectionString = link1;

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.CommandText = $"INSERT INTO users(LOGIN, PASSWORD, EMAIL, REGISTRATIONDATE) " +
                              $"VALUES ({login}, {password}, {email}, {date});";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        
        
    }
}
