using Npgsql;
using NpgsqlTypes;
using System.Linq;

namespace Wow.Models
{
    public class Avatar
    {
        int id;
        string fileName;
        byte[] data;

        public int Id { get{ return this.id; } set { this.id = value; } }
        public string FileName { get { return this.fileName; } set { this.fileName = value; } }
        public byte[] ImageData { get { return this.data; } set { this.data = value; } }


        private static readonly string? Link = "Server=localhost;Port=5432;Username=user;Password=password;Database=postgres";

        public Avatar(int id, string fileName, byte[] imageData) 
        {
            Id = id;
            FileName = fileName;
            ImageData = imageData;
        }

        public Avatar() { }

        public void SavePhoto()
        {
            using (var connection = new NpgsqlConnection(Link))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {

                    command.Connection = connection;
                    command.CommandText = $"insert into avatars (id, filename, imagedata) VALUES (@id, @filename, @data);";
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@filename", FileName);
                    command.Parameters.AddWithValue("@data", NpgsqlDbType.Bytea, ImageData.Length, ImageData);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
