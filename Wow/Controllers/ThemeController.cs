using Npgsql;
using Wow.Models;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

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
        cmd.Parameters.AddWithValue("@text", theme.Text.ToString());
        cmd.Parameters.AddWithValue("@header", theme.Header);
        cmd.Parameters.AddWithValue("@cathegory", theme.Cathegory);
        cmd.Connection = conn;
        cmd.ExecuteNonQuery();
        await conn.CloseAsync();
    }

    public static List<Theme> GetThemes()
    {
        List<Theme> themes = new List<Theme>();
        try
        {
            var conn = new NpgsqlConnection(link);
            conn.ConnectionString = link;
            conn.Open();
            var cmd = new NpgsqlCommand();
            cmd.CommandText = "SELECT * FROM themes;";
            cmd.Connection = conn;
            using (var reader = cmd.ExecuteReader())
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
                        Comments = reader.IsDBNull(8) ? null : JsonStringToComments(reader.GetString(8)),
                        Views = reader.GetInt32(9)
                    };
                    themes.Add(theme);
                }
                return themes;
            }
        } catch (InvalidCastException e)
        {

        }
        return themes;
    }

    public static string CommentsToJsonString(List<Comment> comments)
    {
        string json = JsonConvert.SerializeObject(comments);
        return json;
    }

    public static List<Comment> JsonStringToComments(string json)
    {
        List<Comment> comments = JsonConvert.DeserializeObject<List<Comment>>(json);
        return comments;
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
                    Comments = JsonStringToComments(reader.GetString(8)),
                    Views = reader.GetInt32(9)
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
                    Comments = JsonStringToComments(reader.GetString(8)),
                    Views = reader.GetInt32(9)
                };
                return theme;
            }
        }
        return new Theme();
    }

}