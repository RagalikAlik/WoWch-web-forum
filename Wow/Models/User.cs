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

        public void GetUserFromDb()
        {

        }

        public void WriteUserToDb(string login, string password, string email, DateTime date)
        {
            const string link1 = "Server=localhost;Port=5432;Username=user;Password=password;Database=postgres";

            NpgsqlConnection conn = new NpgsqlConnection(link1);
            conn.ConnectionString = link1;

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.CommandText = $"INSERT INTO users(LOGIN, PASSWORD, EMAIL, REGISTRATIONDATE) VALUES ({login}, {password}, {email}, {date});";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
