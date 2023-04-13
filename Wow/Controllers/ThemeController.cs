using Npgsql;

namespace Wow.Controllers;

public class ThemeController
{
    private const string link = "Server=localhost;Port=5432;Username=user;Password=password;Database=postgres";

    public static async Task WriteTheme()
    {
        NpgsqlConnection conn = new NpgsqlConnection(link);
        conn.ConnectionString = link;
        conn.Open();
        NpgsqlCommand cmd = new NpgsqlCommand();
        cmd.CommandText = "INSERT INTO themes(HEADER, RELEASEDATE, CREATOR, TEXT, likes, dislikes) VALUES ('я люблю котлеты', '11/03/2023', 'xtc', 'я люблю котлеты, это моя любимая еда', 0, 0);";
        cmd.Connection = conn;
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}