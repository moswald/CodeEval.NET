namespace CodeEval.LongestLines
{
    using System;
    using System.IO;
    using System.Linq;

    static class LongestLinesProgram
    {
        static void Main(string[] args)
        {
            var file = File.ReadAllLines(args[0]);
            var count = int.Parse(file[0]);

            foreach (var line in file.Skip(1).OrderByDescending(line => line.Length).Take(count))
            {
                Console.WriteLine(line);
            }
        }
    }
}
