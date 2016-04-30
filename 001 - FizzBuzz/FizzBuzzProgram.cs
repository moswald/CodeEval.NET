namespace CodeEval.FizzBuzz
{
    using System;
    using System.IO;
    using System.Linq;

    static class FizzBuzzProgram
    {
        static void Main(string[] args)
        {
            foreach (var output in File.ReadLines(args[0])
                .Where(line => line != null)
                .Select(line => line.Split(' '))
                .Select(split => Challenge.FizzBuzz(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])))
                .Select(strings => string.Join(" ", strings)))
            {
                Console.WriteLine(output);
            }
        }
    }
}
