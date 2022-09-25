using System;

namespace Longest_Substring_with_At_Least_K_Repeating_Characters
{
    class Program
    {
        static void Main(string[] args)
        {
			string str = "abababbdabcabc";
			int k = 2;
			Solution s = new Solution();
			var answer = s.LongestSubstring(str, k);
			Console.WriteLine(answer);

        }
    }

    // Time - O(N^2)
	public class Solution
	{
		public int LongestSubstring(string s, int k)
		{
            // Call divide and conquer helper method
            return div(s, 0, s.Length, k);
        }
        private int div(string s, int start, int end, int k)
        {

            /**
             * Base Case 1 of 2:
             * 
             * If this substring is shorter than k, then no characters in it
             * can be repeated k times, therefore this substring and all
             * substrings that could be formed from it are invalid,
             * therefore return 0.
             */
            if (end - start < k) return 0;

            /**
             * Count the frequency of characters in this substring.
             * 
             * We are guaranteed from the problem statement that the given String
             * can only contain lowercase (English?) characters, so we use a
             * table of length 26 to store the character counts.
             */
            int[] a = new int[26];
            for (int i = start; i < end; i++)
            {
                a[s[i] - 'a']++;
            }

            // For every character in the above frequency table
            for (int i = 0; i < a.Length; i++)
            {

                /**
                 * If this character occurs at least once, but fewer than k times
                 * in this substring, we know:
                 * (1) this character cannot be part of the longest valid substring,
                 * (2) the current substring is not valid.
                 * 
                 * Hence, we will "split" this substring on this character,
                 * wherever it occurs, and check the substrings formed by that split
                 */
                if (a[i] > 0 && a[i] < k)
                {

                    /**
                     * Look for each occurrence of this character (i + 'a')
                     * in this substring.
                     */
                    for (int j = start; j < end; j++)
                    {
                        if (s[j] == i + 'a')
                        {

                            // "Split" into two substrings to solve recursively
                            int l = div(s, start, j, k);
                            int r = div(s, j + 1, end, k);
                            return Math.Max(l, r);
                        }
                    }
                }
            }

            /**
             * Base Case 2 of 2:
             * 
             * If every character in this substring occurs at least k times,
             * then this is a valid substring, so return this substring's length.
             */
            return end - start;
        }
    }
}
