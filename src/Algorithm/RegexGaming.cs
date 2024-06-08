using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace src.Algorithm;

public class RegexGaming
{
    // Simpan mapping karakter alay ke karakter asli
    private static readonly Dictionary<char, string> AngkaKapitalMapping = new()
    {
        { 'o', "[oO0]" }, { 'O', "[oO0]" },
        { 'i', "[iI1]" }, { 'I', "[iI1]" },
        { 'z', "[zZ2]" }, { 'Z', "[zZ2]" },
        { 'E', "[Ee3]" }, { 'e', "[Ee3]" },
        { 'A', "[Aa4]" }, { 'a', "[Aa4]" },
        { 's', "[sS5]" }, { 'S', "[sS5]" },
        { 'G', "[Gg6]" }, { 'g', "[Gg6]" },
        { 'T', "[Tt7]" }, { 't', "[Tt7]" },
        { 'B', "[Bb8]" }, { 'b', "[Bb8]" },
    };

    private static readonly HashSet<char> Vowels = new() { 'a', 'i', 'u', 'e', 'o', 'A', 'I', 'U', 'E', 'O' };

    public static string GenerateRegexPattern(string original)
    {
        var methods = new[] { "orisinil", "angka-besar-kecil", "singkat", "kombinasi" };
        var regexPatterns = methods.Select(method => method switch
        {
            "orisinil" => GenerateOrisinilPattern(original),
            "angka-besar-kecil" => GenerateAngkaandBesarKecilPattern(original),
            "singkat" => GenerateSingkatPattern(original),
            "kombinasi" => GenerateKombinasiPattern(original),
            _ => original
        }).ToList();

        return string.Join("|", regexPatterns);
    }

    public static string FixAlayWord(string originalText, string alayText)
    {
        // Split the original text into words
        var originalWords = originalText.Split(' ');

        // Create a dictionary to store the regex patterns and their corresponding original words
        var regexToOriginalWord = new Dictionary<string, string>();

        foreach (var word in originalWords)
        {
            // Generate regex pattern for each word (tiap kata ada regexnya masing-masing)
            string regexPattern = GenerateRegexPattern(word);
            regexToOriginalWord[regexPattern] = word;
        }

        // Replace each alay word with the original word
        string[] alayWords = alayText.Split(' ');
        // Create a list to store the corrected words
        List<string> correctedWords = new List<string>();

        foreach (var alayWord in alayWords)
        {
            string correctedWord = alayWord;

            foreach (var pair in regexToOriginalWord)
            {
                string regexPattern = pair.Key;
                string originalWord = pair.Value;

                // Compile the regex pattern, pakai IgnoreCase biar gak peduli huruf besar kecil
                Regex alayRegex = new Regex(regexPattern, RegexOptions.IgnoreCase);

                // Check if the alay word matches the regex pattern
                if (alayRegex.IsMatch(alayWord))
                {
                    // Replace the alay word with the original word
                    correctedWord = originalWord;
                    break; // Stop checking other patterns once a match is found
                }
            }

            correctedWords.Add(correctedWord);
        }

        // Join the corrected words back into a single string
        string correctedText = string.Join(" ", correctedWords);

        return correctedText;
    }

    private static string GenerateOrisinilPattern(string original)
    {
        return Regex.Escape(original); // Escape all special characters
    }

    private static string GenerateAngkaandBesarKecilPattern(string original)
    {
        return string.Concat(original.Select(c => AngkaKapitalMapping.ContainsKey(c) ? AngkaKapitalMapping[c] : Regex.Escape(c.ToString())));
    }

    private static string GenerateSingkatPattern(string original)
    {
        var words = original.Split(' ');
        var result = new StringBuilder();
        foreach (var word in words)
        {
            if (word.Length > 0)
            {
                result.Append(Regex.Escape(word[0].ToString()));
                result.Append(string.Concat(word.Skip(1).Where(c => !Vowels.Contains(c)).Select(c => Regex.Escape(c.ToString()))));
            }
            result.Append(@"\s*");
        }

        return result.ToString().Trim();
    }

    private static string GenerateKombinasiPattern(string original)
    {
        var angkaPattern = GenerateAngkaandBesarKecilPattern(original);
        return GenerateSingkatPattern(angkaPattern);
    }

    // public static void Main(string[] args)
    // {
    //     string originalText = "Saya Suka Nasi Padang";
    //     string alayText = "S4y4 sUkA Ns1 P4d4n6";

    //     string correctedText = FixAlayWord(originalText, alayText);

    //     Console.WriteLine($"Teks asli: {originalText}");
    //     Console.WriteLine($"Teks alay: {alayText}");
    //     Console.WriteLine($"Teks yang dikoreksi: {correctedText}");
    // }
}
