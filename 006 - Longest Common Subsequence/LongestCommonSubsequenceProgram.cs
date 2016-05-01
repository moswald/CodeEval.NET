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
                var left = InitializeSubsequences(split[0]);
                var longest = CompareSubsequences(left, split[1]);

                Console.WriteLine(longest.Trim());

                left.Clear();
            }
        }

        static ISet<string> InitializeSubsequences(string sequence)
        {
            var set = new HashSet<string>();

            foreach (var subsequence in EnumerateSubsequences(sequence.ToCharArray(), 0))
            {
                set.Add(subsequence);
            }

            return set;
        }

        static string CompareSubsequences(ISet<string> set, string sequence)
        {
            var longest = string.Empty;

            foreach (var subsequence in EnumerateSubsequences(sequence.ToCharArray(), 0))
            {
                if (set.Contains(subsequence) && subsequence.Length > longest.Length)
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
    }
}
