using Bogus;

public class Data
{
    // Generate sidik jari tuples
    public static List<Tuple<string, string>> GenerateSidikJariTuples()
    {
        // Initialize faker
        var fake = new Faker();

        // List all files and remove README.md
        var testFiles = Directory.GetFiles("..\\src\\test").Where(f => !f.EndsWith("README.md")).ToList();

        // Generate sidik jari tuples
        var sidikJari = new List<Tuple<string, string>>();
        string? checkNumber = null;
        string? currentName = null;
        foreach (String f in testFiles)
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
                sidikJari.Add(new Tuple<string, string>(f.Substring(6), currentName));
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
                data["jenis_kelamin"] = fake.PickRandom(new[] { "Laki-Laki", "Perempuan" });
                data["golongan_darah"] = fake.PickRandom(new[] { "A", "B", "AB", "O" });
                data["alamat"] = fake.Address.FullAddress();
                data["agama"] = fake.PickRandom(new[] { "Islam", "Kristen", "Katolik", "Hindu", "Budha", "Konghucu" });
                data["status_perkawinan"] = fake.PickRandom(new[] { "Belum Menikah", "Menikah", "Cerai" });
                data["pekerjaan"] = fake.Name.JobTitle();
                data["kewarganegaraan"] = fake.PickRandom(new[] { "WNI", "WNA" });
            }

            if (data["nama"] != name)
            {
                data["nik"] = fake.Random.Number(100000000, 999999999).ToString();
                data["nama"] = name;
                data["tempat_lahir"] = fake.Address.City();
                data["tanggal_lahir"] = fake.Date.Past(30, DateTime.Now.AddYears(-18)).ToString("yyyy-MM-dd");
                data["jenis_kelamin"] = fake.PickRandom(new[] { "Laki-Laki", "Perempuan" });
                data["golongan_darah"] = fake.PickRandom(new[] { "A", "B", "AB", "O" });
                data["alamat"] = fake.Address.FullAddress();
                data["agama"] = fake.PickRandom(new[] { "Islam", "Kristen", "Katolik", "Hindu", "Budha", "Konghucu" });
                data["status_perkawinan"] = fake.PickRandom(new[] { "Belum Menikah", "Menikah", "Cerai" });
                data["pekerjaan"] = fake.Name.JobTitle();
                data["kewarganegaraan"] = fake.PickRandom(new[] { "WNI", "WNA" });
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

        switch (methods[method])
        {
            case "orisinil":
                return name;
            case "besar-kecil":
                return BesarKecil(name);
            case "angka":
                return Angka(name);
            case "singkat":
                return Singkat(name);
            case "kombinasi":
                return Kombinasi(name);
            default:
                return name;
        }
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

        var temp = new string(name.Select(c => mapping.ContainsKey(c) ? mapping[c] : c).ToArray());
        return temp;
    }

    public static string Singkat(string name)
    {
        var temp = new string(name.Where(c => !new[] { 'a', 'i', 'u', 'e', 'o', 'A', 'I', 'U', 'E', 'O' }.Contains(c)).ToArray());
        return temp;
    }

    public static string Kombinasi(string name)
    {
        return Singkat(Angka(BesarKecil(name)));
    }
}