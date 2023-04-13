using Npgsql;


namespace Wow.Controllers
{
    public class AccountController
    {
        private const string link = "Server=localhost;Port=5432;Username=user;Password=password;Database=postgres";

        public static void Register(string login, string password, string email)
        {
            NpgsqlConnection conn = new NpgsqlConnection(link);
            conn.ConnectionString = link;

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();

            //cmd.CommandText = "INSERT INTO themes(HEADER, RELEASEDATE, CREATOR, TEXT) VALUES ('я люблю котлеты', '11/03/2023', 'xtc', 'я люблю котлеты, это моя любимая еда');";

            cmd.CommandText = $"INSERT INTO users(LOGIN, PASSWORD, EMAIL, REGISTRATIONDATE) VALUES('{login}', '{password}', '{email}', '{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}');";
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }


    }
}
