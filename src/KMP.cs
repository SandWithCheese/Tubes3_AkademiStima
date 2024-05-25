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
            // If the characters match, increment both pointers and set the value of the lps array to the length
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else
            {
                // If the characters do not match, reset the length pointer to the previous value
                if (length != 0)
                {
                    length = lps[length - 1];
                }
                // Else, if the length pointer is at the beginning, set the value of the lps array to 0 and increment i
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
            // If the characters match, increment both pointers
            if (pattern[j] == text[i])
            {
                j++;
                i++;
                maxLength = Math.Max(maxLength, j);
            }

            // If the pattern is found, reset the pattern pointer to the previous value to find the next match
            if (j == pattern.Length)
            {
                j = lps[j - 1];
            }

            else if (i < text.Length && pattern[j] != text[i])
            {
                // If the characters do not match, reset the pattern pointer to the previous value
                if (j != 0)
                {
                    j = lps[j - 1];
                }
                // If the pattern pointer is at the beginning, increment i (text pointer)
                else
                {
                    i++;
                }
            }
        }

        // Return the length of the longest match
        return maxLength;
    }

    // Function to calculate the percentage of similarity based on the longest match
    public static double CalculateSimilarity(string text, string pattern)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
            throw new ArgumentException("Strings must not be null.");

        // Find the length of the longest match
        int longestMatch = KMPSearchLongestMatch(text, pattern);

        // Return the percentage of similarity based on the length of the longest match
        return (double)longestMatch / pattern.Length * 100;
    }
}
