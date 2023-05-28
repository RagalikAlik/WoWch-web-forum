using Npgsql;
using Wow.Models;
using System.Text.Json;
using System.Text;

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
        cmd.CommandText = $"INSERT INTO themes(RELEASEDATE, CREATOR, TEXT, HEADER, CATHEGORY) VALUES (@releaseDate, @creator, @text, @header, @cathegory);";
        cmd.Parameters.AddWithValue("@releaseDate", theme.ReleaseDate);
        cmd.Parameters.AddWithValue("@creator", theme.Creator);
        cmd.Parameters.AddWithValue("@text", theme.Text);
        cmd.Parameters.AddWithValue("@header", theme.Header);
        cmd.Parameters.AddWithValue("@cathegory", theme.Cathegory);
        cmd.Connection = conn;
        cmd.ExecuteNonQuery();
        await conn.CloseAsync();
    }

    //public static async Theme GetThemes()
    //{
        
    //}

    private byte[] CommentsToByte(List<Comment> comments)
    {
        string json = JsonSerializer.Serialize(comments);
        byte[] commentBytes = Encoding.UTF8.GetBytes(json);
        return commentBytes;
    }
}