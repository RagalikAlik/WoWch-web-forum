using Npgsql.Internal.TypeHandlers;

namespace Wow.Models;

public class Theme
{
    private int _id;
    private string _creator;
    private string? _text;
    private DateTime _releaseDate;
    private int _likes;
    private int _dislikes;
    

    public int Id { get { return this._id; } set { this._id = value; } }
    public string Creator { get { return this._creator; } set { this._creator = value; } }
    public string? Text { get { return this._text; } set { this._text = value; } }
    public DateTime ReleaseDate { get { return this._releaseDate; } set { this._releaseDate = value; } }
    public int Likes { get { return this._likes; } set { this._likes = value; } }
    public int Dislikes{ get { return this._dislikes; } set { this._dislikes = value; } }

    public Theme(string creator, string text, DateTime releaseDate)
    {
        Creator = creator;
        Text = text;
        ReleaseDate = releaseDate;
    }
    
    public Theme(int id, string creator, string text, DateTime releaseDate, int likes, int dislikes)
    {
        Id = id;
        Creator = creator;
        Text = text;
        ReleaseDate = releaseDate;
        Likes = likes;
        Dislikes = dislikes;
    }
}