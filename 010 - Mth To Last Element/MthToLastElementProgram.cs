namespace CodeEval.MthToLastElement
{
    using System;
    using System.IO;
    using System.Linq;

    static class MthToLastElementProgram
    {
        static void Main(string[] args)
        {
            foreach (var output in File.ReadLines(args[0])
                .Where(line => line != null)
                .Select(line => line.Split(' '))
                .Select(
                    split =>
                    {
                        var index = int.Parse(split.Last());
                        return split.Reverse().Skip(index).Take(1);
                    })
                .Where(result => result.Any()))
            {
                Console.WriteLine(output.Single());
            }
        }
    }
}
