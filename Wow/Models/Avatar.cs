using Npgsql;
using NpgsqlTypes;
using System.Linq;
using Wow.Controllers;

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
                    if (IfImageExists())
                    {
                        command.CommandText = "UPDATE avatars SET imagedata = @data WHERE id = @Id;";
                    }
                    else
                    {
                        command.CommandText = "insert into avatars (id, filename, imagedata) VALUES (@Id, @filename, @data);";
                    }


                    //command.CommandText = @$"
                    //        DO $$
                    //            DECLARE
                    //                v_id INT := @Id;
                    //                v_data bytea := @data;
                    //            BEGIN
                    //                IF EXISTS(SELECT * FROM avatars WHERE id = v_id)
                    //                THEN 
                    //                    UPDATE avatars SET imagedata = v_data WHERE id = v_id;
                    //                ELSE
                    //                    insert into avatars (id, filename, imagedata) VALUES (v_id, @filename, v_data);
                    //                END IF;
                    //        END$$;
                    //        ";
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@filename", FileName);
                    command.Parameters.AddWithValue("@data", NpgsqlTypes.NpgsqlDbType.Bytea, ImageData.Length).Value = ImageData;

                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool IfImageExists() 
        {
            using (var conn = new NpgsqlConnection(Link))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT EXISTS(SELECT * FROM avatars WHERE id = @id)";
                    cmd.Parameters.AddWithValue("@id", AccountController.user.Id);

                    bool recordExists = (bool)cmd.ExecuteScalar();

                    return recordExists;
                }
            }
            return false;
        }
    }
}
