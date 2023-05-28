using Npgsql.Internal.TypeHandlers;

namespace Wow.Models;

public class Theme
{
    private int _id;
    private string _creator;
    private string? _text;
    private string _header;
    private string _cathegory;
    private DateTime _releaseDate;
    private int _likes;
    private int _dislikes;
    private List<Comment>? _comments;
    

    public int Id { get { return this._id; } set { this._id = value; } }
    public string Creator { get { return this._creator; } set { this._creator = value; } }
    public string? Text { get { return this._text; } set { this._text = value; } }
    public string Header { get { return _header; } set { _header = value; } }
    public string Cathegory { get { return _cathegory; } set { _cathegory = value; } }
    public DateTime ReleaseDate { get { return this._releaseDate; } set { this._releaseDate = value; } }
    public int Likes { get { return this._likes; } set { this._likes = value; } }
    public int Dislikes{ get { return this._dislikes; } set { this._dislikes = value; } }
    public List<Comment> Comments { get { return _comments; } set { _comments = value; } }
    
    public Theme(string creator, string text, string cathegory, string header, DateTime releaseDate)
    {
        Creator = creator;
        Text = text;
        Cathegory = cathegory;
        Header = header;
        ReleaseDate = releaseDate;
    }
    
    public Theme(int id, string creator, string text, string header, string cathegory, DateTime releaseDate, int likes, int dislikes, List<Comment> comments)
    {
        Id = id;
        Creator = creator;
        Text = text;
        Cathegory = cathegory;
        Header = header;
        ReleaseDate = releaseDate;
        Likes = likes;
        Dislikes = dislikes;
        Comments = comments;
    }
}