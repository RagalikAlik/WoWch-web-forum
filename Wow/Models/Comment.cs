using Newtonsoft.Json;

namespace Wow.Models
{
    public class Comment
    {
        private string _author;
        private string _text;
        private DateTime _timestamp;
        private int _likes;
        private int _dislikes;
        
        public string Author { get { return _author; } set { _author = value; } }
        public string Text { get { return _text; } set { _text = value; } }
        public DateTime Timestamp { get {return _timestamp;} set {_timestamp = value;}}
        public int Likes {get {return _likes;} set {_likes = value;}}
        public int Dislikes { get {return _dislikes;} set {_dislikes = value;}}

        public Comment(string author, string text)
        {
            Author = author;
            Text = text;
            Timestamp = DateTime.Now;
            Likes = 0;
            Dislikes = 0;
        }

        [JsonConstructor]
        public Comment(string author, string text, DateTime timestamp, int likes, int dislikes)
        {
            Author = author;
            Text = text;
            Timestamp = timestamp;
            Likes = likes;
            Dislikes = dislikes;
        }
    }
}
