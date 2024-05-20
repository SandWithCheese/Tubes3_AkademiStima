using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;

namespace src.MVVM.Model
{
    public class BiodataRepository : IBiodataRepository
    {
        private readonly string _connectionString;

        public BiodataRepository(string databasePath)
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
                command.CommandText = "CREATE TABLE biodata (NIK VARCHAR(16), nama VARCHAR(100) DEFAULT NULL, tempat_lahir VARCHAR(50) DEFAULT NULL, tanggal_lahir DATE DEFAULT NULL, jenis_kelamin VARCHAR(10) CHECK(jenis_kelamin IN ('Laki-Laki', 'Perempuan')) DEFAULT NULL, golongan_darah VARCHAR(5) DEFAULT NULL, alamat VARCHAR(255) DEFAULT NULL, agama VARCHAR(50) DEFAULT NULL, status_perkawinan VARCHAR(20) CHECK(status_perkawinan IN ('Belum Menikah', 'Menikah', 'Cerai')) DEFAULT NULL, pekerjaan VARCHAR(100) DEFAULT NULL, kewarganegaraan VARCHAR(50) DEFAULT NULL);";
                command.ExecuteNonQuery();
            }
        }

        public void Add(Biodata entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO biodata (nik, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan) VALUES (@nik, @nama, @tempat_lahir, @tanggal_lahir, @jenis_kelamin, @golongan_darah, @alamat, @agama, @status_perkawinan, @pekerjaan, @kewarganegaraan);";
                command.Parameters.AddWithValue("@nik", entity.Nik);
                command.Parameters.AddWithValue("@nama", entity.Nama);
                command.Parameters.AddWithValue("@tempat_lahir", entity.TempatLahir);
                command.Parameters.AddWithValue("@tanggal_lahir", entity.TanggalLahir);
                command.Parameters.AddWithValue("@jenis_kelamin", entity.JenisKelamin);
                command.Parameters.AddWithValue("@golongan_darah", entity.GolonganDarah);
                command.Parameters.AddWithValue("@alamat", entity.Alamat);
                command.Parameters.AddWithValue("@agama", entity.Agama);
                command.Parameters.AddWithValue("@status_perkawinan", entity.StatusPerkawinan);
                command.Parameters.AddWithValue("@pekerjaan", entity.Pekerjaan);
                command.Parameters.AddWithValue("@kewarganegaraan", entity.Kewarganegaraan);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Biodata> GetAll()
        {
            var result = new List<Biodata>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM biodata;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Biodata
                        {
                            Nik = reader.GetString(0),
                            Nama = reader.GetString(1),
                            TempatLahir = reader.GetString(2),
                            TanggalLahir = DateOnly.FromDateTime(reader.GetDateTime(3)),
                            JenisKelamin = reader.GetString(4),
                            GolonganDarah = reader.GetString(5),
                            Alamat = reader.GetString(6),
                            Agama = reader.GetString(7),
                            StatusPerkawinan = reader.GetString(8),
                            Pekerjaan = reader.GetString(9),
                            Kewarganegaraan = reader.GetString(10)
                        });
                    }
                }
            }
            return result;
        }

        public void Update(Biodata entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE biodata SET nama = @nama, tempat_lahir = @tempat_lahir, tanggal_lahir = @tanggal_lahir, jenis_kelamin = @jenis_kelamin, golongan_darah = @golongan_darah, alamat = @alamat, agama = @agama, status_perkawinan = @status_perkawinan, pekerjaan = @pekerjaan, kewarganegaraan = @kewarganegaraan WHERE nik = @nik;";
                command.Parameters.AddWithValue("@nik", entity.Nik);
                command.Parameters.AddWithValue("@nama", entity.Nama);
                command.Parameters.AddWithValue("@tempat_lahir", entity.TempatLahir);
                command.Parameters.AddWithValue("@tanggal_lahir", entity.TanggalLahir);
                command.Parameters.AddWithValue("@jenis_kelamin", entity.JenisKelamin);
                command.Parameters.AddWithValue("@golongan_darah", entity.GolonganDarah);
                command.Parameters.AddWithValue("@alamat", entity.Alamat);
                command.Parameters.AddWithValue("@agama", entity.Agama);
                command.Parameters.AddWithValue("@status_perkawinan", entity.StatusPerkawinan);
                command.Parameters.AddWithValue("@pekerjaan", entity.Pekerjaan);
                command.Parameters.AddWithValue("@kewarganegaraan", entity.Kewarganegaraan);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Biodata entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM biodata WHERE nik = @nik;";
                command.Parameters.AddWithValue("@nik", entity.Nik);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Biodata> GetByNama(string nama)
        {
            var result = new List<Biodata>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM biodata WHERE nama = @nama;";
                command.Parameters.AddWithValue("@nama", nama);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Biodata
                        {
                            Nik = reader.GetString(0),
                            Nama = reader.GetString(1),
                            TempatLahir = reader.GetString(2),
                            TanggalLahir = DateOnly.FromDateTime(reader.GetDateTime(3)),
                            JenisKelamin = reader.GetString(4),
                            GolonganDarah = reader.GetString(5),
                            Alamat = reader.GetString(6),
                            Agama = reader.GetString(7),
                            StatusPerkawinan = reader.GetString(8),
                            Pekerjaan = reader.GetString(9),
                            Kewarganegaraan = reader.GetString(10)
                        });
                    }
                }
            }
            return result;
        }

        public IEnumerable<Biodata> GetByNik(string nik)
        {
            var result = new List<Biodata>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM biodata WHERE nik = @nik;";
                command.Parameters.AddWithValue("@nik", nik);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Biodata
                        {
                            Nik = reader.GetString(0),
                            Nama = reader.GetString(1),
                            TempatLahir = reader.GetString(2),
                            TanggalLahir = DateOnly.FromDateTime(reader.GetDateTime(3)),
                            JenisKelamin = reader.GetString(4),
                            GolonganDarah = reader.GetString(5),
                            Alamat = reader.GetString(6),
                            Agama = reader.GetString(7),
                            StatusPerkawinan = reader.GetString(8),
                            Pekerjaan = reader.GetString(9),
                            Kewarganegaraan = reader.GetString(10)
                        });
                    }
                }
            }
            return result;
        }

        public void DeleteByNik(string nik)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM biodata WHERE nik = @nik;";
                command.Parameters.AddWithValue("@nik", nik);
                command.ExecuteNonQuery();
            }
        }
    }
}