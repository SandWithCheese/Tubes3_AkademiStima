namespace seeder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Your seeding logic here
            Console.WriteLine("Running Seeder Main Method");
            Db.Init("database.db", "schema.sql");
            // Comment all the lines below to use our seeder
            // Uncomment the seed method in db

            string mysqlDumpFilePath = "dummy_stima_kr.sql";
            string sqliteSqlFilePath = "sqlite_dump.sql";

            DotNetEnv.Env.Load(".env");
            byte[] _aesKey = Convert.FromBase64String(DotNetEnv.Env.GetString("AES_KEY"));
            byte[] _aesIv = Convert.FromBase64String(DotNetEnv.Env.GetString("AES_IV"));

            MySQLToSQLiteConverter.ConvertToSQLite(mysqlDumpFilePath, sqliteSqlFilePath, _aesKey, _aesIv);

            Console.WriteLine("Conversion completed successfully.");

            Db.Connect("database.db");
            Db.Migrate(sqliteSqlFilePath);
        }
    }
}
