namespace CodeEval.DetectingCycles
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    static class DetectingCyclesProgram
    {
        static void Main(string[] args)
        {
            foreach (var result in File.ReadLines(args[0])
                .AsParallel()
                .Where(line => line != null)
                .Select(line => line.Split(' ').Select(int.Parse).ToArray())
                .Select(DetectCycle))
            {
                Console.WriteLine(result);
            }
        }

        static string DetectCycle(IList<int> values)
        {
            var first = values.Last();

            var cycle = values.Reverse()
                .Skip(1)
                .TakeWhile(x => x != first)
                .Reverse()
                .Aggregate(
                    string.Empty,
                    (s, value) => $"{s}{value} ");

            return $"{cycle}{first}";
        }
    }
}
