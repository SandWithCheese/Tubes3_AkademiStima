namespace src.Algorithm;

public class BoyerMoore
{
    private static Dictionary<char, int> BuildLastOccurrenceMap(string pattern)
    {
        Dictionary<char, int> lastOccurrence = [];

        for (int i = 0; i < pattern.Length; i++)
        {
            lastOccurrence[pattern[i]] = i;
        }

        return lastOccurrence;
    }

    public static bool BMSearch(string text, string pattern)
    {
        if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(text)) return false;

        Dictionary<char, int> lastOccurrence = BuildLastOccurrenceMap(pattern);
        int m = pattern.Length;
        int n = text.Length;
        // s = shift of the pattern with respect to text
        int s = 0;

        while (s <= (n - m))
        {
            int j = m - 1;

            // Compare the pattern with text from right to left
            while (j >= 0 && pattern[j] == text[s + j])
            {
                j--;
            }

            if (j < 0)
            {
                // Pattern found
                return true;
            }
            else
            {
                // Find the shift
                s += Math.Max(1, j - (lastOccurrence.TryGetValue(text[s + j], out int value) ? value : -1));
            }
        }

        // Pattern not found
        return false;
    }
}

