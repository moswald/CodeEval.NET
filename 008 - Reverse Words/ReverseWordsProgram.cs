namespace CodeEval.ReverseWords
{
    using System;
    using System.IO;
    using System.Linq;

    static class ReverseWordsProgram
    {
        static void Main(string[] args)
        {
            foreach (var reversed in File.ReadLines(args[0])
                .Where(line => line != null)
                .Select(line => line.Split(' ').Reverse())
                .Select(split => string.Join(" ", split)))
            {
                Console.WriteLine(reversed);
            }
        }
    }
}
