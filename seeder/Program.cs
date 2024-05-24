namespace seeder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Your seeding logic here
            Console.WriteLine("Running Seeder Main Method");
            Db.Init("database.db", "schema.sql");
        }
    }
}
