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
                tasks[index] = Task.Factory.StartNew(() => new string(CompareSubsequences(splitLines[index][0], splitLines[index][1]).ToArray()));
            }

            Task.WaitAll(tasks);

            foreach (var t in tasks)
            {
                Console.WriteLine(t.Result);
            }
        }

        static IEnumerable<char> CompareSubsequences(string left, string right)
        {
            var longest = Enumerable.Empty<char>();
            var longestLength = 0;

            foreach (var subsequence in EnumerateSubsequences(left.ToCharArray(), 0))
            {
                if (subsequence.Length > longestLength && right.SparseContains(subsequence.Value))
                {
                    longest = subsequence.Value;
                    longestLength = subsequence.Length;
                }
            }

            return longest;
        }

        static IEnumerable<Subsequence> EnumerateSubsequences(IList<char> sequence, int start)
        {
            for (var i = start; i != sequence.Count; ++i)
            {
                yield return SingleSequences[sequence[i]];

                foreach (var substr in EnumerateSubsequences(sequence, i + 1))
                {
                    yield return new Subsequence
                    {
                        Value = Yield(sequence[i], substr.Value),
                        Length = substr.Length + 1
                    };
                }
            }
        }

        static IEnumerable<char> Yield(char ch, IEnumerable<char> more)
        {
            yield return ch;
            foreach (var next in more)
            {
                yield return next;
            }
        }

        static bool SparseContains(this string self, IEnumerable<char> sub)
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

        struct Subsequence
        {
            public IEnumerable<char> Value;
            public int Length;
        }

        static readonly Subsequence[] SingleSequences = InitializeSingleSubsequences();

        static Subsequence[] InitializeSingleSubsequences()
        {
            var result = new Subsequence['z' + 1];

            result[' '] = new Subsequence
            {
                Value = new[] { ' ' },
                Length = 1
            };

            for (var i = '0'; i <= '9'; ++i)
            {
                result[i] = new Subsequence
                {
                    Value = new[] { i },
                    Length = 1
                };
            }

            for (var i = 'A'; i <= 'Z'; ++i)
            {
                result[i] = new Subsequence
                {
                    Value = new[] { i },
                    Length = 1
                };
            }

            for (var i = 'a'; i <= 'z'; ++i)
            {
                result[i] = new Subsequence
                {
                    Value = new[] { i },
                    Length = 1
                };
            }

            return result;
        }
    }
}