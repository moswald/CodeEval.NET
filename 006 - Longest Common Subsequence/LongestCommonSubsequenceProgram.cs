namespace CodeEval.LongestCommonSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    static class LongestCommonSubsequenceProgram
    {
        static void Main(string[] args)
        {
            var splitLines = File.ReadLines(args[0])
                .Where(line => line != null)
                .Select(line => line.Split(';'))
                .ToArray();

            var tasks = new Task<string>[splitLines.Length];

            for (var i = 0; i != splitLines.Length; ++i)
            {
                var index = i;
                tasks[index] = Task.Factory.StartNew(() => CompareSubsequences(splitLines[index][0], splitLines[index][1]));
            }

            Task.WaitAll(tasks);

            foreach (var t in tasks)
            {
                Console.WriteLine(t.Result);
            }
        }

        static string CompareSubsequences(string left, string right)
        {
            var longest = string.Empty;
            var longestLength = 0;
            var gate = new object();

            Parallel.ForEach(
                EnumerateSubsequences(left.ToCharArray(), 0),
                subsequence =>
                {
                    if (right.SparseContains(subsequence) && subsequence.Length > longestLength)
                    {
                        lock (gate)
                        {
                            if (subsequence.Length > longestLength)
                            {
                                longest = subsequence;
                                longestLength = subsequence.Length;
                            }
                        }
                    }
                });

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