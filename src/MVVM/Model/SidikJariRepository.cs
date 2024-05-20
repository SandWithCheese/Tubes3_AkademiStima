using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;

namespace src.MVVM.Model
{
    public class SidikJariRepository : ISidikJariRepository
    {
        private readonly string _connectionString;

        public SidikJariRepository(string databasePath)
        {
            _connectionString = $"Data Source={databasePath}";
            if (!File.Exists(databasePath))
            {
                CreateTable();
            }
        }

        public void CreateTable()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "CREATE TABLE sidik_jari (berkas_citra TEXT, nama VARCHAR(100) DEFAULT NULL);";
                command.ExecuteNonQuery();
            }
        }

        public void Add(SidikJari entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO sidik_jari (berkas_citra, nama) VALUES (@berkas_citra, @nama);";
                command.Parameters.AddWithValue("@berkas_citra", entity.BerkasCitra);
                command.Parameters.AddWithValue("@nama", entity.Nama);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SidikJari> GetAll()
        {
            var result = new List<SidikJari>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM sidik_jari;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SidikJari
                        {
                            BerkasCitra = reader.GetString(0),
                            Nama = reader.GetString(1)
                        });
                    }
                }
            }
            return result;
        }

        public void Update(SidikJari entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE sidik_jari SET nama = @nama WHERE berkas_citra = @berkas_citra;";
                command.Parameters.AddWithValue("@berkas_citra", entity.BerkasCitra);
                command.Parameters.AddWithValue("@nama", entity.Nama);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(SidikJari entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM sidik_jari WHERE berkas_citra = @berkas_citra and nama = @nama;";
                command.Parameters.AddWithValue("@berkas_citra", entity.BerkasCitra);
                command.Parameters.AddWithValue("@nama", entity.Nama);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SidikJari> GetByNama(string nama)
        {
            var result = new List<SidikJari>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM sidik_jari WHERE nama = @nama;";
                command.Parameters.AddWithValue("@nama", nama);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SidikJari
                        {
                            BerkasCitra = reader.GetString(0),
                            Nama = reader.GetString(1)
                        });
                    }
                }
            }
            return result;
        }

        public IEnumerable<SidikJari> GetByBerkasCitra(string berkasCitra)
        {
            var result = new List<SidikJari>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM sidik_jari WHERE berkas_citra = @berkas_citra;";
                command.Parameters.AddWithValue("@berkas_citra", berkasCitra);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SidikJari
                        {
                            BerkasCitra = reader.GetString(0),
                            Nama = reader.GetString(1)
                        });
                    }
                }
            }
            return result;
        }

        public IEnumerable<SidikJari> GetByNamaAndBerkasCitra(string nama, string berkasCitra)
        {
            var result = new List<SidikJari>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM sidik_jari WHERE nama = @nama AND berkas_citra = @berkas_citra;";
                command.Parameters.AddWithValue("@nama", nama);
                command.Parameters.AddWithValue("@berkas_citra", berkasCitra);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SidikJari
                        {
                            BerkasCitra = reader.GetString(0),
                            Nama = reader.GetString(1)
                        });
                    }
                }
            }
            return result;
        }

        public void DeleteByNama(string nama)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM sidik_jari WHERE nama = @nama;";
                command.Parameters.AddWithValue("@nama", nama);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteByBerkasCitra(string berkasCitra)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM sidik_jari WHERE berkas_citra = @berkas_citra;";
                command.Parameters.AddWithValue("@berkas_citra", berkasCitra);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteByNamaAndBerkasCitra(string nama, string berkasCitra)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM sidik_jari WHERE nama = @nama AND berkas_citra = @berkas_citra;";
                command.Parameters.AddWithValue("@nama", nama);
                command.Parameters.AddWithValue("@berkas_citra", berkasCitra);
                command.ExecuteNonQuery();
            }
        }
    }
}
