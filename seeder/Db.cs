using System.Data.SQLite;
using System.Text;
using seeder;

public class Db
{
    // Static variable to store connection
    public static SQLiteConnection? Conn = null;
    public static SQLiteCommand? Cmd = null;

    private static byte[]? key;
    private static byte[]? iv;

    // Static method to initialize db
    public static void Init(string dbPath, string schemaPath)
    {
        // Connect to db
        Connect(dbPath);
        Migrate(schemaPath);

        // Generate key and iv
        key = AES.GenerateRandomBytes(AES.BlockSize);
        iv = AES.GenerateRandomBytes(AES.BlockSize);

        // Write key and iv to .env file
        WriteToEnvFile("AES_KEY", Convert.ToBase64String(key));
        WriteToEnvFile("AES_IV", Convert.ToBase64String(iv));

        // Seed data
        Seed();
    }

    private static void WriteToEnvFile(string key, string value)
    {
        string envFilePath = ".env";
        var lines = new List<string>();

        if (File.Exists(envFilePath))
        {
            lines = File.ReadAllLines(envFilePath).ToList();
        }

        bool keyExists = false;
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith($"{key}="))
            {
                lines[i] = $"{key}={value}";
                keyExists = true;
                break;
            }
        }

        if (!keyExists)
        {
            lines.Add($"{key}={value}");
        }

        File.WriteAllLines(envFilePath, lines);
    }

    // Static method to initialize connection
    public static void Connect(string dbPath)
    {
        try
        {
            Conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            Conn.Open();
            Cmd = Conn.CreateCommand();
            Cmd.CommandText = "PRAGMA foreign_keys = ON";
            Cmd.ExecuteNonQuery();
        }
        catch (SQLiteException e)
        {
            Console.WriteLine("Error connecting to db");
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Static method to migrate db schema
    public static void Migrate(string schemaPath)
    {
        try
        {
            var schema = File.ReadAllText(schemaPath);
            if (Cmd != null)
            {
                Cmd.CommandText = schema;
                Cmd.ExecuteNonQuery();
            }
        }
        catch (SQLiteException e)
        {
            Console.WriteLine("Error migrating schema");
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Seeding
    public static void Seed()
    {
        // Seed data
        var sidikJariTuples = Data.GenerateSidikJariTuples();
        var biodataTuples = Data.GenerateBiodataTuples(sidikJariTuples.Select(t => (t.Item1, t.Item2)).ToList());

        try
        {
            // Begin transaction
            if (Cmd != null)
            {
                Cmd.CommandText = "BEGIN TRANSACTION";
                Cmd.ExecuteNonQuery();

                // Remove all data
                Cmd.CommandText = "DELETE FROM biodata";
                Cmd.ExecuteNonQuery();
                Cmd.CommandText = "DELETE FROM sidik_jari";
                Cmd.ExecuteNonQuery();
            }

            // Insert data
            foreach (var tuple in biodataTuples)
            {
                if (Cmd != null)
                {
                    Cmd.CommandText = "INSERT INTO biodata (NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) VALUES (@NIK, @nama, @tempat_lahir, @tanggal_lahir, @jenis_kelamin, @golongan_darah, @alamat, @agama, @status_perkawinan, @pekerjaan, @kewarganegaraan)";
                    Cmd.Parameters.AddWithValue("@NIK", tuple.Item1);
                    Cmd.Parameters.AddWithValue("@nama", tuple.Item2);
                    Cmd.Parameters.AddWithValue("@tempat_lahir", Convert.ToBase64String(AES.Encrypt(AES.Pad(Encoding.UTF8.GetBytes(tuple.Item3), AES.BlockSize), key!, iv!)));
                    Cmd.Parameters.AddWithValue("@tanggal_lahir", tuple.Item4);
                    Cmd.Parameters.AddWithValue("@jenis_kelamin", tuple.Item5);
                    Cmd.Parameters.AddWithValue("@golongan_darah", Convert.ToBase64String(AES.Encrypt(AES.Pad(Encoding.UTF8.GetBytes(tuple.Item6), AES.BlockSize), key!, iv!)));
                    Cmd.Parameters.AddWithValue("@alamat", Convert.ToBase64String(AES.Encrypt(AES.Pad(Encoding.UTF8.GetBytes(tuple.Item7), AES.BlockSize), key!, iv!)));
                    Cmd.Parameters.AddWithValue("@agama", Convert.ToBase64String(AES.Encrypt(AES.Pad(Encoding.UTF8.GetBytes(tuple.Item8), AES.BlockSize), key!, iv!)));
                    Cmd.Parameters.AddWithValue("@status_perkawinan", tuple.Item9);
                    Cmd.Parameters.AddWithValue("@pekerjaan", Convert.ToBase64String(AES.Encrypt(AES.Pad(Encoding.UTF8.GetBytes(tuple.Item10), AES.BlockSize), key!, iv!)));
                    Cmd.Parameters.AddWithValue("@kewarganegaraan", Convert.ToBase64String(AES.Encrypt(AES.Pad(Encoding.UTF8.GetBytes(tuple.Item11), AES.BlockSize), key!, iv!)));
                    Cmd.ExecuteNonQuery();
                }
            }

            foreach (var tuple in sidikJariTuples)
            {
                if (Cmd != null)
                {
                    Cmd.CommandText = "INSERT INTO sidik_jari (berkas_citra, nama) VALUES (@berkas_citra, @nama)";
                    Cmd.Parameters.AddWithValue("@berkas_citra", tuple.Item1);
                    Cmd.Parameters.AddWithValue("@nama", tuple.Item2);
                    Cmd.ExecuteNonQuery();
                }
            }

            // Commit transaction
            if (Cmd != null)
            {
                Cmd.CommandText = "COMMIT";
                Cmd.ExecuteNonQuery();
            }
        }
        catch (SQLiteException e)
        {
            // If error, rollback connnection
            if (Cmd != null)
            {
                Cmd.CommandText = "ROLLBACK";
                Cmd.ExecuteNonQuery();
            }
            Console.WriteLine("Error seeding data");
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Close connection & reset connection
    public static void Close()
    {
        // Close connection
        Conn?.Close();
        Conn = null;
        Cmd = null;
    }
}
