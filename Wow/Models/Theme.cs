using Npgsql.Internal.TypeHandlers;

namespace Wow.Models;

public class Theme
{
    private int _id;
    private string _creator;
    private string _text;
    private DateTime _releaseDate;
    private int _likes;
    private int _dislikes;
    private UuidHandler? _image;

    public Theme(int id, string creator, string text, DateTime releaseDate, int likes, int dislikes, UuidHandler image)
    {
        _id = id;
        _creator = creator;
        _text = text;
        _releaseDate = releaseDate;
        _likes = likes;
        _dislikes = dislikes;
        _image = image;
    }
    
    public Theme(int id, string creator, string text, DateTime releaseDate, int likes, int dislikes)
    {
        _id = id;
        _creator = creator;
        _text = text;
        _releaseDate = releaseDate;
        _likes = likes;
        _dislikes = dislikes;
    }
    
    
}