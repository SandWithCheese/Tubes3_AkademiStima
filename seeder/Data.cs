using System.Text;
using Bogus;

public class Data
{
    private static readonly char[] vocals = ['a', 'i', 'u', 'e', 'o', 'A', 'I', 'U', 'E', 'O'];
    private static readonly string[] genders = ["Laki-Laki", "Perempuan"];
    private static readonly string[] bloodTypes = ["A", "B", "AB", "O"];
    private static readonly string[] religions = ["Islam", "Kristen", "Katolik", "Hindu", "Budha", "Konghucu"];
    private static readonly string[] status = ["Belum Menikah", "Menikah", "Cerai"];
    private static readonly string[] citizenship = ["WNI", "WNA"];

    // Generate sidik jari tuples
    public static List<Tuple<string, string>> GenerateSidikJariTuples()
    {
        // Initialize faker
        var fake = new Faker();

        // List all files and remove README.md
        var testFiles = Directory.GetFiles("../src/test").Where(f => !f.EndsWith("README.md")).ToList();

        // Generate sidik jari tuples
        var sidikJari = new List<Tuple<string, string>>();
        string? checkNumber = null;
        string? currentName = null;
        foreach (string f in testFiles)
        {
            var currentNumber = Path.GetFileName(f).Split('_')[0];
            if (checkNumber == null && currentName == null)
            {
                currentName = fake.Name.FullName();
                checkNumber = currentNumber;
            }

            if (checkNumber != currentNumber)
            {
                currentName = fake.Name.FullName();
                checkNumber = currentNumber;
            }

            if (currentName != null)
            {
                var path = f.Replace("\\", "/")[7..];
                sidikJari.Add(new Tuple<string, string>(path, currentName));
            }
        }

        return sidikJari;
    }

    // Generate biodata tuples
    public static List<(string, string, string, string, string, string, string, string, string, string, string)> GenerateBiodataTuples(List<(string, string)> sidikJari)
    {
        // Initialize faker
        var fake = new Faker();

        // Generate biodata tuples
        var biodata = new List<(string, string, string, string, string, string, string, string, string, string, string)>();
        var data = new Dictionary<string, string?>
        {
            { "nik", null },
            { "nama", null },
            { "tempat_lahir", null },
            { "tanggal_lahir", null },
            { "jenis_kelamin", null },
            { "golongan_darah", null },
            { "alamat", null },
            { "agama", null },
            { "status_perkawinan", null },
            { "pekerjaan", null },
            { "kewarganegaraan", null }
        };

        foreach (var tuple in sidikJari)
        {
            var name = tuple.Item2;
            if (!data.Values.All(v => v != null))
            {
                data["nik"] = fake.Random.Number(100000000, 999999999).ToString();
                data["nama"] = name;
                data["tempat_lahir"] = fake.Address.City();
                data["tanggal_lahir"] = fake.Date.Past(30, DateTime.Now.AddYears(-18)).ToString("yyyy-MM-dd");
                data["jenis_kelamin"] = fake.PickRandom(genders);
                data["golongan_darah"] = fake.PickRandom(bloodTypes);
                data["alamat"] = fake.Address.FullAddress();
                data["agama"] = fake.PickRandom(religions);
                data["status_perkawinan"] = fake.PickRandom(status);
                data["pekerjaan"] = fake.Name.JobTitle();
                data["kewarganegaraan"] = fake.PickRandom(citizenship);
            }

            if (data["nama"] != name)
            {
                data["nik"] = fake.Random.Number(100000000, 999999999).ToString();
                data["nama"] = name;
                data["tempat_lahir"] = fake.Address.City();
                data["tanggal_lahir"] = fake.Date.Past(30, DateTime.Now.AddYears(-18)).ToString("yyyy-MM-dd");
                data["jenis_kelamin"] = fake.PickRandom(genders);
                data["golongan_darah"] = fake.PickRandom(bloodTypes);
                data["alamat"] = fake.Address.FullAddress();
                data["agama"] = fake.PickRandom(religions);
                data["status_perkawinan"] = fake.PickRandom(status);
                data["pekerjaan"] = fake.Name.JobTitle();
                data["kewarganegaraan"] = fake.PickRandom(citizenship);
            }
            else
            {
                continue;
            }

            biodata.Add((
                data["nik"] ?? string.Empty,
                AlayGenerator(data["nama"] ?? string.Empty),
                data["tempat_lahir"] ?? string.Empty,
                data["tanggal_lahir"] ?? string.Empty,
                data["jenis_kelamin"] ?? string.Empty,
                data["golongan_darah"] ?? string.Empty,
                data["alamat"] ?? string.Empty,
                data["agama"] ?? string.Empty,
                data["status_perkawinan"] ?? string.Empty,
                data["pekerjaan"] ?? string.Empty,
                data["kewarganegaraan"] ?? string.Empty
            ));
        }

        return biodata;
    }

    public static string AlayGenerator(string name)
    {
        var methods = new[] { "orisinil", "orisinil", "orisinil", "besar-kecil", "angka", "singkat", "kombinasi" };

        var method = new Random().Next(methods.Length);

        return methods[method] switch
        {
            "orisinil" => name,
            "besar-kecil" => BesarKecil(name),
            "angka" => Angka(name),
            "singkat" => Singkat(name),
            "kombinasi" => Kombinasi(name),
            _ => name,
        };
    }

    public static string BesarKecil(string name)
    {
        var temp = new string(name.Select(c => new Random().Next(2) == 0 ? char.ToLower(c) : char.ToUpper(c)).ToArray());
        return temp;
    }

    public static string Angka(string name)
    {
        var mapping = new Dictionary<char, char>
        {
            { 'o', '0' }, { 'O', '0' },
            { 'i', '1' }, { 'I', '1' },
            { 'z', '2' }, { 'Z', '2' },
            { 'E', '3' },
            { 'A', '4' },
            { 's', '5' }, { 'S', '5' },
            { 'G', '6' },
            { 'T', '7' },
            { 'B', '8' },
            { 'g', '9' }
        };

        var temp = new string(name.Select(c => mapping.TryGetValue(c, out char value) ? value : c).ToArray());
        return temp;
    }

    public static string Singkat(string name)
    {
        var words = name.Split(' ');
        var result = new StringBuilder();

        foreach (var word in words)
        {
            if (word.Length > 0)
            {
                // Keep the first letter and remove vowels from the rest of the word
                result.Append(word[0]);
                result.Append(new string(word.Skip(1).Where(c => !vocals.Contains(c)).ToArray()));
            }
            result.Append(' ');
        }

        return result.ToString().Trim();
    }


    public static string Kombinasi(string name)
    {
        return Singkat(Angka(BesarKecil(name)));
    }
}