using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace seeder
{
    public partial class MySQLToSQLiteConverter
    {
        public static void ConvertToSQLite(string mysqlFilePath, string sqliteFilePath, byte[] key, byte[] iv)
        {
            using StreamReader reader = new(mysqlFilePath);
            string line;
            string mysqlSql = "";
            while ((line = reader.ReadLine()!) != null)
            {
                mysqlSql += line;
            }

            ConvertTableToSQLite(mysqlSql, "biodata", "NIK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan", sqliteFilePath, key, iv);
            ConvertTableToSQLite(mysqlSql, "sidik_jari", "berkas_citra, nama", sqliteFilePath, key, iv);
        }

        public static void ConvertTableToSQLite(string mysqlSql, string tableName, string columns, string sqliteFilePath, byte[] key, byte[] iv)
        {
            // Extract the INSERT INTO statement for the specified table
            string insertPattern = $@"INSERT INTO `{tableName}` VALUESs*\((.+?)\);\s*";
            var insertMatch = Regex.Match(mysqlSql, insertPattern, RegexOptions.Singleline);

            if (!insertMatch.Success)
            {
                Console.WriteLine($"Failed to extract INSERT INTO statement for {tableName} from SQL file.");
                return;
            }

            string insertStatement = insertMatch.Groups[1].Value;

            using StreamWriter sw = new(sqliteFilePath, true);

            var valuePattern = new Regex(@"'((?:\\.|[^'])*)'");
            var matches = valuePattern.Matches(insertStatement);

            if (matches.Count > 0)
            {
                int len = columns.Split(", ").Length;
                int i = 0;
                string tuple = "";
                foreach (Match match in matches)
                {
                    // Extract the value, handling escaped single quotes by unescaping them
                    string value = match.Groups[1].Value.Replace("\\'", "'");
                    if (tableName == "biodata" && (i == 2 || i == 5 || i == 6 || i == 7 || i == 9 || i == 10))
                    {
                        // Add encrypted value
                        tuple += $"'{Convert.ToBase64String(AES.Encrypt(AES.Pad(Encoding.UTF8.GetBytes(value), AES.BlockSize), key, iv))}', ";
                    }
                    else
                    {
                        tuple += $"'{value}', ";
                    }
                    i++;
                    if (i == len)
                    {
                        tuple = tuple.Remove(tuple.Length - 2); // Remove the trailing comma and space

                        string insertSql = $"INSERT INTO {tableName} ({columns}) VALUES ({tuple});";
                        sw.WriteLine(insertSql);

                        i = 0;
                        tuple = "";
                    }
                }
            }
        }
    }
}
