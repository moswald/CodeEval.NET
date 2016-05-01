namespace CodeEval.LongestCommonSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    static class LongestCommonSubsequenceProgram
    {
        static void Main(string[] args)
        {
            foreach (var split in File.ReadLines(args[0])
                .Where(line => line != null)
                .Select(line => line.Split(';')))
            {
                var longest = CompareSubsequences(split[0], split[1]);

                Console.WriteLine(longest.Trim());
            }
        }

        static string CompareSubsequences(string left, string right)
        {
            var longest = string.Empty;

            foreach (var subsequence in EnumerateSubsequences(left.ToCharArray(), 0))
            {
                if (right.SparseContains(subsequence) && subsequence.Length > longest.Length)
                {
                    longest = subsequence;
                }
            }

            return longest;
        }

        static IEnumerable<string> EnumerateSubsequences(IList<char> sequence, int start)
        {
            for (var i = start; i != sequence.Count; ++i)
            {
                var str = $"{sequence[i]}";
                yield return str;

                foreach (var substr in EnumerateSubsequences(sequence, i + 1))
                {
                    yield return str + substr;
                }
            }
        }

        static bool SparseContains(this string self, string sub)
        {
            var start = 0;

            foreach (var ch in sub)
            {
                var idx = self.IndexOf(ch, start);
                if (idx == -1)
                {
                    return false;
                }

                start = idx + 1;
            }

            return true;
        }
    }
}