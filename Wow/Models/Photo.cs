using Npgsql;

namespace Wow.Models
{
    public class Photo
    {
        int id;
        int themeId;
        byte[] data;

        public int Id { get { return this.id; } set { this.id = value; } }
        public int ThemeId { get { return this.themeId; } set { this.themeId = value; } }
        public byte[] ImageData { get { return this.data; } set { this.data = value; } }


        private static readonly string? Link = "Server=localhost;Port=5432;Username=user;Password=password;Database=postgres";

        public Photo(int id, byte[] imageData)
        {
            Id = id;
            ImageData = imageData;
        }

        public Photo(int id, int themeId, byte[] imageData)
        {
            Id = id;
            ThemeId = themeId;
            ImageData = imageData;
        }

        public Photo() { }

        public void SavePhoto()
        {
            using (var connection = new NpgsqlConnection(Link))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    //if ()
                    //{
                    //    command.CommandText = "UPDATE themepictures SET imagedata = @data WHERE id = @Id;";
                    //}
                    //else
                    //{
                        command.CommandText = "insert into themepictures (themeid, photodata) VALUES (@Id, @data);";
                    //}


                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@data", NpgsqlTypes.NpgsqlDbType.Bytea, ImageData.Length).Value = ImageData;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
