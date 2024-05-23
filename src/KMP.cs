namespace src;

class KMP
{
    private static int[] findArrayKMP(string pattern)
    {
        int length = 0;
        int i = 1;
        int[] lps = new int[pattern.Length];
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

    // Find the longest match
    private static int KMPSearchLongestMatch(string text, string pattern)
    {
        int[] lps = findArrayKMP(pattern);
        int i = 0; // index for text[]
        int j = 0; // index for pattern[]
        int maxLength = 0; // length of the longest match

        while (i < text.Length)
        {
            if (pattern[j] == text[i])
            {
                j++;
                i++;
                maxLength = Math.Max(maxLength, j);
            }

            if (j == pattern.Length)
            {
                j = lps[j - 1];
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
        return maxLength;
    }

    // Function to calculate the percentage of similarity based on the longest match
    public static double CalculateSimilarity(string text, string pattern)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
            throw new ArgumentException("Strings must not be null.");

        int longestMatch = KMPSearchLongestMatch(text, pattern);

        return (double)longestMatch / pattern.Length * 100;
    }
}
