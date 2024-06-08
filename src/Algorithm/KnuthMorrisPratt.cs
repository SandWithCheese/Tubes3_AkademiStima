namespace src.Algorithm;

public class KnuthMorrisPratt
{
    private static int[] ComputeKMPTable(string pattern)
    {
        int[] lps = new int[pattern.Length];
        int length = 0;
        int i = 1;

        lps[0] = 0;

        while (i < pattern.Length)
        {
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else
            {
                if (length != 0)
                {
                    length = lps[length - 1];
                }
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }
        return lps;
    }

    public static bool KMPSearch(string text, string pattern)
    {
        int[] lps = ComputeKMPTable(pattern);
        int i = 0; // index for text[]
        int j = 0; // index for pattern[]

        while (i < text.Length)
        {
            if (pattern[j] == text[i])
            {
                j++;
                i++;
            }

            if (j == pattern.Length)
            {
                // Pattern found
                return true;
            }
            else if (i < text.Length && pattern[j] != text[i])
            {
                if (j != 0)
                {
                    j = lps[j - 1];
                }
                else
                {
                    i++;
                }
            }
        }

        // Pattern not found
        return false;
    }
}
