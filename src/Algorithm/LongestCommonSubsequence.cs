using System;

namespace src.Algorithm
{
    class LongestCommonSubsequence
    {
        private static int CalculateLCS(string text1, string text2)
        {
            int m = text1.Length;
            int n = text2.Length;
            int[,] dp = new int[2, n + 1];

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (text1[i - 1] == text2[j - 1])
                        dp[i % 2, j] = dp[(i - 1) % 2, j - 1] + 1;
                    else
                        dp[i % 2, j] = Math.Max(dp[(i - 1) % 2, j], dp[i % 2, j - 1]);
                }
            }

            return dp[m % 2, n];
        }

        public static double CalculateSimilarity(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
                throw new ArgumentException("Strings must not be null.");

            int lcsLength = CalculateLCS(text1, text2);

            return (double)lcsLength / Math.Max(text1.Length, text2.Length);
        }
    }
}
