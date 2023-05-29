using Npgsql;
using Wow.Models;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components;

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

    public static List<Theme> GetThemes()
    {
        List<Theme> themes = new List<Theme>();

        var conn = new NpgsqlConnection(link);
        conn.ConnectionString = link;
        conn.Open();
        var cmd = new NpgsqlCommand();
        cmd.CommandText = "SELECT * FROM themes;";
        cmd.Connection = conn;
        using(var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Theme theme = new Theme
                {
                    Id = reader.GetInt32(0),
                    Creator = reader.GetString(2),
                    Text = new MarkupString(reader.GetString(3)),
                    Header = reader.GetString(1),
                    Cathegory = reader.GetString(7),
                    ReleaseDate = reader.GetDateTime(4),
                    Likes = reader.GetInt32(5),
                    Dislikes = reader.GetInt32(6),
                    //Comments = reader.Get
                };
                themes.Add(theme);
            }
            return themes;
        }
        return themes;
    }

    private byte[] CommentsToByte(List<Comment> comments)
    {
        string json = JsonSerializer.Serialize(comments);
        byte[] commentBytes = Encoding.UTF8.GetBytes(json);
        return commentBytes;
    }

    public static Theme GetThemeWithMaxLikes()
    {
        var conn = new NpgsqlConnection(link);
        conn.ConnectionString = link;
        conn.Open();
        var cmd = new NpgsqlCommand();
        cmd.CommandText = "SELECT * FROM themes WHERE likes = (SELECT MAX(likes) FROM themes)";
        cmd.Connection = conn;
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                Theme theme = new Theme
                {
                    Id = reader.GetInt32(0),
                    Creator = reader.GetString(2),
                    Text = new MarkupString(reader.GetString(3)),
                    Header = reader.GetString(1),
                    Cathegory = reader.GetString(7),
                    ReleaseDate = reader.GetDateTime(4),
                    Likes = reader.GetInt32(5),
                    Dislikes = reader.GetInt32(6),
                    //Comments = reader.Get
                };
                return theme;
            }
        }
        return new Theme();
    }


    public static Theme GetThemeWithMaxDislikes()
    {
        var conn = new NpgsqlConnection(link);
        conn.ConnectionString = link;
        conn.Open();
        var cmd = new NpgsqlCommand();
        cmd.CommandText = "SELECT * FROM themes WHERE dislikes = (SELECT MAX(dislikes) FROM themes)";
        cmd.Connection = conn;
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                Theme theme = new Theme
                {
                    Id = reader.GetInt32(0),
                    Creator = reader.GetString(2),
                    Text = new MarkupString(reader.GetString(3)),
                    Header = reader.GetString(1),
                    Cathegory = reader.GetString(7),
                    ReleaseDate = reader.GetDateTime(4),
                    Likes = reader.GetInt32(5),
                    Dislikes = reader.GetInt32(6),
                    //Comments = reader.Get
                };
                return theme;
            }
        }
        return new Theme();
    }

}