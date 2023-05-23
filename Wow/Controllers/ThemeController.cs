using Npgsql;
using Wow.Models;

namespace Wow.Controllers;


public class ThemeController
{
    private const string link = "Server=localhost;Port=5432;Username=user;Password=password;Database=postgres";

    public static async Task WriteTheme(Theme theme)
    {
        var conn = new NpgsqlConnection(link);
        conn.ConnectionString = link;
        conn.Open();
        var cmd = new NpgsqlCommand();
        cmd.CommandText = $"INSERT INTO themes(RELEASEDATE, CREATOR, TEXT) VALUES (@releaseDate, @creator, @text);";
        cmd.Parameters.AddWithValue("@releaseDate", theme.ReleaseDate);
        cmd.Parameters.AddWithValue("@creator", theme.Creator);
        cmd.Parameters.AddWithValue("@text", theme.Text);
        cmd.Connection = conn;
        cmd.ExecuteNonQuery();
        await conn.CloseAsync();
    }
}