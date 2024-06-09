using System;

namespace src.Algorithm
{
    class LongestCommonSubsequence
    {
        private static int[,] CalculateLCS(string text1, string text2)
        {
            int m = text1.Length;
            int n = text2.Length;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                        dp[i, j] = 0;
                    else if (text1[i - 1] == text2[j - 1])
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    else
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }

            return dp;
        }

        public static double CalculateSimilarity(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
                throw new ArgumentException("Strings must not be null.");

            int[,] lcs = CalculateLCS(text1, text2);
            int lcsLength = lcs[text1.Length, text2.Length];

            return (double)lcsLength / Math.Max(text1.Length, text2.Length);
        }
    }
}